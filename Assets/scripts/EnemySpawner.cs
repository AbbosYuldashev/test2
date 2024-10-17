using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject miniEnemyPrefab; // Reference to the mini enemy prefab
    public GameObject bossEnemyPrefab; // Reference to the boss enemy prefab
    public float spawnInterval = 2f; // Time between mini enemy spawns
    public float minX = -5f; // Minimum x position for spawning mini enemies
    public float maxX = 5f;  // Maximum x position for spawning mini enemies
    public float spawnY = 6f; // Fixed y position for spawning enemies
    public float bossX = 0f;
    public int for_boss = 5;
    public static bool is_boss_spawn = false;// Fixed x position for spawning the boss

    

    private void Start()
    {
        // Repeatedly call the SpawnMiniEnemy method
        InvokeRepeating(nameof(SpawnMiniEnemy), 0f, spawnInterval);
    }

    private void SpawnMiniEnemy()
    {
        // Generate a random x position within the range for mini enemies
        float randomX = Random.Range(minX, maxX);
        Vector3 spawnPosition = new Vector3(randomX, spawnY, 0);

        // Spawn a mini enemy at the random position
        Instantiate(miniEnemyPrefab, spawnPosition, Quaternion.identity);
    }

    private void Update()
    {
        Debug.Log("working1111111");
        // Check the bullet count
     

        // Check if enough mini enemies have been destroyed and the boss hasn't spawned yet
       if ((level1_bullet.mini_enemy_destroyed>=for_boss)&&!is_boss_spawn)
        {
            
            SpawnBossEnemy();
            is_boss_spawn=true;
        }
    }

    private void SpawnBossEnemy()
    {
        Debug.Log("Bosssssss spawnde");
        // Spawn the boss at a fixed position (center of the screen)
        Vector3 bossSpawnPosition = new Vector3(bossX, spawnY, 0);

        Instantiate(bossEnemyPrefab, bossSpawnPosition, Quaternion.identity);
        Debug.Log("Boss enemy spawned!"); // Debug message to confirm the boss spawns
        CancelInvoke(nameof(SpawnMiniEnemy)); // Stop spawning mini enemies
         
    }
}
