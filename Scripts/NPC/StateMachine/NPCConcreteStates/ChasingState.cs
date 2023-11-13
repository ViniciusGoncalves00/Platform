public class ChasingState : NPCStates
{
    private readonly NPCBaseClass _npc;

    public ChasingState(NPCBaseClass npc)
    {
        _npc = npc;
    }

    public override void EnterState()
    {
        _npc.EnterChaseState();
    }

    public override void ExitState()
    {
        _npc.ExitChaseState();
    }

    public override void PhysicsUpdate()
    {
        _npc.Chase();
    }

    public override void AnimationTriggerEvent()
    {
    }
}