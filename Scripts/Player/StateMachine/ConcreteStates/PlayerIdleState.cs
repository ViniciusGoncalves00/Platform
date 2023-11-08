using UnityEngine;

public class PlayerIdleState : PlayerState
{
    public PlayerIdleState(Player player) : base(player)
    {
    }

    public override void EnterState()
    {
        player.Rigidbody2D.bodyType = RigidbodyType2D.Static;
        
        player.JumpCounter = 0;
        player.DashCounter = 0;
    }

    public override void ExitState()
    {
        player.Rigidbody2D.bodyType = RigidbodyType2D.Dynamic;
    }

    public override void PhysicsUpdate()
    {
        PlayerUtilities.CheckDirectionToFace(player, player.LookDirection > 0);

        //player.Rigidbody2D.velocity = new Vector2(0, 0);
        //player.Rigidbody2D.angularVelocity = 0;

        #region Change State Input Region

        if (InputManager.MoveInput)
        {
            player.StateMachine.ChangeState(player.RunningState);
        }

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
    }

    public override void AnimationTriggerEvent()
    {
        //TODO player swings
    }
}