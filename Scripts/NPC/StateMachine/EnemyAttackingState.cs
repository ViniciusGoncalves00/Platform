public class EnemyAttackingState : AttackingState<Enemy>
{
    private readonly Enemy _enemy;

    public EnemyAttackingState(Enemy enemy) : base(enemy)
    {
        _enemy = enemy;
    }

    public override void EnterState()
    {
        _enemy.EnterAttackState();
    }

    public override void ExitState()
    {
        _enemy.ExitAttackState();
    }

    public override void PhysicsUpdate()
    {
        _enemy.Attack();
    }

    public override void AnimationTriggerEvent()
    {
    }
}