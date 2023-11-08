using Unity.Mathematics;
using UnityEngine;

public class PlayerJumpingState : PlayerState
{
    public PlayerJumpingState(Player player) : base(player)
    {
    }

    public override void EnterState()
    {
        Jump();

        PlayerUtilities.SetGravityScale(player, player.Data.gravityFall);
        
        player.jumpParticle.Play();
    }

    public override void ExitState()
    {
        PlayerUtilities.SetGravityScale(player, player.Data.gravityUp);
    }

    public override void PhysicsUpdate()
    {
        PlayerUtilities.CheckDirectionToFace(player, player.LookDirection > 0);

        #region Change State Input Region

        if (InputManager.DashInput)
        {
            player.StateMachine.ChangeState(player.DashingState);
        }

        if (InputManager.SlideInput)
        {
            player.StateMachine.ChangeState(player.SlidingState);
        }

        #endregion

        if (player.Rigidbody2D.velocity.y < 0)
        {
            player.StateMachine.ChangeState(player.FallingState);
        }

        if (InputManager.JumpInput && player.LastTimeOnGround > 0.05f)
        {
            Jump();
        }

        if (InputManager.MoveInput)
        {
            PlayerUtilities.InAirAcceleration(player, player.Data.airAcceleration);
        }

        if (player.Rigidbody2D.velocity.y is < 1 and > 0)
        {
            PlayerUtilities.SetGravityScale(player, player.Data.gravityPeak);
        }
    }
    
    public override void AnimationTriggerEvent()
    {
        //TODO animation when initiating jump, when upping and when falling
    }

    private void Jump()
    {
        if (player.JumpCounter < player.Data.maxJump)
        {
            //If "jump" is pressed and the new input axis is different from previous input axis, do a "mortal" jump
            //If "jump" is pressed and the new input axis is equals from previous input axis, do a normal jump
            
            PlayerUtilities.SetGravityScale(player, player.Data.gravityUp);

            player.PlayerTransform.rotation = quaternion.identity;

            player.JumpCounter++;
            
            PlayerUtilities.SetNewVelocity(player, player.Rigidbody2D.velocity.x, 0);
            player.Rigidbody2D.AddForce(new Vector2(player.Data.jumpForce * 0.5f * player.MoveVariableInput, player.Data.jumpForce / player.JumpCounter), ForceMode2D.Impulse);
        }
    }
}