public class AttackingState : NPCStates
{
    private readonly NPCBaseClass _npc;

    public AttackingState(NPCBaseClass npc)
    {
        _npc = npc;
    }

    public override void EnterState()
    {
        _npc.EnterAttackState();
    }

    public override void ExitState()
    {
        _npc.ExitAttackState();
    }

    public override void PhysicsUpdate()
    {
        _npc.Attack();
    }

    public override void AnimationTriggerEvent()
    {
    }
}