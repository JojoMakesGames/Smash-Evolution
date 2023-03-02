using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerGroundedState
{
    public PlayerIdleState(Player player, PlayerData playerData, string animBoolName) : base(player, playerData, animBoolName) {
    }

    public override void PhysicsUpdate() {
        base.PhysicsUpdate();
        
    }
    public override void LogicUpdate() {
        base.LogicUpdate();
        if (player.InputHandler.XInput != 0) {
            stateMachine.ChangeState(player.MovingState);
        }
    }
}
