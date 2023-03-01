using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlidingStopState : GroundedState
{
    private float frictionAmount;
    public SlidingStopState(Player player, PlayerData playerData, string animBoolName) : base(player, playerData, animBoolName) {
    }
    public override void Enter() {
        base.Enter();
        Debug.Log("SlidingStopState");
    }
    
    public override void Exit() {
        base.Exit();
    }
    public override void PhysicsUpdate() {
        base.PhysicsUpdate();
        frictionAmount = Mathf.Min(Mathf.Abs(player.RB.velocity.x), playerData.friction);
        frictionAmount *= Mathf.Sign(player.RB.velocity.x);
        player.RB.AddForce(Vector2.right * -frictionAmount, ForceMode2D.Impulse);
    }
    public override void LogicUpdate() {
        base.LogicUpdate();
        if (player.RB.velocity.x == 0) {
            stateMachine.ChangeState(player.IdleState);
        } else if(player.InputHandler.XInput != 0) {
            stateMachine.ChangeState(player.MovingState);
        }
    }
}
