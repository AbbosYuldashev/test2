using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int health = 3; // Number of hits before the enemy is destroyed

    public void TakeDamage(int damage)
    {
        health -= damage; // Reduce health by the amount of damage

        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        // Destroy the enemy when its health reaches zero
        Destroy(gameObject);
    }
}
