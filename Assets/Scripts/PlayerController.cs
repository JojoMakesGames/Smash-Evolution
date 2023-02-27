using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    public bool wallCling = false;

    [HideInInspector]
    public bool jumping = false;
    [SerializeField]
    private Rigidbody2D rb;

    public bool isGrounded = false;
    public Transform groundCheck;
    public Transform leftWallCheck;
    public Transform rightWallCheck;
    public bool isLeftWall;
    public bool isRightWall;
    public float wallRadius = 0.2f;
    public float groundRadius = 0.2f;
    public List<LayerMask> whatIsGround;
    public List<LayerMask> whatIsWall;
    public InputActionAsset actions;


     public void Awake()
    {
        var playerActions = actions.FindActionMap("player");
        playerActions.FindAction("jump").started += OnJump;
        playerActions.FindAction("jump").performed += OnJump;
        playerActions.FindAction("jump").canceled += OnJump;
    }

    void FixedUpdate()
    {
        foreach(LayerMask ground in whatIsGround)
        {
            isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, ground);
            if (isGrounded)
            {
                break;
            }
        }
        foreach(LayerMask wall in whatIsWall)
        {
            isLeftWall = Physics2D.OverlapCircle(leftWallCheck.position, wallRadius, wall);
            isRightWall = Physics2D.OverlapCircle(rightWallCheck.position, wallRadius, wall);
            if (isLeftWall || isRightWall)
            {
                break;
            }
        }
        if (isLeftWall || isRightWall)
        {

            Debug.Log("Wall");
            wallCling = true;
        }
        else
        {
            wallCling = false;
        }
        
        float yVelocity = rb.velocity.y;

        if (yVelocity <= 0)
        {
            gameObject.layer = (int) Layers.Player;
        } 

    }

    public void OnJump(InputAction.CallbackContext context)
    {
        switch (context.phase)
        {
            case InputActionPhase.Performed:
            case InputActionPhase.Canceled:
                if(isGrounded) {
                    gameObject.layer = (int) Layers.PassThroughPlatform;
                }
                break;
        }
    }
}
