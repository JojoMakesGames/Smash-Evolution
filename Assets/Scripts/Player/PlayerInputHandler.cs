using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    public Vector2 RawMovementInput { get; private set; }
    public float XInput { get { return RawMovementInput.x; } }


    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnMove(InputAction.CallbackContext context) {
        RawMovementInput = context.ReadValue<Vector2>();
    }

    public void OnRoll(InputAction.CallbackContext context) {
        Debug.Log("Roll");
    }

    public void OnJump(InputAction.CallbackContext context) {
        Debug.Log("OnJump");
    }
}
