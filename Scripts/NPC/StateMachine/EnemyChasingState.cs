public class EnemyChasingState : ChasingState<Enemy>
{
    private readonly Enemy _enemy;

    public EnemyChasingState(Enemy enemy) : base(enemy)
    {
        _enemy = enemy;
    }

    public override void EnterState()
    {
        _enemy.EnterChaseState();
    }

    public override void ExitState()
    {
        _enemy.ExitChaseState();
    }

    public override void PhysicsUpdate()
    {
        _enemy.Chase();
    }

    public override void AnimationTriggerEvent()
    {
    }
}