using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    
    [SerializeField] private float maxSpeed = 10f;
    
    private void Start()
    {
        if (!TryGetComponent(out rb))
        {
            Debug.Log("No RigidBody2D on " + gameObject.name);
        }
    }
    
    private void FixedUpdate()
    {
        if(rb == null) return;
        Movement();
    }


    /// <summary>
    /// Handles the player's movement.
    /// </summary>
    private void Movement()
    {
        float moveX = Input.GetAxis("Horizontal") * maxSpeed;
        
        Vector2 newVelocity;
        newVelocity.x = moveX;
        newVelocity.y = rb.velocity.y;
        rb.velocity = newVelocity;
    }
    
    /// <summary>
    /// Checks if the player is grounded.
    /// Does a raycast down from the player's position and checks if the ray hits a collider.
    /// Also checks if the player's vertical velocity is zero.
    /// </summary>
    private bool IsGrounded()
    {
        return Physics2D.Raycast(transform.position, Vector2.down, 0.1f).collider != null && rb.velocity.y == 0;
    }
}
