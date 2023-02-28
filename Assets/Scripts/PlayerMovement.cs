using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float maxJumpHeight = 3f;
    public float minJumpHeight = 3f;
    public float maxSpeed = 5f;
    public float maxAirSpeed;
    public float gravityScaleMultiplier;
    public float acceleration;
    public float jumpCutMultiplier;


    public float speed = 5f;
    [SerializeField]
    private Rigidbody2D rb;

    private Vector2 moveAmount;

    public InputActionAsset actions;
    public PlayerController controller;
    private InputAction moveAction;
    

    public void Awake()
    {
        var playerActions = actions.FindActionMap("player");
        playerActions.FindAction("jump").started += OnJump;
        playerActions.FindAction("jump").performed += OnJump;
        playerActions.FindAction("jump").canceled += OnJump;

        moveAction = playerActions.FindAction("move");
        moveAction.performed += OnMove;
        moveAction.canceled += OnMove;
        
        playerActions.FindAction("roll").performed += OnRoll;
    }

    public void OnEnable()
    {
        actions.FindActionMap("player").Enable();
    }

    public void OnDisable()
    {
        actions.FindActionMap("player").Disable();
    }

    void FixedUpdate()
    {
        float targetSpeed = moveAmount.x * maxSpeed;
        float speedDiff = targetSpeed - rb.velocity.x;
        float movement = Mathf.Pow(Math.Abs(speedDiff) * acceleration, 0.9f) * Mathf.Sign(speedDiff);
        if (rb.velocity.y < 0) {
            rb.gravityScale = gravityScaleMultiplier;
        }
        
        rb.AddForce(Vector2.right * movement);
        if(controller.isGrounded && !moveAction.inProgress){
            float amount = Mathf.Min(Mathf.Abs(rb.velocity.x), .2f);
            amount *= Mathf.Sign(rb.velocity.x);
            rb.AddForce(Vector2.right * -amount, ForceMode2D.Impulse);
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        Vector2 readMove = context.ReadValue<Vector2>();
        moveAmount = new Vector2(readMove.x, 0);
    }


    public void OnJump(InputAction.CallbackContext context)
    {
        switch (context.phase)
        {
            case InputActionPhase.Performed:
                if(controller.isGrounded) {
                    rb.AddForce(Vector2.up * Mathf.Sqrt(maxJumpHeight * -2f * Physics.gravity.y), ForceMode2D.Impulse);
                }
                break;
            case InputActionPhase.Canceled:
                rb.AddForce(Vector2.down * rb.velocity.y * (1 - jumpCutMultiplier), ForceMode2D.Impulse);
                break;
            default:
                break;
        }
    }

    public void OnRoll(InputAction.CallbackContext context)
    {
        // float x = rb.velocity.x;
        // if(x != 0) {
        //     rb.AddForce(moveAmount * 10, ForceMode2D.Impulse);
        // }
    }
}
