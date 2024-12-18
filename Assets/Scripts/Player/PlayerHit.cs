using System;
using UnityEngine;

public class PlayerHit : MonoBehaviour
{
    private bool isInvincible = false;
    private Rigidbody2D rb;


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void TakeDamage(Vector2 direction, float force, float invincibilityDuration)
    {
        rb.velocity = new Vector2(direction.x * force, direction.y * force);
        
        // Make the player temporarily invincible
        isInvincible = true;
        Invoke(nameof(ResetInvincibility), invincibilityDuration);
    }

    private void ResetInvincibility()
    {
        isInvincible = false;
    }
}