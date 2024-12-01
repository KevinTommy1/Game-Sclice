using System.Collections;
using Unity.Mathematics;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    
    [Header("Animation")] 
    [SerializeField] private Animator anim;
    private static readonly int Idle = Animator.StringToHash("Idle");
    //private static readonly int Dash = Animator.StringToHash("Dash");
    private static readonly int Jump = Animator.StringToHash("Jump");
    //private static readonly int Fall = Animator.StringToHash("Fall");
    private static readonly int WalkLeft = Animator.StringToHash("WalkLeft");
    
    private static readonly int WalkRight = Animator.StringToHash("WalkRight");
    
    [Header("Jumping & speed")]
    private float jumpStartPos = 9999f;
    private float elevationGained = 0f;
    private bool isJumping = false;
    private bool jumpKeyDown = false;
    private bool jumpKeyUp = false;

    [SerializeField] private float jumpHeight;
    [SerializeField] private float size = 1;
    [SerializeField] private float maxSpeed = 10f;
    [SerializeField] private float jumpForce = 10f;

    [Header("Dashing")] 
    [SerializeField] private float dashingVelocity = 14f;
    [SerializeField] private float dashDistance = 2f;
    [SerializeField] private float dashingTime = 0.5f;

    private Vector2 lastMovementDirection = new(1, 0);
    private Vector2 dashingDirection;
    private bool isDashing;
    private bool canDash = true;
    private bool isMoving = false;


    private void Start()
    {
        if (!TryGetComponent(out rb))
        {
            Debug.LogError("No RigidBody2D on " + gameObject.name);
        }

        if (!TryGetComponent(out anim))
        {
            Debug.LogError("No Animator on " + gameObject.name);
        }
    }

    private void Update()
    {
        Movement();
        if (!jumpKeyDown && Input.GetKeyDown(KeyCode.Space))
        {
            jumpKeyDown = true;
        }

        if (!jumpKeyUp && Input.GetKeyUp(KeyCode.Space))
        {
            jumpKeyUp = true;
        }

        var dashInput = Input.GetButtonDown("Dash");

        if (!dashInput || !canDash) return;
        isDashing = true;
        canDash = false;
        dashingDirection = new Vector2(Input.GetAxisRaw("Horizontal"), 0);
        //anim.SetTrigger(Dash);
        if (dashingDirection == Vector2.zero)
        {
            dashingDirection = lastMovementDirection;
        }

        StartCoroutine(StopDashing());
    }

    private void FixedUpdate()
    {
        if (rb == null) return;
    }

    private void Movement()
    {
        if (jumpKeyDown && IsGrounded())
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            jumpStartPos = transform.position.y;
            isJumping = true;
            //anim.SetTrigger(Jump);
            
            jumpKeyDown = false;
        }

        elevationGained = transform.position.y - jumpStartPos;
        if (elevationGained > jumpHeight || jumpKeyUp)
        {
            isJumping = false;
            jumpKeyUp = false;
            //anim.SetTrigger(Fall);
        }

        if (isDashing)
        {
            rb.velocity = dashingDirection.normalized * dashingVelocity;
            isJumping = false;
        }
        else
        {
            float moveX = Input.GetAxis("Horizontal") * maxSpeed;
            if (math.abs(moveX) < 0.8f) moveX = 0;
            Vector2 newVelocity;
            newVelocity.x = moveX;
            newVelocity.y = isJumping ? jumpForce : -jumpForce;
            rb.velocity = newVelocity;
            if (moveX > 0)
            {
                anim.SetTrigger(WalkRight);
            } else if (moveX < 0)
            {
                anim.SetTrigger(WalkLeft);
            }
            else
            {
                anim.SetTrigger(Idle);
            }
            // 

            if (moveX != 0)
            {
                lastMovementDirection = new Vector2(moveX, 0);
            }
        }

        if (IsGrounded())
        {
            canDash = true;
        }
    }

    private IEnumerator StopDashing()
    {
        yield return new WaitForSeconds(dashingTime);
        isDashing = false;
    }

    private bool IsGrounded()
    {
        Debug.DrawRay(transform.position + Vector3.down * size, Vector2.down * 2f, Color.red);
        return Physics2D.Raycast(transform.position + Vector3.down * size, Vector2.down, 0.1f).collider != null &&
               rb.velocity.y == 0;
    }
}