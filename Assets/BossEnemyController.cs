using UnityEngine;

public class BossEnemyController : MonoBehaviour
{
    public float moveSpeed = 1f; // Speed at which the boss moves down
    public GameObject bulletPrefab; // Reference to the bullet prefab
    public Transform shootPoint; // Point from which the bullets will be instantiated
    public float shootInterval = 2f;
    public static int boss_health = 5;
    public static bool isdead = false;// Time between each shot

    private void Start()
    {
        // Start the shooting routine
        InvokeRepeating(nameof(Shoot), shootInterval, shootInterval);
    }

    private void Update()
    {
        // Move the boss downwards
        transform.position += Vector3.down * moveSpeed * Time.deltaTime;

        // Optional: Add logic to stop or destroy the boss if it reaches a certain position
        if ((transform.position.y < -6f)) // Change this value based on your game design
        {
            LevelTimer.isLevelComplete = true;
            Destroy(gameObject); // Destroy the boss if it goes below a certain point
        }

        if (boss_health <= 0)
        {
            isdead=true;
            Destroy(gameObject);
        }
    }

    private void Shoot()
    {
        // Instantiate a bullet at the shoot point
        Instantiate(bulletPrefab, shootPoint.position, Quaternion.identity);
    }
    private void OnTriggerEnter2D(Collider2D hitInfo)
    {
        if (hitInfo.CompareTag("Bullet"))
        {
            boss_health--;
        }

    }
}
