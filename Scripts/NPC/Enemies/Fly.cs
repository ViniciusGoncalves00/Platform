public class Fly : FlyEnemy
{
    public override void EnterIdleState()
    {
        base.EnterIdleState();
    }

    public override void ExitIdleState()
    {
        base.ExitIdleState();
    }

    public override void EnterSearchState()
    {
        base.EnterSearchState();
    }

    public override void ExitSearchState()
    {
        base.ExitSearchState();
    }

    public override void EnterChaseState()
    {
        base.EnterChaseState();
    }

    public override void ExitChaseState()
    {
        base.ExitChaseState();
    }

    public override void EnterAttackState()
    {
        base.EnterAttackState();
    }

    public override void ExitAttackState()
    {
        base.ExitAttackState();
    }

    public override void Idle()
    {
        base.Idle();
    }

    public override void Search()
    {
        base.Search();

        SquareSearch();
    }

    public override void Chase()
    {
        base.Chase();

        SquareChase();
    }

    public override void Attack()
    {
        base.Attack();
        
        if (TouchTrigger.Count != 0)
        {
            Player.TakeDamage(npcSo.damageType1);
        }
    }

    private protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
    }
}
