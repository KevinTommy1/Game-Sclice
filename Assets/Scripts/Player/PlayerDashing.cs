using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDashing : MonoBehaviour
{
    private Rigidbody2D rb;
    private PlayerMovement playerMovement;
    
    [Header("Dashing")] 
    [SerializeField] private float dashingVelocity = 14f;
    [SerializeField] private float dashDistance = 2f;
    [SerializeField] private float dashingTime = 0.5f;
    
    private Vector2 dashingDirection;
    private bool isDashing;
    private bool canDash = true;

    private void Start()
    {
        if (!TryGetComponent(out rb))
        {
            Debug.LogError("Rigidbody2D not found on " + gameObject.name);
        }
        playerMovement = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        var inputX = Input.GetAxisRaw("Horizontal");
        var dashInput = Input.GetButtonDown("Dash");

        if (dashInput && canDash)
        {
            isDashing = true;
            canDash = false;
            dashingDirection = new Vector2(inputX, Input.GetAxisRaw("Vertical"));
            if (dashingDirection == Vector2.zero)
            {
                dashingDirection = new Vector2(transform.localScale.x, 0);
            }
            StartCoroutine(StopDashing());
        }

        if (isDashing)
        {
            rb.velocity = dashingDirection.normalized * dashingVelocity;
            return;
        }

        if (playerMovement.IsGrounded())
        {
            canDash = true;
        }
    }

    private IEnumerator StopDashing()
    {
        yield return new WaitForSeconds(dashingTime);
        isDashing = false;
    }
    


}