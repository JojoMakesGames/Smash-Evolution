using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : PlayerAbilityState
{
    private int amountOfJumpsLeft;
    public bool canJump { get { return amountOfJumpsLeft > 0; }}
    private Vector2 workspace;
    public PlayerJumpState(Player player, PlayerData playerData, string animBoolName) : base(player, playerData, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.InputHandler.UseJumpInput();
        workspace = player.CurrentVelocity;
        workspace.y = 0;
        player.RB.velocity = workspace;
        player.RB.AddForce(Vector2.up * Mathf.Sqrt(playerData.maxJumpHeight * -2f * Physics.gravity.y), ForceMode2D.Impulse);
        isAbilityDone = true;
        amountOfJumpsLeft--;
        player.InAirState.SetIsJumping();
    }

    public void ResetAmountOfJumpsLeft() => amountOfJumpsLeft = playerData.amountOfJumps;
}
