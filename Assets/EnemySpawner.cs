using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab; // Reference to the enemy prefab
    public float spawnRate = 2f; // Time between spawns
    public float spawnRangeX = 8f; // The horizontal range for enemy spawning

    private float nextSpawnTime = 0f;

    void Update()
    {
        // Check if it's time to spawn a new enemy
        if (Time.time >= nextSpawnTime)
        {
            SpawnEnemy();
            nextSpawnTime = Time.time + spawnRate; // Reset spawn timer
        }
    }

    void SpawnEnemy()
    {
        // Generate a random position within the X range at the top of the screen
        Vector3 spawnPosition = new Vector3(Random.Range(-spawnRangeX, spawnRangeX), 6f, 0f);

        // Instantiate an enemy at the random position
        Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
    }
}
