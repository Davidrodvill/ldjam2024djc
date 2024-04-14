using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float moveSpeed = 5f;       
    public float jumpForce = 5f;       
    public Rigidbody2D rb;              
    private bool isGrounded = true;     

    void Update()
    {
        MovePlayer();
        Jump();
    }

    void MovePlayer()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal"); //right and left movement
        rb.velocity = new Vector2(horizontalInput * moveSpeed, rb.velocity.y); // Set the horizontal velocity
    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.W) && isGrounded)
        {
            rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse); //jump
            isGrounded = false; 
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {//if touching ground, then true
        if (collision.contacts[0].normal.y > 0.5)
        {
            isGrounded = true;  
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {//if not touching ground, then not true
        if (collision.collider != null)
        {
            isGrounded = false;  
        }
    }
}
