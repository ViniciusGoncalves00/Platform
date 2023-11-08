using UnityEngine;

public class EnemyDefault : Enemy
{
    private Vector2 _target;
    private float _timer; //remove when doing attack logic
 

    public override void EnterAttackState()
    {
        _timer = 0;
    }
    
    public override void EnterSearchState()
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

        if (Vector2.Distance(Rigidbody2D.position, _target) < 0.1f)
        {
            RandomIndex = Random.Range(0, waypoints.Length);
        }
        
        _target = new Vector2(waypoints[RandomIndex].x, Rigidbody2D.position.y);
        
        Rigidbody2D.position = Vector3.MoveTowards(Rigidbody2D.position, _target, velocity * Time.deltaTime);
    }

    public override void Attack()
    {
        _timer += Time.deltaTime;
        
        if (_timer > 1)
        {
            StateMachine.ChangeState(ChasingState);
        }
    }

    public override void Chase()
    {
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

        var target = Vector3.MoveTowards(Rigidbody2D.position, Player.Rigidbody2D.position, velocity * Time.deltaTime);
        Rigidbody2D.position = new Vector2(target.x, Rigidbody2D.position.y);
    }
}