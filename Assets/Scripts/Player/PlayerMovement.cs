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
    
    private enum PlayerState { Idle, MovingLeft, MovingRight, Jumping, Dashing }
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
        if (Input.GetAxisRaw("Horizontal") >= 1)
        {
            currentState = PlayerState.MovingRight;
            lastDirection = 1;
        }
        else if (Input.GetAxisRaw("Horizontal") <= -1)
        {
            currentState = PlayerState.MovingLeft;
            lastDirection = -1;
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
            case PlayerState.Dashing:
                rb.velocity = new Vector2(dashSpeed * lastDirection, rb.velocity.y);
                isDashing = true;
                StartCoroutine(DashTimer());
                break;
            case PlayerState.Idle:
            default:
                rb.velocity = new Vector2(0, rb.velocity.y);
                break;
        }
    }
    
    private IEnumerator DashTimer()
    {
        yield return new WaitForSeconds(dashLength);
        isDashing = false;
        canTakeDamage = true;
    }
    
}