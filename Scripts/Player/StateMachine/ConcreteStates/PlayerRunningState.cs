using System;
using UnityEngine;

public class PlayerRunningState : PlayerState
{
    public PlayerRunningState(Player player) : base(player)
    {
    }

    public override void EnterState()
    {
        player.JumpCounter = 0;

        player.runParticle.Play();
    }

    public override void ExitState()
    {
        player.runParticle.Stop();
    }

    public override void PhysicsUpdate()
    {
        PlayerUtilities.CheckDirectionToFace(player, player.LookDirection > 0);

        #region Change State Input Region

        if (InputManager.JumpInput)
        {
            player.StateMachine.ChangeState(player.JumpingState);
        }

        if (InputManager.DashInput)
        {
            player.StateMachine.ChangeState(player.DashingState);
        }

        if (InputManager.SlideInput)
        {
            player.StateMachine.ChangeState(player.SlidingState);
        }

        #endregion

        if (player.LastTimeOnGround > 0.05)
        {
            player.StateMachine.ChangeState(player.FallingState);
            player.JumpCounter++;
        }

        // PlayerUtilities.InGroundAcceleration(player, player.Data.AccelerationVelocity, player.Data.DecelerationVelocity);
        
        var targetVelocity = player.MoveInput * player.Data.runMaxVelocity;
        var currentVelocity = player.Rigidbody2D.velocity;

        if (Math.Sign(currentVelocity.x) != Math.Sign(targetVelocity) && currentVelocity.x != 0) // sinais opostos
        {
            currentVelocity = Vector3.MoveTowards(currentVelocity, Vector3.zero, player.Data.runDeceleration * Time.deltaTime);
        }
        else
        {
            currentVelocity = Vector3.MoveTowards(currentVelocity, Vector3.right * targetVelocity, player.Data.runAcceleration * Time.deltaTime);
        }
        
        player.Rigidbody2D.velocity = currentVelocity;

        if (player.Rigidbody2D.velocity.x == 0 && player.MoveInput == 0)
        {
            player.StateMachine.ChangeState(player.IdleState);
        }
    }

    public override void AnimationTriggerEvent()
    {
        //TODO running animation
    }
}