using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInAirState : PlayerState
{
    private bool isJumping;
    private bool jumpInputStop { get { return player.InputHandler.JumpInputStop; } }
    private bool isGrounded;
    private float targetSpeed;
    private float speedDiff;
    private float movement;
    private float minJumpTime;

    public PlayerInAirState(Player player, PlayerData playerData, string animBoolName) : base(player, playerData, animBoolName)
    {
    }
    
    public override void DoChecks()
    {
        base.DoChecks();
        isGrounded = player.IsGrounded;
    }

    public override void LogicUpdate() {
        base.LogicUpdate();
        if (isGrounded && player.CurrentVelocity.y < 0.01f) {
            stateMachine.ChangeState(player.IdleState);
        } else if (player.InputHandler.JumpingInput) {
            stateMachine.ChangeState(player.JumpState);
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
            if (jumpInputStop && playerData.minJumpTime < Time.time - startTime)
            {
                player.RB.AddForce(Vector2.down * player.RB.velocity.y * (1 - playerData.jumpCutMultiplier), ForceMode2D.Impulse);
                isJumping = false;
            }
            else if (player.CurrentVelocity.y <= 0f)
            {
                isJumping = false;
            }
        }
    }

    public void SetIsJumping() => isJumping = true;


}
