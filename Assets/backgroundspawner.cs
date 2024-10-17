using UnityEngine;

public class BackgroundSpawner : MonoBehaviour
{
    public GameObject backgroundPrefab;   // Prefab for the background sprite
    public float spawnInterval = 5f;      // Time between spawning new backgrounds
    public float scrollSpeed = 2f;        // Speed at which the background moves
    public float destroyDelay = 10f;      // Time after which the background gets destroyed

    private float timeSinceLastSpawn;

    void Update()
    {
        // Update the timer for spawning new background
        timeSinceLastSpawn += Time.deltaTime;

        // Check if it's time to spawn a new background
        if (timeSinceLastSpawn >= spawnInterval)
        {
            SpawnBackground();
            timeSinceLastSpawn = 0f;  // Reset timer after spawning
        }
    }

    // Function to spawn a new background
    void SpawnBackground()
    {
        // Spawn a new background at the top of the screen
        GameObject newBackground = Instantiate(backgroundPrefab, new Vector3(0, transform.position.y, 0), Quaternion.identity);

        // Start moving the background down and destroy it after some time
        newBackground.AddComponent<ScrollingBackground>().Initialize(scrollSpeed, destroyDelay);
    }
}
