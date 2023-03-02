using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundedState : PlayerState
{
    protected int xInput;
    protected int yInput;

    protected bool isTouchingCeiling;

    private bool JumpInput;
    private bool grabInput;
    private bool isGrounded;
    public PlayerGroundedState(Player player, PlayerData playerData, string animBoolName) : base(player, playerData, animBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
        isGrounded = player.IsGrounded;
    }

    public override void Enter()
    {
        base.Enter();
        
        Debug.Log("Entered Grounded State");
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if(player.InputHandler.JumpingInput) {
            stateMachine.ChangeState(player.JumpState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
