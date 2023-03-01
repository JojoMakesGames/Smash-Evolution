using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState
{
    protected Player player;
    protected PlayerStateMachine stateMachine { get { return player.StateMachine; } }
    protected PlayerData playerData { get; private set;}
    protected float startTime;
    private string animBoolName;

    public PlayerState(Player player, PlayerData playerData, string animBoolName) {
        this.player = player;
        this.animBoolName = animBoolName;
        this.playerData = playerData;
    }

    public virtual void Enter(){
        startTime = Time.time;
    }
    public virtual void Exit() {
        Debug.Log("Exit");
    }
    public virtual void PhysicsUpdate() {
    }
    public virtual void LogicUpdate() {
    }

    public virtual void DoChecks() { }
}
