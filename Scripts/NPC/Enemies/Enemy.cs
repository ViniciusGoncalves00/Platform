using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : NPCBaseClass
{
    #region State Machine

    public NPCStateMachine StateMachine { get; private set; }
    public IdleState IdleState { get; private set; }
    public SearchingState SearchingState { get; private set; }
    public ChasingState ChasingState { get; private set; }
    public AttackingState AttackingState { get; private set; }

    #endregion

    [SerializeField] internal int velocity = 1;
    private float _lastPosition;
    private int _lookingDirection = 1;

    private protected Rigidbody2D Rigidbody2D;
    private protected SpriteRenderer SpriteRenderer;

    [SerializeField] private protected Vector2[] waypoints;
    private protected int RandomIndex;
    private Vector2 _groundCheckPointA, _groundCheckPointB;

    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private LayerMask groundLayer;
    internal readonly List<Collider2D> ChaseTrigger = new();
    internal readonly List<Collider2D> AttackTrigger = new();
    internal readonly List<Collider2D> GroundTrigger = new();
    [SerializeField] private float chaseRange = 4;
    [SerializeField] private float attackRange = 2;

    private ContactFilter2D _contactFilterPlayer;
    private ContactFilter2D _contactFilterGround;

    internal Player Player;

    private void Awake()
    {
        #region State Machine

        StateMachine = new NPCStateMachine();

        IdleState = new IdleState(this);
        SearchingState = new SearchingState(this);
        ChasingState = new ChasingState(this);
        AttackingState = new AttackingState(this);

        #endregion

        Rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
        SpriteRenderer = gameObject.GetComponent<SpriteRenderer>();

        Player = FindObjectOfType<Player>();
    }

    private void Start()
    {
        StateMachine.ChangeState(SearchingState);

        _contactFilterPlayer = new ContactFilter2D()
        {
            useLayerMask = true,
            layerMask = playerLayer
        };

        _contactFilterGround = new ContactFilter2D()
        {
            useLayerMask = true,
            layerMask = groundLayer
        };
    }

    private void FixedUpdate()
    {
        StateMachine.CurrentNpcStates.PhysicsUpdate();

        var position = gameObject.transform.position;
        if (position.x > _lastPosition)
        {
            _lookingDirection = 1;
            SpriteRenderer.flipX = false;
        }
        else if (position.x < _lastPosition)
        {
            _lookingDirection = -1;
            SpriteRenderer.flipX = true;
        }

        _lastPosition = position.x;

        Physics2D.OverlapCircle(position, attackRange, _contactFilterPlayer, AttackTrigger);
        Physics2D.OverlapCircle(position, chaseRange, _contactFilterPlayer, ChaseTrigger);

        _groundCheckPointA = new Vector3(position.x + 1.0f * _lookingDirection, position.y - 0.5f, 0);
        _groundCheckPointB = new Vector3(_groundCheckPointA.x + 0.2f * _lookingDirection, _groundCheckPointA.y - 0.5f, 0);
        Physics2D.OverlapArea(_groundCheckPointA, _groundCheckPointB, _contactFilterGround, GroundTrigger);

        // var pointA = new Vector3(position.x - 0.1f, position.y - 0.5f, 0);
        // var pointB = new Vector3(position.x + 0.1f, position.y - 1.0f, 0);
        // Physics2D.OverlapArea(pointA, pointB, _contactFilterGround, _groundTrigger);
    }

    private void OnDrawGizmos()
    {
        var position = gameObject.transform.position;

        if (GizmosManager.ShowEnemyGroundTrigger)
        {
            Gizmos.color = Color.green;
            var size = new Vector3(_groundCheckPointB.x - _groundCheckPointA.x, _groundCheckPointB.y - _groundCheckPointA.y,
                0);
            var center = new Vector3(size.x / 2, size.y / 2);
            var centerPosition = new Vector3(position.x + center.x + 1.0f * _lookingDirection, position.y + center.y - 0.5f,
                0);
            Gizmos.DrawWireCube(centerPosition, size);

            // var center = new Vector2(position.x, position.y - 0.75f);
            // var size = new Vector2(0.2f, 0.5f);
            //
            // Gizmos.DrawWireCube(center, size);
        }

        if (GizmosManager.ShowChaseTrigger)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(position, chaseRange);
        }

        if (GizmosManager.ShowAttackTrigger)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(position, attackRange);
        }

        if (GizmosManager.ShowWaypoints)
        {
            Gizmos.color = Color.magenta;
            foreach (var waypoint in waypoints)
            {
                Gizmos.DrawSphere(waypoint, 0.2f);
            }
        }
    }
}