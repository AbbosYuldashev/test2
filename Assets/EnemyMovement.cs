using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float speed = 2f; // Speed of the enemy movement

    void Update()
    {
        // Move the enemy downward (negative y-axis)
        transform.position += Vector3.down * speed * Time.deltaTime;
    }

    // Optional: Destroy the enemy when it goes off-screen
    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
