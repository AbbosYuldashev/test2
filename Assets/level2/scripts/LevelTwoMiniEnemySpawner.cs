using UnityEngine;
using System.Collections;

public class LevelTwoMiniEnemySpawner : MonoBehaviour
{
    public GameObject miniEnemyPrefab;      // Reference to the mini enemy prefab
    public GameObject bossPrefab;           // Reference to the boss prefab
    public AnimationCurve enemyCurve;        // The curve that defines enemy movement (snake-like pattern)
    public float initialSpawnInterval = 2f; // Initial time between spawns
    public float minSpawnInterval = 0.5f;   // Minimum spawn interval (the fastest it can get)
    public float spawnSpeedIncreaseRate = 0.05f; // How much the spawn speed increases after each wave
    public int numberOfEnemies = 5;          // Number of mini enemies to spawn at once
    public float curveDuration = 5f;         // Time it takes for an enemy to move along the curve
    public float topPosition = 5f;           // The Y position where enemies start
    public float bottomPosition = -5f;       // The Y position where enemies end
    public int bossSpawnScore = 30;          // Score required to spawn the boss

    private float currentSpawnInterval;      // Current spawn interval, which decreases over time
    private bool bossSpawned = false;        // Whether the boss has been spawned
    private Bullet bulletScript;             // Reference to the bullet script to get the score

    private void Start()
    {
        // Initialize the spawn interval
        currentSpawnInterval = initialSpawnInterval;
        bulletScript = FindObjectOfType<Bullet>();  // Find the bullet script to track the score
        // Start spawning enemies
        StartCoroutine(SpawnEnemies());
    }

    private IEnumerator SpawnEnemies()
    {
        // Infinite loop to keep spawning enemies
        while (!bossSpawned)
        {
            // Spawn a group of mini enemies
            SpawnMiniEnemies();

            // Wait for the current spawn interval before spawning the next wave
            yield return new WaitForSeconds(currentSpawnInterval);

            // Decrease the spawn interval to make enemies spawn faster, but not less than minSpawnInterval
            if (currentSpawnInterval > minSpawnInterval)
            {
                currentSpawnInterval -= spawnSpeedIncreaseRate;
                currentSpawnInterval = Mathf.Max(currentSpawnInterval, minSpawnInterval); // Ensure it doesn't go below the minimum
            }

            // Check if the score is 30 or higher to spawn the boss
            if (level2_bullet.mini_enemy_destroyed >= bossSpawnScore && !bossSpawned)
            {
                SpawnBoss();
                bossSpawned = true; // Set flag to stop mini enemy spawning
            }
        }
    }

    private void SpawnMiniEnemies()
    {
        // Spawn multiple enemies at different positions along the curve
        for (int i = 0; i < numberOfEnemies; i++)
        {
            // Instantiate the mini enemy
            GameObject enemy = Instantiate(miniEnemyPrefab, transform.position, Quaternion.identity);

            // Start moving the enemy along the curve
            StartCoroutine(MoveEnemyAlongCurve(enemy, i));
        }
    }

    private IEnumerator MoveEnemyAlongCurve(GameObject enemy, int index)
    {
        float elapsedTime = 0f;
        Vector3 startPosition = new Vector3(transform.position.x, topPosition, transform.position.z); // Start at the top
        Vector3 targetPosition = new Vector3(transform.position.x, bottomPosition, transform.position.z); // Move to the bottom

        // Move the enemy along the curve
        while (elapsedTime < curveDuration)
        {
            // Check if the enemy is null (destroyed)
            if (enemy == null)
            {
                yield break;  // Exit the coroutine if the enemy has been destroyed
            }

            float t = elapsedTime / curveDuration; // Normalized time (0 to 1)

            // Evaluate the horizontal movement based on the curve
            float curveOffset = enemyCurve.Evaluate(t); // Horizontal movement based on curve

            // Modify enemy position
            Vector3 newPosition = Vector3.Lerp(startPosition, targetPosition, t); // Linear vertical movement
            newPosition.x += curveOffset; // Apply curve offset to the X-axis for snake-like motion

            // Set the new position to the enemy
            enemy.transform.position = newPosition;

            // Increment the elapsed time
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Destroy the enemy after it reaches the bottom, if it still exists
        if (enemy != null)
        {
            Destroy(enemy);
        }
    }

    private void SpawnBoss()
    {
        // Instantiate the boss at the spawner's position
        Instantiate(bossPrefab, transform.position, Quaternion.identity);
        Debug.Log("Boss spawned!");
    }
}
