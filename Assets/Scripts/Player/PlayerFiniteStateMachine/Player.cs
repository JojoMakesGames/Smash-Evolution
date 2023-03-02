using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerStateMachine StateMachine { get; private set; }
    public PlayerInputHandler InputHandler { get; private set; }
    public List<LayerMask> WhatIsGround;
    public Transform GroundCheck;
    public float GroundCheckRadius;
    public List<LayerMask> WhatIsWall;
    public Transform LeftWallCheck;
    public Transform ReftWallCheck;
    public float WallCheckRadius;
    public Rigidbody2D RB { get; private set; }

    [SerializeField]
    private PlayerData playerData;

    public PlayerIdleState IdleState { get; private set; }
    public PlayerMovingState MovingState { get; private set; } 
    public PlayerSlidingStopState SlidingStopState { get; private set; }
    public PlayerInAirState InAirState { get; private set; }
    public PlayerJumpState JumpState { get; private set; }

    public bool IsGrounded { get  {
        foreach (LayerMask layer in WhatIsGround)
        {
            if(Physics2D.OverlapCircle(GroundCheck.position, GroundCheckRadius, layer)) {
                return true;
            }
        }
        return false;
    } }
    public bool TouchingLeftWall { get {
        foreach(LayerMask wall in WhatIsWall)
        {
            if(Physics2D.OverlapCircle(LeftWallCheck.position, WallCheckRadius, wall)) {
                return true;
            }
        }
        return false;
    } }
    public bool TouchingRightWall { get {
        foreach(LayerMask wall in WhatIsWall)
        {
            if(Physics2D.OverlapCircle(ReftWallCheck.position, WallCheckRadius, wall)) {
                return true;
            }
        }
        return false;
    } }
    public Vector2 CurrentVelocity { get { return RB.velocity; }}


    void Start()
    {
        RB = GetComponent<Rigidbody2D>();
        StateMachine = new PlayerStateMachine();
        InputHandler = GetComponent<PlayerInputHandler>();
        IdleState = new PlayerIdleState(this, playerData, "idle");
        MovingState = new PlayerMovingState(this, playerData, "move");
        SlidingStopState = new PlayerSlidingStopState(this, playerData, "slideStop");
        InAirState = new PlayerInAirState(this, playerData, "inAir");
        JumpState = new PlayerJumpState(this, playerData, "jump");
        StateMachine.Initialize(IdleState);
    }

    void Update()
    {
        Debug.Log("StateMachine: " + StateMachine);
        Debug.Log("CurrentState: " + StateMachine.CurrentState);
        StateMachine.CurrentState.LogicUpdate();
    }

    void FixedUpdate()
    {
        StateMachine.CurrentState.PhysicsUpdate();
    }

}
