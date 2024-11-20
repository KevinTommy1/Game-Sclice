using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDashing : MonoBehaviour
{
    private Rigidbody2D rb;
    private bool isDashing = false;
    private Vector2 dashDirection;

    [SerializeField] private float dashDistance = 5f;
    [SerializeField] private float dashDuration = 0.5f;

    private void Start()
    {
        if (!TryGetComponent(out rb))
        {
            Debug.LogError("Rigidbody2D not found on " + gameObject.name);
        }
    }

    private void FixedUpdate()
    {
        if (rb == null) return;
        Dash();
    }

    /// <summary>
    /// Initiates a dash movement when the Left Shift key is pressed. 
    /// Sets the dash direction based on current horizontal and vertical input 
    /// and starts the dash coroutine if not already dashing.
    /// </summary>
    private void Dash()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && !isDashing)
        {
            isDashing = true;
            dashDirection = new Vector2(Input.GetAxis("Horizontal"), 0f);
            StartCoroutine(DashCoroutine());
        }
    }

    IEnumerator DashCoroutine()
    {
        float elapsedTime = 0f;

        while (elapsedTime < dashDuration)
        {
            float t = Time.deltaTime / dashDuration;

            rb.transform.position = Vector2.Lerp(rb.transform.position, dashDirection * dashDistance, t);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        isDashing = false;
    }
}