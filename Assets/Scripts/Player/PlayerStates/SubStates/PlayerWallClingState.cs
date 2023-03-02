using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallClingState : PlayerState
{
    private Vector2 workspace;
    private float speedDiff;
    public PlayerWallClingState(Player player, PlayerData playerData, string animBoolName) : base(player, playerData, animBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
        Debug.Log("WallCling");
        player.JumpState.ResetAmountOfJumpsLeft();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (player.JumpState.CanJump) {
            stateMachine.ChangeState(player.WallJumpState);
        } else if (!player.IsTouchingWall || player.InputHandler.XInput == 0){
            if (player.IsGrounded) {
                stateMachine.ChangeState(player.IdleState);
            } else {
                stateMachine.ChangeState(player.InAirState);
            }
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        speedDiff = Mathf.Min(Mathf.Abs(player.CurrentVelocity.y), playerData.wallClingFriction);
        speedDiff *= Mathf.Sign(player.CurrentVelocity.y);
        player.RB.AddForce(Vector2.up * -speedDiff, ForceMode2D.Impulse);
        
    }

}
