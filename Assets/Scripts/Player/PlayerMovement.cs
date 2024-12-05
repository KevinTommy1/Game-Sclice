using System.Collections;
using Unity.Mathematics;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private PlayerState currentState = PlayerState.Idle;

    [Header("Dashing")]
    [SerializeField] private bool isDashing;
    [SerializeField] private bool canDash;
    [SerializeField] private bool canTakeDamage = true;
    [SerializeField] private float dashingTime = 0.5f;
    
    [Header("Movement")]
    [SerializeField] private float movementSpeed;
    


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
        if (isDashing) return;
        if (Input.GetAxisRaw("Horizontal") >= 1)
        {
            currentState = PlayerState.MovingRight;
        }
        else if (Input.GetAxisRaw("Horizontal") <= -1)
        {
            currentState = PlayerState.MovingLeft;
        }
        else
        {
            currentState = PlayerState.Idle;
        }

        if (Input.GetAxisRaw("Fire3") >= 1 && canDash && !isDashing)
        {
            // Perform dashing
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
                // Make dashing logic
                break;
            case PlayerState.Idle:
                rb.velocity = new Vector2(0, rb.velocity.y);
                break;
            default:
                rb.velocity = new Vector2(0, rb.velocity.y);
                break;
        }
    }
    
    
    // make dashing method
    public void Dash()
    {
        
    }

    private enum PlayerState
    {
        Idle,
        MovingLeft,
        MovingRight,
        FallingLeft,
        FallingRight,
        Jumping,
        Dashing
    }
}