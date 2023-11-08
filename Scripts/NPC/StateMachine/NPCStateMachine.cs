public class NPCStateMachine<T>
{
    public NPCStates<T> CurrentNpcStates { get; private set; }
    
    public void ChangeState(NPCStates<T> newState)
    {
        CurrentNpcStates?.ExitState();
        CurrentNpcStates = newState;
        CurrentNpcStates?.EnterState();
    }
}