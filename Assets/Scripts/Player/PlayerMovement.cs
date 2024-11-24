using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private float jumpStartPos = 9999f;
    private float elevationGained = 0f;
    private bool isJumping = false; 
    private bool jumpKeyDown = false;
    private bool jumpKeyUp = false;
    private bool facingRight = false;

    [SerializeField] private float jumpHeight;
    [SerializeField] private float size = 1;
    [SerializeField] private float maxSpeed = 10f;
    [SerializeField] private float jumpForce = 10f;
    
    private void Start()
    {
        if (!TryGetComponent(out rb))
        {
            Debug.LogError("No RigidBody2D on " + gameObject.name);
        }
    }

    private void Update()
    {
        if (!jumpKeyDown && Input.GetKeyDown(KeyCode.Space))
        {
            jumpKeyDown = true;
        }
        if (!jumpKeyUp && Input.GetKeyUp(KeyCode.Space))
        {
            jumpKeyUp = true;
        }
    }


    private void FixedUpdate()
    {
        if(rb == null) return;
        TurnCheck();
        Movement();
    }

    private void TurnCheck()
    {
        //if (Math.Abs(Input.GetAxis("Horizontal")) < 0.01f) return;
        if (Input.GetAxis("Horizontal") > 0.05f && !facingRight)
        {
            Turn();
        }
        else if (Input.GetAxis("Horizontal") < -0.05f && facingRight)
        {
            Turn();
        }
    }

    private void Turn()
    {
        print("Turn");
        if (facingRight)
        {
            transform.rotation = Quaternion.Euler(transform.rotation.x, 180f, transform.rotation.z);
            facingRight = false;
        }
        else
        {
            transform.rotation = Quaternion.Euler(transform.rotation.x, 0, transform.rotation.z);
            facingRight = true;
        }
    }

    //Input.GetButtonDown("Jump")
    /// <summary>
    /// Handles the player's movement.
    /// </summary>
    private void Movement()
    {
        Vector2 newVelocity;
        
        if (jumpKeyDown)
        {
            if (IsGrounded())
            {
                rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                jumpStartPos = transform.position.y;
                isJumping = true;
            }

            print("Jump");
            jumpKeyDown = false;
        }

        elevationGained = transform.position.y - jumpStartPos;
        if (elevationGained > jumpHeight || jumpKeyUp){
            print("Fall");
            print("Elevation gained: " + elevationGained);
            isJumping = false;
            jumpKeyUp = false;
        }
        
        
        float moveX = Input.GetAxis("Horizontal") * maxSpeed;
        newVelocity.x = moveX;
        newVelocity.y = isJumping? jumpForce : -jumpForce;
        rb.velocity = newVelocity;
    }
    
    /// <summary>àa
    /// Checks if the player is grounded.
    /// Does a raycast down from the player's position and checks if the ray hits a collider.
    /// Also checks if the player's vertical velocity is zero.
    /// </summary>
    private bool IsGrounded()
    {
        Debug.DrawRay( transform.position + Vector3.down * size, Vector2.down * 2f, Color.red);
        return Physics2D.Raycast(transform.position + Vector3.down * size, Vector2.down, 0.1f).collider != null && rb.velocity.y == 0;
    }
}
