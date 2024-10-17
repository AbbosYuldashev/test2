using UnityEngine;
using UnityEngine.UI;
public class BossMovement : MonoBehaviour
{
    public float horizontalSpeed = 3f;  // Speed at which the boss moves horizontally
    public float horizontalRange = 5f;  // How far the boss moves left and right
    public float verticalSpeed = 1f;    // Optional vertical movement speed
    public float rotationSpeed = 50f;   // Speed of rotation for the boss
    public GameObject bossBulletPrefab; // Bullet prefab the boss will shoot
    public Transform bulletSpawnPoint;  // Where the bullet should spawn from
    public float shootInterval = 2f;    // Time between each bullet shot
    public static int bossHealth = 50;         // Health of the boss
    public float bulletSpeed = 10f;
    public GameObject BossdestroyedEnemyPrefab;
   

    private Vector3 startPosition;      // Starting position of the boss
    private float shootTimer;// Timer to control bullet shooting

    private void Start()
    {
        // Save the boss's starting position
        startPosition = transform.position;
        transform.Rotate(0, 0,180);
        
    }

    private void Update()
    {
        
        float horizontalMovement = Mathf.Sin(Time.time * horizontalSpeed) * horizontalRange;
        float verticalMovement = Mathf.Sin(Time.time * verticalSpeed); // Optional vertical movement

        // Set the new position of the boss based on the sine wave calculation
        transform.position = new Vector3(startPosition.x + horizontalMovement, startPosition.y + verticalMovement, startPosition.z);

        // Set the new position of the boss based on the sine wave calculation

        // Rotate the boss prefab around the Z-axis
        

        // Handle shooting bullets
        shootTimer += Time.deltaTime;
        if (shootTimer >= shootInterval)
        {
            ShootBullet();
            shootTimer = 0f;
        }
    }

    private void ShootBullet()
    {
        // Instantiate the boss's bullet at the specified spawn point
        GameObject bullet = Instantiate(bossBulletPrefab, bulletSpawnPoint.position, Quaternion.identity);
        Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();
        bulletRb.velocity = transform.up * bulletSpeed;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the enemy is hit by a bullet
        if (other.CompareTag("Bullet2"))
        {
            Debug.Log("Mini enemy hit!");

            TakeDamage(1);

           
        }
    }

    // Method to reduce boss health when hit by the player's bullets
    public void TakeDamage(int damageAmount)
    {
        bossHealth -= damageAmount;
        if (bossHealth <= 0)
        {
            DestroyBoss();
        }
    }

    private void DestroyBoss()
    {
        Vector3 miniEnemyPosition = transform.position;
        Quaternion bossRotation = transform.rotation;

        // Destroy the boss game object when its health reaches zero

        GameObject destructionEffect = Instantiate(BossdestroyedEnemyPrefab, miniEnemyPosition,bossRotation);
        Destroy(gameObject);
        Destroy(destructionEffect, 1);
        Debug.Log("Boss defeated!");
    }
}
