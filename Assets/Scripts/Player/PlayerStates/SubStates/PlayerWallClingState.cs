using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallClingState : PlayerState
{
    public PlayerWallClingState(Player player, PlayerData playerData, string animBoolName) : base(player, playerData, animBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();

    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (player.InputHandler.XInput == 0) {
            stateMachine.ChangeState(player.IdleState);
        }
    }

}
