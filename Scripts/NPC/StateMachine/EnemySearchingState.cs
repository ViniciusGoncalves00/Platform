public class EnemySearchingState : SearchingState<Enemy>
{
    private readonly Enemy _enemy;

    public EnemySearchingState(Enemy enemy) : base(enemy)
    {
        _enemy = enemy;
    }

    public override void EnterState()
    {
        _enemy.EnterSearchState();
    }

    public override void ExitState()
    {
        _enemy.ExitSearchState();
    }

    public override void PhysicsUpdate()
    {
        _enemy.Search();
    }

    public override void AnimationTriggerEvent()
    {
    }
}