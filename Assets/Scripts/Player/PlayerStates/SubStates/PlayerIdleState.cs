using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerGroundedState
{
    public PlayerIdleState(Player player, PlayerData playerData, string animBoolName) : base(player, playerData, animBoolName) {
    }
    public override void Enter() {
        base.Enter();
        Debug.Log("IdleState");
    }
    
    public override void Exit() {
        Debug.Log("Exit Idle");
        base.Exit();
    }
    public override void PhysicsUpdate() {
        base.PhysicsUpdate();
        
    }
    public override void LogicUpdate() {
        base.LogicUpdate();
        if (player.InputHandler.XInput != 0) {
            Debug.Log("Switch to MovingState");
            Debug.Log("StateMachine: " + stateMachine);
            Debug.Log("Desired State: " + player.MovingState);
            stateMachine.ChangeState(player.MovingState);
        }
    }
}
