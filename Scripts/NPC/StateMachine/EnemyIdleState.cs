public class EnemyIdleState : IdleState<Enemy>
{
    private readonly Enemy _enemy;

    public EnemyIdleState(Enemy enemy) : base(enemy)
    {
        _enemy = enemy;
    }

    public override void EnterState()
    {
        _enemy.EnterIdleState();
    }

    public override void ExitState()
    {
        _enemy.ExitIdleState();
    }

    public override void PhysicsUpdate()
    {
        _enemy.Idle();
    }

    public override void AnimationTriggerEvent()
    {
    }
}