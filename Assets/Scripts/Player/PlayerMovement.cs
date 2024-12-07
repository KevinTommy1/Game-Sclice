using System;
using System.Collections;
using Unity.Mathematics;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 5f; // Speed of player movement
    [SerializeField] private float jumpHeight = 5f; // Height of player jump
    [SerializeField] private float dashSpeed = 15f; // Speed of the dash
    [SerializeField] private float dashLength = 0.3f; // Duration of the dash in seconds
    [SerializeField] private bool isGrounded = false; // Tracks if the player is on the ground
    [SerializeField] private Rigidbody2D rb; // Player's Rigidbody2D component
    [SerializeField] private bool canTakeDamage = true; // Determines if the player can take damage

    private enum PlayerState
    {
        Idle,
        MovingLeft,
        MovingRight,
        Jumping,
        Dashing
    }

    private PlayerState currentState = PlayerState.Idle;

    private bool isDashing = false;
    private int lastDirection = 0;

    private void Start()
    {
        if (!TryGetComponent(out rb))
        {
            Debug.LogError("No RigidBody2D on " + gameObject.name);
        }
    }

    private void Update()
    {
        HandleMovement();
    }

    private void HandleMovement()
    {
        if (Input.GetAxisRaw("Horizontal") <= -1)
        {
            currentState = PlayerState.MovingLeft;
            lastDirection = -1;
        }
        else if (Input.GetAxisRaw("Horizontal") >= 1)
        {
            currentState = PlayerState.MovingRight;
            lastDirection = 1;
        }
        else if (Input.GetAxisRaw("Dash") >= 1 && !isDashing)
        {
            currentState = PlayerState.Dashing;
            canTakeDamage = false;
        }
        else
        {
            currentState = PlayerState.Idle;
        }

        if (Input.GetAxisRaw("Jump") >= 1 && isGrounded)
        {
            currentState = PlayerState.Jumping;
            rb.velocity = new Vector2(rb.velocity.x, jumpHeight);
        }

        HandleState();
    }

    private void HandleState()
    {
        switch (currentState)
        {
            case PlayerState.MovingLeft:
                rb.velocity = new Vector2(-movementSpeed, rb.velocity.y);
                break;

            case PlayerState.MovingRight:
                rb.velocity = new Vector2(movementSpeed, rb.velocity.y);
                break;

            case PlayerState.Jumping:
                rb.velocity = new Vector2(rb.velocity.x, jumpHeight);
                isGrounded = false;
                currentState = PlayerState.Idle; // Reset state after jump impulse
                break;
            case PlayerState.Dashing:
                StartCoroutine(PerformDash());
                break;
            case PlayerState.Idle:
                rb.velocity = new Vector2(0, rb.velocity.y);
                break;
        }
    }

    private IEnumerator PerformDash()
    {
        isDashing = true;
        canTakeDamage = false;

        // Lock Y axis to prevent falling while dashing
        rb.constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;

        // Lock movement and determine dash direction using the last direction
        float dashDirection = lastDirection;
        if (dashDirection != 0)
        {
            rb.velocity = new Vector2(dashDirection * dashSpeed, rb.velocity.y);
        }

        yield return new WaitForSeconds(dashLength);

        // stop movement and reset contraints
        rb.velocity = new Vector2(0, rb.velocity.y);
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;

        isDashing = false;
        canTakeDamage = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            if (!isDashing)
            {
                currentState = PlayerState.Idle;
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
}