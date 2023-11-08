using UnityEngine;

public class PlayerDashingState : PlayerState
{
    private float _timeToEnd;
    
    public PlayerDashingState(Player player) : base(player)
    {
    }

    public override void EnterState()
    {
        
        
        PlayerUtilities.SetGravityScale(player, player.Data.withoutGravity);

        _timeToEnd = 0;
        
        Dash();
    }

    public override void ExitState()
    {
        PlayerUtilities.SetGravityScale(player, player.Data.gravityUp);
    }

    public override void PhysicsUpdate()
    {
        _timeToEnd += Time.fixedDeltaTime;
        if (_timeToEnd > 0.5f && player.IsOnGround)
        {
            PlayerUtilities.SetNewVelocity(player, 0, 0);
            player.StateMachine.ChangeState(player.RunningState);
        }
        else if (_timeToEnd > 0.5f && !player.IsOnGround)
        {
            PlayerUtilities.SetNewVelocity(player, 0, 0);
            player.StateMachine.ChangeState(player.FallingState);
        }
    }

    public override void AnimationTriggerEvent()
    {
        
        
        //TODO dash animation
    }

    private void Dash()
    {
        if (player.DashCounter < player.Data.dashMax)
        {
            player.DashCounter++;
            PlayerUtilities.SetNewVelocity(player, 0, 0);
            player.Rigidbody2D.AddForce(new Vector2(player.Data.dashForce * player.LookDirection, 0f), ForceMode2D.Impulse);
        }
    }
}
