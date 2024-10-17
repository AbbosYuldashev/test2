using UnityEngine;

public class MiniEnemy : MonoBehaviour
{
    public GameObject destroyedEnemyPrefab; // Reference to the destroyed enemy prefab
    public float destructionDelay = 1f;// Time to wait before destroying the destruction prefab

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the enemy is hit by a bullet
        if (other.CompareTag("Bullet2"))
        {
            
            //Debug.Log("mini enemy hit!");

            // Get the position of the mini enemy when it is destroyed
            Vector3 miniEnemyPosition = transform.position;
            Quaternion minienemyrotation = transform.rotation;

            // Instantiate the destruction prefab at the mini enemy's position
            GameObject destructionEffect = Instantiate(destroyedEnemyPrefab, miniEnemyPosition, minienemyrotation);

            // Log the position of the destruction effect

            // Destroy the mini enemy
            Destroy(gameObject);

            // Destroy the destruction prefab after a delay
            Destroy(destructionEffect, 1);
        }
    }
}
