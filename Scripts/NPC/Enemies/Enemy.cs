using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : NPCBaseClass
{
    [SerializeField] private protected Vector2[] waypoints;
    private protected int RandomIndex;
    
    private protected List<Collider2D> ChaseTrigger = new();
    private protected List<Collider2D> AttackTrigger = new();
    private protected List<Collider2D> TouchTrigger = new();
    private protected List<Collider2D> GroundTrigger = new();
    [SerializeField] private protected float chaseRange = 4;
    [SerializeField] private protected float attackRange = 2;
    [SerializeField] private protected float touchRange = 1;

    private protected ContactFilter2D _contactFilterPlayer;
    private protected ContactFilter2D _contactFilterGround;
    
    private protected Vector2 Target;

    private protected Player Player;

    private protected override void Awake()
    {
        base.Awake();

        Player = FindObjectOfType<Player>();
    }

    private protected override void Start()
    {
        base.Start();
        
        StateMachine.ChangeState(IdleState);

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

    private protected override void Update()
    {
        base.Update();
    }

    private protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    private protected virtual void OnDrawGizmos()
    {
    }
    
    public override void Idle()
    {
        if (ChaseTrigger.Count == 0)
        {
            StateMachine.ChangeState(SearchingState);
        }

        else if (AttackTrigger.Count != 0)
        {
            StateMachine.ChangeState(AttackingState);
        }
    }

    public override void Search()
    {
        if (ChaseTrigger.Count != 0)
        {
            StateMachine.ChangeState(ChasingState);
        }
    }

    public override void Chase()
    {
        base.Chase();
    }

    public override void Attack()
    {
        base.Attack();
    }
}