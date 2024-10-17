using UnityEngine;

public class PowerUpSpawner : MonoBehaviour
{
    public GameObject[] powerUpPrefabs; // Array of power-up prefabs
    public float spawnInterval = 10f; // Time between spawns
    public float minX = -8f; // Minimum X position for spawning
    public float maxX = 8f;  // Maximum X position for spawning
    public float spawnY = 6f; // Y position where power-ups will spawn
    public float fallSpeed = 2f; // Speed at which the power-ups fall

    private void Start()
    {
        InvokeRepeating(nameof(SpawnPowerUp), spawnInterval, spawnInterval);
    }

    private void SpawnPowerUp()
    {
        // Generate a random X position within the specified range
        float randomX = Random.Range(minX, maxX);

        // Set the spawn position with random X and fixed Y
        Vector3 spawnPosition = new Vector3(randomX, spawnY, 0);

        // Instantiate a random power-up at the random position
        int randomIndex = Random.Range(0, powerUpPrefabs.Length);
        GameObject spawnedPowerUp = Instantiate(powerUpPrefabs[randomIndex], spawnPosition, Quaternion.identity);

        // Add a downward movement to the power-up
        Rigidbody2D rb = spawnedPowerUp.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = new Vector2(0, -fallSpeed);
        }
        else
        {
            Debug.LogWarning("Rigidbody2D not found on the power-up prefab.");
        }
    }
}
