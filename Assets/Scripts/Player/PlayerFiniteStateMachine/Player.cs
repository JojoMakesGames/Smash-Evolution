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
    public Rigidbody2D RB { get; private set; }

    [SerializeField]
    private PlayerData playerData;

    public IdleState IdleState { get; private set; }
    public MovingState MovingState { get; private set; } 
    public SlidingStopState SlidingStopState { get; private set; }

    public bool IsGrounded { get  {
        foreach (LayerMask layer in WhatIsGround)
        {
            if (Physics2D.OverlapCircle(GroundCheck.position, GroundCheckRadius, layer))
            {
                return true;
            }
        }
        return false;
    } }

    void Start()
    {
        RB = GetComponent<Rigidbody2D>();
        StateMachine = new PlayerStateMachine();
        InputHandler = GetComponent<PlayerInputHandler>();
        IdleState = new IdleState(this, playerData, "idle");
        MovingState = new MovingState(this, playerData, "move");
        SlidingStopState = new SlidingStopState(this, playerData, "slideStop");
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
