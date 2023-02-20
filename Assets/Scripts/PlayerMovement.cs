using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerMovement : MonoBehaviour
{
    public float jumpHeight = 3f;
    public float speed = 1f;
    public bool canJump = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Get the input from the player
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        // Move the player
        Vector3 move = transform.right * x + transform.up * z;
        transform.position += move * speed * Time.deltaTime;

        // Jump
        if (canJump && Input.GetButtonDown("Jump") && Math.Abs(GetComponent<Rigidbody2D>().velocity.y) < 0.001f)
        {
            Debug.Log("Jump");
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
        Debug.Log("Collision");
         canJump = true;
     }
 
    void OnCollisionExit2D(Collision2D other) 
    {
        Debug.Log("Collision Exit");
        canJump = false;
    }

}
