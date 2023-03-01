using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovingState : PlayerGroundedState
{
    private float targetSpeed;
    private float speedDiff;
    private float movement;
    private float frictionAmount;
    public PlayerMovingState(Player player, PlayerData playerData, string animBoolName) : base(player, playerData, animBoolName) {
    }
    public override void Enter() {
        base.Enter();
        Debug.Log("MovingState");
    }
    
    public override void PhysicsUpdate() {
        base.PhysicsUpdate();
        targetSpeed = playerData.maxSpeed * player.InputHandler.XInput;
        speedDiff = targetSpeed - player.RB.velocity.x;
        movement = Mathf.Pow(Mathf.Abs(speedDiff) * playerData.acceleration, 0.9f) * Mathf.Sign(speedDiff);
        player.RB.AddForce(Vector2.right * movement);       
    }
    public override void LogicUpdate() {
        base.LogicUpdate();
        if (player.InputHandler.XInput == 0) {
            stateMachine.ChangeState(player.SlidingStopState);
        }
    }
}
