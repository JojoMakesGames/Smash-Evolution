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
    public float frictionAmount;
    public int doubleJumps;

    public bool wallCling;

    [SerializeField]
    private Rigidbody2D rb;

    public bool isGrounded;
    public Transform groundCheck;
    public Transform leftWallCheck;
    public Transform rightWallCheck;
    public bool isLeftWall;
    public bool isRightWall;
    public float wallRadius = 0.2f;
    public float groundRadius = 0.2f;
    public List<LayerMask> whatIsGround;
    public List<LayerMask> whatIsWall;

    public float speed = 5f;
    public float wallClingGravity;

    private Vector2 moveAmount;

    public InputActionAsset actions;
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
        foreach(LayerMask ground in whatIsGround)
        {
            isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, ground);
            if (isGrounded)
            {
                rb.gravityScale = 1;
                break;
            }
        }
        #region WallCling
        foreach(LayerMask wall in whatIsWall)
        {
            isLeftWall = Physics2D.OverlapCircle(leftWallCheck.position, wallRadius, wall);
            isRightWall = Physics2D.OverlapCircle(rightWallCheck.position, wallRadius, wall);
            if (isLeftWall || isRightWall)
            {
                break;
            }
        }
        if (isLeftWall && !isGrounded && moveAmount.x < 0)
        {
            rb.AddForce(Vector2.up * rb.velocity.y);
            rb.gravityScale = wallClingGravity;
            doubleJumps = 1;
            wallCling = true;
        }
        else if (isRightWall && !isGrounded && moveAmount.x > 0)
        {
            rb.AddForce(Vector2.up * rb.velocity.y);
            rb.gravityScale = .05f;
            doubleJumps = 1;
            wallCling = true;
        }
        else
        {
            rb.gravityScale = 1;
            wallCling = false;
        }
        #endregion


        if (isGrounded) {
            doubleJumps = 1;
        }
        float targetSpeed = moveAmount.x * maxSpeed;
        float speedDiff = targetSpeed - rb.velocity.x;
        float movement = Mathf.Pow(Math.Abs(speedDiff) * acceleration, 0.9f) * Mathf.Sign(speedDiff);
        if (rb.velocity.y < 0 && !wallCling) {
            gameObject.layer = (int) Layers.Player;
            rb.gravityScale = gravityScaleMultiplier;
        }
        
        rb.AddForce(Vector2.right * movement);
        if(isGrounded && !moveAction.inProgress){
            float amount = Mathf.Min(Mathf.Abs(rb.velocity.x), frictionAmount);
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
                if(isGrounded) {
                    gameObject.layer = (int) Layers.PassThroughPlatform;
                    rb.AddForce(Vector2.up * Mathf.Sqrt(maxJumpHeight * -2f * Physics.gravity.y), ForceMode2D.Impulse);
                } else if (wallCling) {
                    if (isLeftWall) {
                        rb.AddForce(Vector2.right * Mathf.Sqrt(maxJumpHeight * -2f * Physics.gravity.y), ForceMode2D.Impulse);
                    } else if (isRightWall) {
                        rb.AddForce(Vector2.left * Mathf.Sqrt(maxJumpHeight * -2f * Physics.gravity.y), ForceMode2D.Impulse);
                    }
                    rb.AddForce(Vector2.up * Mathf.Sqrt(maxJumpHeight * -2f * Physics.gravity.y), ForceMode2D.Impulse);
                } else if (doubleJumps > 0) {
                    rb.velocity = new Vector2(rb.velocity.x, 0);
                    rb.AddForce(Vector2.up * Mathf.Sqrt(maxJumpHeight * -2f * Physics.gravity.y), ForceMode2D.Impulse);
                    doubleJumps--;
                } 
                break;
            case InputActionPhase.Canceled:
                if (rb.velocity.y > 0) {
                    rb.AddForce(Vector2.down * rb.velocity.y * (1 - jumpCutMultiplier), ForceMode2D.Impulse);
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
