using UnityEngine;

public abstract class NPCBaseClass : MonoBehaviour
{
    #region State Machine

    public NPCStateMachine StateMachine { get; private set; }
    public IdleState IdleState { get; private set; }
    public SearchingState SearchingState { get; private set; }
    public ChasingState ChasingState { get; private set; }
    public AttackingState AttackingState { get; private set; }

    #endregion

    [SerializeField] private protected NpcSO npcSo;
    
    [SerializeField] private protected LayerMask playerLayer;
    [SerializeField] private protected LayerMask groundLayer;
    
    private protected Rigidbody2D Rigidbody2D;
    private SpriteRenderer SpriteRenderer;

    private float _lastPosition;
    private protected int _lookingDirection = 1;

    private protected virtual void Awake()
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
    }

    private protected virtual void Start()
    {
        SpriteRenderer.sprite = npcSo.spriteRenderer;
    }

    private protected virtual void Update()
    {
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
    }

    private protected virtual void FixedUpdate()
    {
        StateMachine.CurrentNpcStates.PhysicsUpdate();
    }

    public virtual void EnterIdleState()
    {
    }

    public virtual void ExitIdleState()
    {
    }

    public virtual void EnterSearchState()
    {
    }

    public virtual void ExitSearchState()
    {
    }

    public virtual void EnterChaseState()
    {
    }

    public virtual void ExitChaseState()
    {
    }

    public virtual void EnterAttackState()
    {
    }

    public virtual void ExitAttackState()
    {
    }

    public virtual void Idle()
    {
    }

    public virtual void Search()
    {
    }

    public virtual void Chase()
    {
    }

    public virtual void Attack()
    {
    }
}