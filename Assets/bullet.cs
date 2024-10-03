using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float lifetime = 3f; // Time before the bullet is destroyed

    void Start()
    {
        // Destroy the bullet after a certain time if it doesn't hit anything
        Destroy(gameObject, lifetime);
    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        if (hitInfo.CompareTag("Enemy"))
        {
            // Destroy the enemy and the bullet on impact
            Destroy(hitInfo.gameObject);
            Destroy(gameObject);
        }
    }
}
