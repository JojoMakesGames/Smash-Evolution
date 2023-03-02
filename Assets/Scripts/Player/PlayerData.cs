using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="newPlayerData", menuName ="Data/Player Data/Base Data")]
public class PlayerData : ScriptableObject
{
    [Header("Move State")]
    public float movementVelocity = 5f;
    public float maxSpeed = 10f;
    public float acceleration = 6f;
    public float friction = .2f;

    [Header("Jump State")]
    public float jumpVelocity = 15f;
    public float maxJumpHeight = 6f;
    public int amountOfJumps = 1;
    public float jumpCutMultiplier = .1f;

    [Header("Wall Jump State")]
    public float wallJumpVelocity = 20;
    public float wallJumpTime = 0.4f;
    public float wallClingFriction = 0.5f;
    public Vector2 wallJumpAngle = new Vector2(1, 2);

    [Header("In Air State")]
    public float coyoteTime = 0.2f;
    public float variableJumpHeightMultiplier = 0.5f;
    public float airMovementVelocity = 15f;
    public float fallMultiplier = 2.5f;
    public float gravityScale = 1f;
    public float maxAirSpeed = 10f;
    public float minJumpTime = 0.15f;

    [Header("Wall Slide State")]
    public float wallSlideVelocity = 3f;
    public float wallClingGravity = 0.5f;

    [Header("Wall Climb State")]
    public float wallClimbVelocity = 3f;

    [Header("Ledge Climb State")]
    public Vector2 startOffset;
    public Vector2 stopOffset;

    [Header("Dash State")]
    public float dashCooldown = 0.5f;
    public float maxHoldTime = 1f;
    public float holdTimeScale = 0.25f;
    public float dashTime = 0.2f;
    public float dashVelocity = 30f;
    public float drag = 10f;
    public float dashEndYMultiplier = 0.2f;
    public float distBetweenAfterImages = 0.5f;

    [Header("Crouch States")]
    public float crouchMovementVelocity = 5f;
    public float crouchColliderHeight = 0.8f;
    public float standColliderHeight = 1.6f;
}