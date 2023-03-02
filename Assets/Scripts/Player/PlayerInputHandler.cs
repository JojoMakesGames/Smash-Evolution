using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    public Vector2 RawMovementInput { get; private set; }
    public float XInput { get { return RawMovementInput.x; } }
    public float NormalizedXInput { get { return RawMovementInput.normalized.x; }}
    public bool JumpingInput { get; private set; }
    public bool IsJumping { get; private set; }
    public bool JumpInputStop { get; private set; }
    private float lastJumpInputTime;


    // Update is called once per frame
    void Update()
    {
        if (Time.time >= lastJumpInputTime + 0.2f) {
            JumpingInput = false;
        }
    }

    public void OnMove(InputAction.CallbackContext context) {
        RawMovementInput = context.ReadValue<Vector2>();
    }

    public void OnRoll(InputAction.CallbackContext context) {
    }

    public void OnJump(InputAction.CallbackContext context) {
        if (context.performed) {
            lastJumpInputTime = Time.time;
            JumpingInput = true;
            IsJumping = true;
            JumpInputStop = false;
        }
        if (context.canceled) {
            JumpInputStop = true;
        }
    }

    public void UseJumpInput() {
        JumpingInput = false;
    }

}
