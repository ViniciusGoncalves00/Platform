public abstract class NPCStates
{
    public virtual void EnterState() { }
    
    public virtual void ExitState() { }
    public virtual void PhysicsUpdate() { }
    public virtual void AnimationTriggerEvent() { }
}