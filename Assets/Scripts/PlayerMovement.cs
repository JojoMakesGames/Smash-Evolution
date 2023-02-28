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
    public bool stopping;

    public float speed = 5f;
    [SerializeField]
    private Rigidbody2D rb;

    private Vector2 moveAmount;

    public InputActionAsset actions;
    public PlayerController controller;

    public void Awake()
    {
        var playerActions = actions.FindActionMap("player");
        playerActions.FindAction("jump").started += OnJump;
        playerActions.FindAction("jump").performed += OnJump;
        playerActions.FindAction("jump").canceled += OnJump;

        playerActions.FindAction("move").performed += OnMove;
        playerActions.FindAction("move").canceled += OnMove;
        
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
        if (stopping && rb.velocity.x != 0)
        {
            rb.AddForce(new Vector2(-1 * rb.velocity.x, 0), ForceMode2D.Force);
        }
        if(rb.velocity.x <= 1) {
            stopping = false;
        }
        
        rb.AddForce(moveAmount * speed, ForceMode2D.Force);
    }

    void LateUpdate() {
        rb.velocity = new Vector2(Mathf.Clamp(rb.velocity.x, -maxSpeed, maxSpeed), Mathf.Clamp(rb.velocity.y, -maxSpeed, maxSpeed));
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        stopping = false;
        Vector2 readMove = context.ReadValue<Vector2>();
        moveAmount = new Vector2(readMove.x, 0);
        if (context.phase == InputActionPhase.Canceled)
        {
            stopping = true;
        }
    }


    public void OnJump(InputAction.CallbackContext context)
    {
        Debug.Log(context.phase);
        if(controller.isGrounded) {
            Vector2 jumpforce = new Vector2(0, Mathf.Sqrt(maxJumpHeight * -2f * Physics.gravity.y));
            Debug.Log(jumpforce);
            rb.AddForce(jumpforce, ForceMode2D.Impulse);
        }
        // switch (context.phase)
        // {
        //     case InputActionPhase.Started:
        //         break;
        //     case InputActionPhase.Performed:
        //         if(controller.isGrounded) {
        //             rb.AddForce(Vector2.up * Mathf.Sqrt(maxJumpHeight * -2f * Physics.gravity.y), ForceMode2D.Impulse);
        //         }
        //         break;
        //     case InputActionPhase.Canceled:
        //         if(controller.isGrounded) {
        //             rb.AddForce(Vector2.up * Mathf.Sqrt(minJumpHeight * -2f * Physics.gravity.y), ForceMode2D.Impulse);
        //         }
        //         break;
        //     default:
        //         break;
        // }
    }

    public void OnRoll(InputAction.CallbackContext context)
    {
        // float x = rb.velocity.x;
        // if(x != 0) {
        //     rb.AddForce(moveAmount * 10, ForceMode2D.Impulse);
        // }
    }
}
