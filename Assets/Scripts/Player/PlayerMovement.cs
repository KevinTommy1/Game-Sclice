using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")] 
    [SerializeField] private float moveSpeed = 7.5f;
    [SerializeField] private float maxFallSpeed = 20f;

    [Header("Jump")] 
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private float jumpTime = 0.5f;

    [Header("Turn Check")] 
    [SerializeField] private GameObject lLeg;
    [SerializeField] private GameObject rLeg;

    [Header("Ground Check")] 
    [SerializeField] private float extraHeight = 0.25f;
    [SerializeField] private LayerMask whatIsGround;
    
    [Header("Particles")]
    [SerializeField] private ParticleSystem movementParticles;
    [SerializeField] private ParticleSystem landParticles;

    public bool IsFacingRight { get; set; }
    private Rigidbody2D rb;
    private Collider2D coll;
    private Animator anim;
    private float moveInput;
    private bool isJumping;
    [SerializeField]private bool isFalling;
    private float jumpTimeCounter;
    private RaycastHit2D groundHit;
    private Coroutine resetTriggerCoroutine;
    private float fallSpeedYDampingChangeThreshold;

    #region Unity Callback Functions

    private void Awake()
    {
        StartDirectionCheck();
    }


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        coll = GetComponent<Collider2D>();
         fallSpeedYDampingChangeThreshold = 
             CameraManager.instance._fallSpeedYDampingChangeThreshold;
    }

    private void Update()
    {
        Move();
        Jump();

         //if we are falling past a certain speed threshold
         if (rb.velocity.y < fallSpeedYDampingChangeThreshold && !CameraManager.instance.IsLerpingYDamping && !CameraManager.instance.LerpedFromPlayerFalling)
         {
             CameraManager.instance.LerpYDamping(true);
         }
         //if we are standing still or moving up
         if (rb.velocity.y >= 0f && !CameraManager.instance.IsLerpingYDamping && CameraManager.instance.LerpedFromPlayerFalling)
         {
             //reset so it can be called again
             CameraManager.instance.LerpedFromPlayerFalling = false;
             CameraManager.instance.LerpYDamping(false);
         }
    }

    private void FixedUpdate()
    {
        //clamp the player's fall speed in the Y (I set a super high upper limit to ensure we can have a fast jump speed if we want)
        rb.velocity = new Vector3(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -maxFallSpeed, maxFallSpeed * 10));
        if (moveInput > 0 || moveInput < 0)
        {
            TurnCheck();
        }
    }

    #endregion

    #region Movement Functions

    private void Move()
    {
        moveInput = UserInput.instance.moveInput.x;
        if (moveInput > 0 || moveInput < 0)
        {
            //anim.SetBool("isWalking", true);
            HandleTrigger("Walk");
            movementParticles.Play();
        }
        else
        {
            //anim.SetBool("isWalking", false);
            HandleTrigger("Idle");
            movementParticles.Stop();
        }
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);
    }

    private void Jump()
    {
        //button was pushed this frame
        if (UserInput.instance.controls.Jumping.Jump.WasPressedThisFrame() && IsGrounded())
        {
            isJumping = true;
            jumpTimeCounter = jumpTime;
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            //anim.SetTrigger("jump");
            HandleTrigger("Jump");
        }

        //button is being held
        if (UserInput.instance.controls.Jumping.Jump.IsPressed())
        {
            if (jumpTimeCounter > 0 && isJumping)
            {
                float jumpPercentage = 1 - (jumpTimeCounter / jumpTime);
                rb.velocity = new Vector2(rb.velocity.x, jumpForce * (1 - jumpPercentage));
                
                jumpTimeCounter -= Time.deltaTime;
            }
            else if (jumpTimeCounter == 0)
            {
                isJumping = false;
                isFalling = true;
            }
            else
            {
                isJumping = false;
            }
        }

        //button was released this frame
        if (UserInput.instance.controls.Jumping.Jump.WasReleasedThisFrame())
        {
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Min(rb.velocity.y, jumpForce / 2));
            isJumping = false;
            isFalling = true;
        }

        if (!isJumping && CheckForLand())
        {
            //anim.SetTrigger("land");
            HandleTrigger("Land");
            resetTriggerCoroutine = StartCoroutine(Reset());
        }
        DrawGroundCheck();
    }

    #endregion

    #region Ground/Landed Check

    private bool IsGrounded()
    {
        groundHit = Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, extraHeight,
            whatIsGround);
        if (groundHit.collider != null)
        {
            anim.SetBool("IsGrounded", true);
            return true;
        }
        else
        {
            anim.SetBool("IsGrounded", false);
            return false;
        }
    }

    private bool CheckForLand()
    {
        if (isFalling)
        {
            if (IsGrounded())
            {
                //player has landed
                landParticles.Play();
                isFalling = false;
                return true;
            }
            return false;
        }
        return false;
    }

    private IEnumerator Reset()
    {
        yield return null;
        //anim.ResetTrigger("land");
    }

    #endregion

    #region Turn Checks

    private void StartDirectionCheck()
    {
        if (rLeg.transform.position.x > lLeg.transform.position.x)
        {
            IsFacingRight = true;
        }
        else
        {
            IsFacingRight = false;
        }
    }

    private void TurnCheck()
    {
        if (UserInput.instance.moveInput.x > 0 && !IsFacingRight)
        {
            Turn();
        }

        else if (UserInput.instance.moveInput.x < 0 && IsFacingRight)
        {
            Turn();
        }
    }

    private void Turn()
    {
        if (IsFacingRight)
        {
            Vector3 rotator = new Vector3(transform.rotation.x, 0f, transform.rotation.z);
            transform.rotation = Quaternion.Euler(rotator);
            IsFacingRight = !IsFacingRight;
            //turn the camera follow object
            CameraManager.instance.CallCameraFaceDirection();
        }
        else
        {
            Vector3 rotator = new Vector3(transform.rotation.x, 180f, transform.rotation.z);
            transform.rotation = Quaternion.Euler(rotator);
            IsFacingRight = !IsFacingRight;
            //turn the camera follow object
            CameraManager.instance.CallCameraFaceDirection();
        }
    }

    #endregion

    #region Debug Functions

    private void DrawGroundCheck()
    {
        Color rayColor;
        if (IsGrounded())
        {
            rayColor = Color.green;
        }
        else
        {
            rayColor = Color.red;
        }
        Debug.DrawRay(coll.bounds.center + new Vector3(coll.bounds.extents.x, 0),
            Vector2.down * (coll.bounds.extents.y + extraHeight), rayColor);
        Debug.DrawRay(coll.bounds.center - new Vector3(coll.bounds.extents.x, 0),
            Vector2.down * (coll.bounds.extents.y + extraHeight), rayColor);
        Debug.DrawRay(coll.bounds.center - new Vector3(coll.bounds.extents.x, coll.bounds.extents.y + extraHeight),
            Vector2.right * (coll.bounds.extents.x * 2), rayColor);
    }

    #endregion

    #region animations triggers

    public void HandleTrigger(string trigger)
    {
        anim.ResetTrigger("Idle");
        anim.ResetTrigger("Walk");
        anim.ResetTrigger("Land");       
        anim.ResetTrigger("Jump");
        anim.ResetTrigger("Turn");
        
        anim.SetTrigger(trigger);
    }

    #endregion
}