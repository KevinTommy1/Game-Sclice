using UnityEngine;

public class Health : MonoBehaviour
{
    private int health;

    
    private void AddHealth(int amount)
    {
        health += amount;
    }
    
    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}