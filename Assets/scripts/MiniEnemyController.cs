using UnityEngine;

public class MiniEnemyController : MonoBehaviour
{
    public float moveSpeed = 2f; // Speed of the mini enemy
    public float lifeTime = 5f;  // Time before the enemy is destroyed (in seconds)

    private void Start()
    {
        // Destroy the enemy after 'lifeTime' seconds to avoid data consumption
        Destroy(gameObject, lifeTime);
    }

    private void Update()
    {
        // Move the enemy downwards along the y-axis
        transform.position += Vector3.down * moveSpeed * Time.deltaTime;

        // Optionally: Add shooting logic here
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bullet"))
        {
            Destroy(gameObject); // Destroy 
                                 
        }

    }


    // Call this method when the enemy is destroyed by other means (e.g., being hit by a bullet)
    public void DestroyEnemy()
    {
        Destroy(gameObject);
    }
}
