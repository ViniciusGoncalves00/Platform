public class IdleState : NPCStates
{
    private readonly NPCBaseClass _npc;

    public IdleState(NPCBaseClass npc)
    {
        _npc = npc;
    }

    public override void EnterState()
    {
        _npc.EnterIdleState();
    }

    public override void ExitState()
    {
        _npc.ExitIdleState();
    }

    public override void PhysicsUpdate()
    {
        _npc.Idle();
    }

    public override void AnimationTriggerEvent()
    {
    }
}