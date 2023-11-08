public class SearchingState : NPCStates
{
    private readonly NPCBaseClass _npc;

    public SearchingState(NPCBaseClass npc) : base(npc)
    {
        _npc = npc;
    }

    public override void EnterState()
    {
        _npc.EnterSearchState();
    }

    public override void ExitState()
    {
        _npc.ExitSearchState();
    }

    public override void PhysicsUpdate()
    {
        _npc.Search();
    }

    public override void AnimationTriggerEvent()
    {
    }
}