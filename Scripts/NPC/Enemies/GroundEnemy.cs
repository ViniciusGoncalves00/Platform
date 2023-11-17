using UnityEngine;

public abstract class GroundEnemy : Enemy
{
    private protected Vector2 _groundCheckPointA, _groundCheckPointB;
    
    private float _timer; //remove when doing attack logic
    
    private protected override void FixedUpdate()
    {
        base.FixedUpdate();
        
        var position = gameObject.transform.position;
        
        _groundCheckPointA = new Vector3(position.x + 1.0f * _lookingDirection, position.y - 0.5f, 0);
        _groundCheckPointB = new Vector3(_groundCheckPointA.x + 0.2f * _lookingDirection, _groundCheckPointA.y - 0.5f, 0);
        
        Physics2D.OverlapArea(_groundCheckPointA, _groundCheckPointB, _contactFilterGround, GroundTrigger);
        
        Physics2D.OverlapCircle(position, chaseRange, _contactFilterPlayer, ChaseTrigger);
        Physics2D.OverlapCircle(position, attackRange, _contactFilterPlayer, AttackTrigger);
        Physics2D.OverlapCircle(position, touchRange, _contactFilterPlayer, TouchTrigger);
    }
    
    private protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        
        var position = gameObject.transform.position;

        if (GizmosManager.ShowEnemyGroundTrigger)
        {
            Gizmos.color = Color.green;
            var size = new Vector3(_groundCheckPointB.x - _groundCheckPointA.x, _groundCheckPointB.y - _groundCheckPointA.y,
                0);
            var center = new Vector3(size.x / 2, size.y / 2);
            var centerPosition = new Vector3(position.x + center.x + 1.0f * _lookingDirection, position.y + center.y - 0.5f,
                0);
            size.z = Mathf.Abs(size.x);
            Gizmos.DrawWireCube(centerPosition, size);
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
            foreach (var waypoint in waypoints)
            {
                Gizmos.DrawSphere(waypoint, 0.2f);
            }
        }
    }

    public override void Idle()
    {
        base.Idle();

        if (AttackTrigger.Count != 0)
        {
            StateMachine.ChangeState(AttackingState);
        }

        else if (ChaseTrigger.Count == 0)
        {
            StateMachine.ChangeState(SearchingState);
        }
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

        else if (GroundTrigger.Count == 0)
        {
            StateMachine.ChangeState(IdleState);
        }
    }
    
    public override void Attack()
    {
        base.Attack();
        
        if (AttackTrigger.Count == 0)
        {
            StateMachine.ChangeState(ChasingState);
        }
        
        else if (GroundTrigger.Count == 0)
        {
            StateMachine.ChangeState(IdleState);
        }
    }
}