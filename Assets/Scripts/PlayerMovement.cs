using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerMovement : MonoBehaviour
{
    public float jumpHeight = 3f;
    public float speed = 1f;
    public bool canJump = false;
    public bool jumping = false;

    // Update is called once per frame
    void Update()
    {
        // Get the input from the player
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        // Get the player's z velocity
        float zVelocity = GetComponent<Rigidbody2D>().velocity.y;
        if (zVelocity > 0 && jumping)
        {
            gameObject.layer = (int) Layers.PassTrhoughPlatform;
        } else
        {
            gameObject.layer = (int) Layers.Player;
        }

        // Move the player
        Vector3 move = transform.right * x + transform.up * z;
        transform.position += move * speed * Time.deltaTime;

        // Jump
        if (canJump && Input.GetButtonDown("Jump") && Math.Abs(GetComponent<Rigidbody2D>().velocity.y) < 0.001f)
        {
            jumping = true;
            GetComponent<Rigidbody2D>().AddForce(Vector3.up * Mathf.Sqrt(jumpHeight * -2f * Physics.gravity.y), ForceMode2D.Impulse);
        }

        // Roll
        if (Input.GetButtonDown("Shield"))
        {
            Debug.Log("Shield");
            if(x != 0) {
                GetComponent<Rigidbody2D>().AddForce(move * 10, ForceMode2D.Impulse);
            }
        }
    }

    void OnCollisionEnter2D(Collision2D other) 
     {
        if (jumping) {
            jumping = false;
        }
        canJump = true;
     }
 
    void OnCollisionExit2D(Collision2D other) 
    {
        if (gameObject.layer == (int) Layers.Player)
        {
            canJump = false;
        }
    }

}
