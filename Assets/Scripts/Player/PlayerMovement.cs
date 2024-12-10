using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 5f; // Speed of player movement
    [SerializeField] private float jumpHeight = 5f; // Height of player jump
    [SerializeField] private bool isGrounded = false; // Tracks if the player is on the ground
    [SerializeField] private Rigidbody2D rb; // Player's Rigidbody2D component
    [SerializeField] private bool canTakeDamage = true; // Determines if the player can take damage

    private enum PlayerState
    {
        Idle,
        MovingLeft,
        MovingRight,
        Jumping,
    }

    // Current state of the player
    [SerializeField] private PlayerState currentState = PlayerState.Idle;

    [SerializeField] private bool isDashing = false;
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
        HandleInput();
    }


    private void HandleInput()
    {
        var moveLeft = Input.GetAxisRaw("Horizontal") <= -1;
        var moveRight = Input.GetAxisRaw("Horizontal") >= 1;
        var jumpInput = Input.GetAxisRaw("Jump") >= 1;
        
        HandleMovement(moveLeft, moveRight, jumpInput);
    }

    private void HandleMovement(bool moveLeft, bool moveRight, bool jumpInput)
    {
        if (moveLeft)
        {
            currentState = PlayerState.MovingLeft;
            lastDirection = -1;
        }
        else if(moveRight)
        {
            currentState = PlayerState.MovingRight;
            lastDirection = 1;
        }
        else
        {
            currentState = PlayerState.Idle;
        }
        if (jumpInput && isGrounded)
        {
            currentState = PlayerState.Jumping;
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
            case PlayerState.Idle:
                rb.velocity = new Vector2(0, rb.velocity.y);
                break;
        }
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