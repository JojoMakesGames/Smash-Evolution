using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInAirState : PlayerState
{
    private bool isJumping;
    private bool jumpInputStop { get { return player.InputHandler.JumpInputStop; } }
    private float targetSpeed;
    private float speedDiff;
    private float movement;
    private float minJumpTime;
    private bool cutJump;

    public PlayerInAirState(Player player, PlayerData playerData, string animBoolName) : base(player, playerData, animBoolName)
    {
    }
    
    public override void LogicUpdate() {
        base.LogicUpdate();
        if (player.IsGrounded && player.CurrentVelocity.y < 0.01f) {
            stateMachine.ChangeState(player.IdleState);
        } else if (player.JumpState.CanJump) {
            stateMachine.ChangeState(player.JumpState);
        } else if (player.IsTouchingLeftWall && player.InputHandler.XInput > 0) {
            stateMachine.ChangeState(player.WallClingState);
        } else if (player.IsTouchingRightWall && player.InputHandler.XInput < 0) {
            stateMachine.ChangeState(player.WallClingState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        CheckJumpMultiplier();
        targetSpeed = playerData.maxAirSpeed * player.InputHandler.XInput;
        speedDiff = targetSpeed - player.RB.velocity.x;
        movement = Mathf.Pow(Mathf.Abs(speedDiff) * playerData.acceleration, 0.9f) * Mathf.Sign(speedDiff);
        player.RB.AddForce(Vector2.right * movement);  
        if(player.CurrentVelocity.y < 0) {
            player.RB.gravityScale = playerData.fallMultiplier;
        }
    }

    public override void Exit()
    {
        base.Exit();
        player.RB.gravityScale = playerData.gravityScale;
    }

    private void CheckJumpMultiplier()
    {
        if (isJumping)
        {
            if (!cutJump) {
                cutJump = jumpInputStop && Time.time - startTime < playerData.minJumpTime  ;
            }
            if (cutJump && playerData.minJumpTime < Time.time - startTime)
            {
                player.RB.AddForce(Vector2.down * player.RB.velocity.y * (1 - playerData.jumpCutMultiplier), ForceMode2D.Impulse);
                isJumping = false;
                cutJump = false;
            }
            else if (player.CurrentVelocity.y <= 0f)
            {
                isJumping = false;
                cutJump = false;
            }
        }
    }

    public void SetIsJumping() => isJumping = true;


}
