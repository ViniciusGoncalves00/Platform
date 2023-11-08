public abstract class NPCStates<T>
{
    protected T Npc;

    protected NPCStates(T npc)
    {
        Npc = npc;
    }
    public virtual void EnterState() { }
    public virtual void ExitState() { }
    public virtual void PhysicsUpdate() { }
    public virtual void AnimationTriggerEvent() { }
}
