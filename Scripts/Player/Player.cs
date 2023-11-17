using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class Player : MonoBehaviour
{
    #region State Machine

    public PlayerStateMachine StateMachine { get; private set; }
    public PlayerIdleState IdleState { get; private set; }
    public PlayerRunningState RunningState { get; private set; }
    public PlayerJumpingState JumpingState { get; private set; }
    public PlayerFallingState FallingState { get; private set; }
    public PlayerWallJumpingState WallJumpingState { get; private set; }
    public PlayerDashingState DashingState { get; private set; }
    public PlayerSlidingState SlidingState { get; private set; }
    
    #endregion
    
    [SerializeField] internal PlayerData Data;
    [SerializeField] internal UISkills uiSkills;

    internal Transform PlayerTransform;
    internal Rigidbody2D Rigidbody2D;
    internal UIManager UIManager;
    
    internal float MoveInput;
    internal float MoveVariableInput;
    internal int LookDirection;

    [Header ("Particles")]
    [SerializeField] internal ParticleSystem runParticle;
    [SerializeField] internal ParticleSystem groundParticle;
    [SerializeField] internal ParticleSystem jumpParticle;
    
    #region Physics
    [Header ("Physics")]
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private Transform _groundCheckPointA;
    [SerializeField] private Transform _groundCheckPointB;
    private List<Collider2D> _groundList = new();
    private ContactFilter2D _groundContactFilter;
    #endregion
    
    #region Timers
    internal float LastTimeOnGround;
    internal float LastTimeOnWall;
    internal float LastTimeOnWallRight;
    internal float LastTimeOnWallLeft;
    internal float TakeDamageCooldown;
    #endregion
    
    #region Counter
    internal int JumpCounter;
    internal int DashCounter;
    #endregion
    
    #region Bools
    internal bool IsOnGround;
    #endregion

    public Vector3 currentRotation;

    public CameraShake cameraShake;

    private void Awake()
    {
        #region State Machine
        StateMachine = new PlayerStateMachine();

        IdleState = new PlayerIdleState(this);
        RunningState = new PlayerRunningState(this);
        JumpingState = new PlayerJumpingState(this);
        FallingState = new PlayerFallingState(this);
        WallJumpingState = new PlayerWallJumpingState(this);
        DashingState = new PlayerDashingState(this);
        SlidingState = new PlayerSlidingState(this);
        #endregion
        
        PlayerTransform = gameObject.transform;
        Rigidbody2D = PlayerTransform.GetComponent<Rigidbody2D>();
        UIManager = FindObjectOfType<UIManager>();
    }

    private void Start()
    {
        StateMachine.ChangeState(IdleState);
        
        _groundContactFilter = new ContactFilter2D
        {
            useLayerMask = true,
            layerMask = _groundLayer
        };

        LookDirection = 1;
    }

    private void Update()
    {
        StateMachine.currentPlayerState.PhysicsUpdate();
        
        LastTimeOnGround += Time.deltaTime;
        LastTimeOnWall += Time.deltaTime;
        LastTimeOnWallRight += Time.deltaTime;
        LastTimeOnWallLeft += Time.deltaTime;
        TakeDamageCooldown += Time.deltaTime;
        
        MoveInput = Input.GetAxisRaw("Horizontal");
        MoveVariableInput = Input.GetAxis("Horizontal");

        if (MoveInput != 0)
        {
            LookDirection = (int)MoveInput;
        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            uiSkills.Inventory();
        }
    }

    private void FixedUpdate()
    {
        StateMachine.currentPlayerState.PhysicsUpdate();
        
        #region Collision
        Physics2D.OverlapArea(_groundCheckPointA.position, _groundCheckPointB.position, _groundContactFilter, _groundList);
        #endregion
        
        if (_groundList.Count != 0)
        {
            IsOnGround = true;
            
            LastTimeOnGround = 0;
            
            Rigidbody2D.freezeRotation = false;
        }
        else
        {
            IsOnGround = false;
            var rotation = Vector3.RotateTowards(Rigidbody2D.position, new Vector2(0,0), 1f, Time.deltaTime);
            Rigidbody2D.rotation = rotation.z;
            Rigidbody2D.freezeRotation = true;
        }
    } 

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;

        if (GizmosManager.ShowPlayerGroundTrigger)
        {
            var firstPoint = _groundCheckPointA.position;
            var thirdPoint = _groundCheckPointB.position;

            var secondPoint = new Vector2(thirdPoint.x, firstPoint.y);
            var fourthPoint = new Vector2(firstPoint.x, thirdPoint.y);
        
            Gizmos.DrawLine(firstPoint, secondPoint);
            Gizmos.DrawLine(secondPoint, thirdPoint);
            Gizmos.DrawLine(thirdPoint, fourthPoint);
            Gizmos.DrawLine(fourthPoint, firstPoint);
        }
    }

    public void TakeDamage(float damage)
    {
        if (TakeDamageCooldown > 1.0f)
        {
            TakeDamageCooldown = 0;
            Data.health -= damage;
            
            UIManager.UpdateInterface();
        }
    }
}
