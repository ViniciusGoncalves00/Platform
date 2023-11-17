using UnityEngine;

public class FlyEnemy : Enemy
{
    [Header("Square Movement")]
    [SerializeField] private protected Vector3 bottomLeft;
    [SerializeField] private protected Vector3 topRight;
    
    private bool _playerOutsideInHorizontal;
    private bool _playerOutsideInVertical;
    
    private protected override void Awake()
    {
        base.Awake();
    }

    private protected override void Start()
    {
        base.Start();

        Target = transform.position;
    }
    private protected override void Update()
    {
        base.Update();
    }

    private protected override void FixedUpdate()
    {
        base.FixedUpdate();
        
        var position = gameObject.transform.position;
        
        Physics2D.OverlapCircle(position, chaseRange, _contactFilterPlayer, ChaseTrigger);
        Physics2D.OverlapCircle(position, attackRange, _contactFilterPlayer, AttackTrigger);
        Physics2D.OverlapCircle(position, touchRange, _contactFilterPlayer, TouchTrigger);
    }
    
    public override void Chase()
    {
        base.Chase();
        
        if (AttackTrigger.Count != 0)
        {
            StateMachine.ChangeState(AttackingState);
        }

        else if (ChaseTrigger.Count == 0)
        {
            StateMachine.ChangeState(SearchingState);
        }
    }
    
    private protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        
        var position = gameObject.transform.position;

        if (GizmosManager.ShowEnemyGroundTrigger)
        {
        }

        if (GizmosManager.ShowChaseTrigger)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(position, chaseRange);
        }

        if (GizmosManager.ShowAttackTrigger)
        {
            Gizmos.color = new Color(1.0f, 0.5f, 0.0f);
            Gizmos.DrawWireSphere(position, attackRange);
        }
        
        if (GizmosManager.ShowTouchTrigger)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(position, touchRange);
        }
        
        if (GizmosManager.ShowWaypoints)
        {
            Gizmos.color = Color.magenta;

            GizmosExtend.DrawArea(bottomLeft, topRight);
        }
    }

    private protected void SquareSearch()
    {
        if (Vector2.Distance(Rigidbody2D.position, Target) < 0.1f)
        {
            var randomX = Random.Range(bottomLeft.x, topRight.x);
            var randomY = Random.Range(bottomLeft.y, topRight.y);
            Target = new Vector3(randomX, randomY, 0.0f);
        }
        
        Rigidbody2D.position = Vector3.MoveTowards(Rigidbody2D.position, Target, npcSo.velocitySearch * Time.deltaTime);
    }

    private protected void SquareChase()
    {
        Target = Vector3.MoveTowards(Rigidbody2D.position, Player.Rigidbody2D.position, npcSo.velocityChase * Time.deltaTime);

        OutsideArea();

        if (_playerOutsideInHorizontal)
        {
            if (Rigidbody2D.position.x < bottomLeft.x || Rigidbody2D.position.x > topRight.x)
            {
                Target = new Vector2(Rigidbody2D.position.x, Target.y);
            } 
        }

        if (_playerOutsideInVertical)
        {
            if (Rigidbody2D.position.y < bottomLeft.y || Rigidbody2D.position.y > topRight.y)
            {
                Target = new Vector2(Target.x, Rigidbody2D.position.y);
            }
        }
        
        Rigidbody2D.position = Target;
    }
    
    private void OutsideArea()
    {
        if (Target.x < bottomLeft.x ||
            Target.x > topRight.x)
        {
            _playerOutsideInHorizontal = true;
        }
        
        else
        {
            _playerOutsideInHorizontal = false;
        }

        if (Target.y > bottomLeft.y ||
            Target.y < topRight.y)
        {
            _playerOutsideInVertical = true;
        }
        else
        {
            _playerOutsideInVertical = false;
        }
    }

    private protected void PolygonalMovement()
    {
        
    }

    private protected void WaypointsMovement()
    {
        
    }
}