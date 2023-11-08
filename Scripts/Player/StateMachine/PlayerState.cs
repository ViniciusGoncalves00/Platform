public abstract class PlayerState
{
    protected Player player;

    public PlayerState(Player player)
    {
        this.player = player;
    }
    public virtual void EnterState() { }
    public virtual void ExitState() { }
    public virtual void PhysicsUpdate() { }
    public virtual void AnimationTriggerEvent() { }
}
