using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerStateMachine StateMachine { get; private set; }
    public PlayerInputHandler InputHandler { get; private set; }
    public bool FacingRight { get; private set; }
    public List<LayerMask> WhatIsGround;
    public Transform GroundCheck;
    public float GroundCheckRadius;
    public List<LayerMask> WhatIsWall;
    public Transform LeftWallCheck;
    public Transform ReftWallCheck;
    public float WallCheckRadius;
    public Rigidbody2D RB { get; private set; }
    public Animator Anim { get; private set; }

    [SerializeField]
    private PlayerData playerData;

    public PlayerIdleState IdleState { get; private set; }
    public PlayerMovingState MovingState { get; private set; } 
    public PlayerSlidingStopState SlidingStopState { get; private set; }
    public PlayerInAirState InAirState { get; private set; }
    public PlayerJumpState JumpState { get; private set; }
    public PlayerWallClingState WallClingState { get; private set; }
    public PlayerWallJumpState WallJumpState { get; private set; }

    public bool IsGrounded { get  {
        foreach (LayerMask layer in WhatIsGround)
        {
            if(Physics2D.OverlapCircle(GroundCheck.position, GroundCheckRadius, layer)) {
                return true;
            }
        }
        return false;
    } }
    public bool IsTouchingLeftWall { get {
        foreach(LayerMask wall in WhatIsWall)
        {
            if(Physics2D.OverlapCircle(LeftWallCheck.position, WallCheckRadius, wall)) {
                return true;
            }
        }
        return false;
    } }
    public bool IsTouchingRightWall { get {
        foreach(LayerMask wall in WhatIsWall)
        {
            if(Physics2D.OverlapCircle(ReftWallCheck.position, WallCheckRadius, wall)) {
                return true;
            }
        }
        return false;
    } }
    public bool IsTouchingWall { get { return IsTouchingLeftWall || IsTouchingRightWall; } }
    public Vector2 CurrentVelocity { get { return RB.velocity; }}


    void Start()
    {
        FacingRight = true;
        RB = GetComponent<Rigidbody2D>();
        Anim = GetComponent<Animator>();
        StateMachine = new PlayerStateMachine();
        InputHandler = GetComponent<PlayerInputHandler>();
        IdleState = new PlayerIdleState(this, playerData, "idle");
        MovingState = new PlayerMovingState(this, playerData, "move");
        SlidingStopState = new PlayerSlidingStopState(this, playerData, "slideStop");
        InAirState = new PlayerInAirState(this, playerData, "inAir");
        JumpState = new PlayerJumpState(this, playerData, "jump");
        WallClingState = new PlayerWallClingState(this, playerData, "wallCling");
        WallJumpState = new PlayerWallJumpState(this, playerData, "wallJump");
        StateMachine.Initialize(InAirState);
    }

    void Update()
    {
        StateMachine.CurrentState.LogicUpdate();
    }

    void FixedUpdate()
    {
        StateMachine.CurrentState.PhysicsUpdate();
    }

    public void Flip()
    {
        FacingRight = !FacingRight;
        transform.Rotate(0f, 180f, 0f);
    }

}
