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

    public float speed = 5f;
    [SerializeField]
    private Rigidbody2D rb;

    private Vector3 moveAmount;

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
        rb.AddForce(moveAmount * speed, ForceMode2D.Impulse);
    }

    void LateUpdate() {
        if(controller.isGrounded) {
            Vector3 clampedMagnitude = Vector3.ClampMagnitude(rb.velocity, maxSpeed);
            rb.velocity = new Vector3(clampedMagnitude.x, rb.velocity.y, 0);
        } else {
            Vector3 clampedMagnitude = Vector3.ClampMagnitude(rb.velocity, maxAirSpeed);
            rb.velocity = new Vector3(clampedMagnitude.x, rb.velocity.y, 0);
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        switch(context.phase) {
            case InputActionPhase.Started:
                break;
            case InputActionPhase.Performed:
                Vector2 readMove = context.ReadValue<Vector2>();
                moveAmount = new Vector3(readMove.x, 0, 0);
                break;
            case InputActionPhase.Canceled:
                moveAmount = Vector3.zero;
                break;
            default:
                break;
        }
    }


    public void OnJump(InputAction.CallbackContext context)
    {
        switch (context.phase)
        {
            case InputActionPhase.Started:
                break;
            case InputActionPhase.Performed:
                if(controller.isGrounded) {
                    rb.AddForce(Vector3.up * Mathf.Sqrt(maxJumpHeight * -2f * Physics.gravity.y), ForceMode2D.Impulse);
                }
                break;
            case InputActionPhase.Canceled:
                if(controller.isGrounded) {
                    rb.AddForce(Vector3.up * Mathf.Sqrt(minJumpHeight * -2f * Physics.gravity.y), ForceMode2D.Impulse);
                }
                break;
            default:
                break;
        }
    }

    public void OnRoll(InputAction.CallbackContext context)
    {
        float x = rb.velocity.x;
        if(x != 0) {
            rb.AddForce(moveAmount * 10, ForceMode2D.Impulse);
        }
    }
}
