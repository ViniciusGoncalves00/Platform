public class PlayerStateMachine
{
    public PlayerState currentPlayerState { get; private set; }
    
    public void ChangeState(PlayerState newState)
    {
        currentPlayerState?.ExitState();
        currentPlayerState = newState;
        currentPlayerState?.EnterState();
    }
}