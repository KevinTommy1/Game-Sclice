using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int health = 5;
    [SerializeField] private bool isInvincible = false;

    private Rigidbody2D rb;

    
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    
    private void AddHealth(int amount)
    {
        health += amount;
    }

    private void ResetInvincibility()
    {
        isInvincible = false;
    }
    
    public void TakeDamage(int damage, Vector2 direction, float force, float invincibilityDuration)
    {
        health -= damage;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
        // Check the direction of the force
        if (direction.x < 0)
        {
            // Apply force to the left
            rb.AddForce(new Vector2(-force, 0), ForceMode2D.Impulse);
        }
        else if (direction.x > 0)
        {
            // Apply force to the right
            rb.AddForce(new Vector2(force, 0), ForceMode2D.Impulse);
        }

        if (direction.y < 0)
        {
            // Apply force downwards
            rb.AddForce(new Vector2(0, -force), ForceMode2D.Impulse);
        }
        else if (direction.y > 0)
        {
            // Apply force upwards
            rb.AddForce(new Vector2(0, force), ForceMode2D.Impulse);
        }

        // Make the player temporarily invincible
        isInvincible = true;
        Invoke(nameof(ResetInvincibility), invincibilityDuration);
    }

    public void GroundSpikeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}