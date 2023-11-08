public abstract class NPCStates
{
    protected NPCBaseClass Npc;

    protected NPCStates(NPCBaseClass npc)
    {
        Npc = npc;
    }
    public virtual void EnterState() { }
    public virtual void EnterAttackState() { }
    
    public virtual void ExitState() { }
    public virtual void PhysicsUpdate() { }
    public virtual void AnimationTriggerEvent() { }
}
