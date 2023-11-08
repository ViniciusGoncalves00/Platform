using UnityEngine;

public class PlayerFallingState : PlayerState
{
    public PlayerFallingState(Player player) : base(player)
    {
    }

    public override void EnterState()
    {
        PlayerUtilities.SetGravityScale(player, player.Data.gravityFall);
        
        if (player.LastTimeOnGround is > 0.05f and < 0.1f)
        {
            player.JumpCounter++;
        }
    }

    public override void ExitState()
    {
        PlayerUtilities.SetGravityScale(player, player.Data.gravityUp);

        if (player.LastTimeOnGround == 0)
        {
            player.groundParticle.Play();
        }
    }

    public override void PhysicsUpdate()
    {
        PlayerUtilities.CheckDirectionToFace(player, player.LookDirection > 0);

        if (Mathf.Abs(player.Rigidbody2D.velocity.y) > Mathf.Abs(player.Data.fallMaxVelocity))
        {
            player.Rigidbody2D.velocity = new Vector2(player.Rigidbody2D.velocity.x , -player.Data.fallMaxVelocity);
        }

        if (player.IsOnGround)
        {
            player.StateMachine.ChangeState(player.RunningState);
        }

        if (InputManager.DashInput)
        {
            player.StateMachine.ChangeState(player.DashingState);
        }

        if (InputManager.JumpInput)
        {
            player.StateMachine.ChangeState(player.JumpingState);
        }

        if (InputManager.MoveInput)
        {
            PlayerUtilities.InAirAcceleration(player, player.Data.airAcceleration);
        }
    }

    public override void AnimationTriggerEvent()
    {
    }
}