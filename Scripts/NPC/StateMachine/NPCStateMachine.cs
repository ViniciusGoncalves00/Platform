public class NPCStateMachine
{
    public NPCStates CurrentNpcStates { get; private set; }
    
    public void ChangeState(NPCStates newState)
    {
        CurrentNpcStates?.ExitState();
        CurrentNpcStates = newState;
        CurrentNpcStates?.EnterState();
    }
}