using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int health = 5;
    
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