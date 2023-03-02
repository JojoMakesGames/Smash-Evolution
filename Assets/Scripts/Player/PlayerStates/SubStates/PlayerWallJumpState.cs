using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallJumpState : PlayerAbilityState
{
    private Vector2 workspace;
    public PlayerWallJumpState(Player player, PlayerData playerData, string animBoolName) : base(player, playerData, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.InputHandler.UseJumpInput();
        player.JumpState.ResetAmountOfJumpsLeft();
        workspace = player.CurrentVelocity;
        workspace.y = 0;
        player.RB.velocity = workspace;
        workspace.y = Mathf.Sqrt(playerData.maxJumpHeight * -2f * Physics.gravity.y);
        workspace.x = Mathf.Sqrt(2f * playerData.maxJumpHeight * Mathf.Abs(Physics.gravity.y)) * -1 * player.InputHandler.XInput;
        player.RB.AddForce(workspace, ForceMode2D.Impulse);
        isAbilityDone = true;
        player.JumpState.DecreaseAmountOfJumpsLeft();
        player.InAirState.SetIsJumping();

    }
}
