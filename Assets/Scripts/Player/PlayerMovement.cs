using System.Collections;
using Unity.Mathematics;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private PlayerState currentState = PlayerState.Idle;

    [Header("Dashing")] [SerializeField] private bool isDashing;
    [SerializeField] private bool canDash;
    [SerializeField] private float dashingTime = 0.5f;
    [SerializeField] private float dashingVelocity = 10f;
    [SerializeField] private Vector2 dashingDirection;
    [SerializeField] private bool canTakeDamage = true;

    [Header("Movement")] [SerializeField] private float movementSpeed;


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
            dashingDirection = Vector2.right;
        }
        else if (Input.GetAxisRaw("Horizontal") <= -1)
        {
            currentState = PlayerState.MovingLeft;
            dashingDirection = Vector2.left;
        }
        else if (Input.GetAxisRaw("Dash") >= 1 && canDash && !isDashing)
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
                rb.velocity = new Vector2(dashingVelocity * dashingDirection.x, rb.velocity.y);
                canDash = false;
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
        yield return new WaitForSeconds(dashingTime);
        canDash = true;
        isDashing = false;
        canTakeDamage = true;
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