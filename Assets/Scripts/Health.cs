using System;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    [SerializeField] private int health = 5;
    [SerializeField] private bool isInvincible = false;
    [SerializeField] private Image healthBar;
    [SerializeField] private Sprite[] healthSprites = new Sprite[5];
    [SerializeField] private GameObject spawnPoint;
    private Rigidbody2D rb;
    private GameObject player;


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindWithTag("Player");
    }


    private void Update()
    {
        if (health <= 0)
        {
            player.transform.position = spawnPoint.transform.position;
            AddHealth(5);
        }
    }

    private void ResetInvincibility()
    {
        isInvincible = false;
    }

    public void TakeDamage(int damage, Vector2 direction, float force, float invincibilityDuration)
    {
        health -= damage;

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

    public void AddHealth(int amount)
    {
        health += amount;
        ImageChanger();
    }

    public void GroundSpikeDamage(int damage)
    {
        health -= damage;
        ImageChanger();
    }

    private void ImageChanger()
    {
        Image healthBarSpriteRenderer = healthBar.GetComponent<Image>();

        // Change the sprite based on the current health value
        if (health >= 0 && health <= 5)
        {
            healthBarSpriteRenderer.sprite = healthSprites[health];
        }
    }
}