using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        if (hitInfo.CompareTag("Enemy"))
        {
            // Destroy the enemy
            Destroy(hitInfo.gameObject);

            // Destroy the player
            Debug.Log("Player Died!");
            Destroy(gameObject);
        }
    }
}
