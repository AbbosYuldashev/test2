using System.Threading;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public static int count = 0;
    public float lifetime = 3f;
    public static int boss_hit = 0;// Time before the bullet is destroyed

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
            count++;
            Debug.Log(count);
        }
        else if (hitInfo.CompareTag("Boss"))
        {
           
            boss_hit++; // Call the boss's TakeDamage method
            Destroy(gameObject); // Destroy the bullet after hitting the boss
        }
    }
}
