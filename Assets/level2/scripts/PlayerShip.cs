using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerShip : MonoBehaviour
{
    public float moveSpeed = 5f;              // Speed at which the player ship moves
    public float speedBoostMultiplier = 2f;   // How much the speed will increase during the speed power-up
    public GameObject bulletPrefab;           // Reference to the bullet prefab
    public Transform bulletSpawnPoint;        // Point where the bullets will spawn
    public float bulletSpeed = 10f;           // Speed of the bullets
    public float shootInterval = 0.5f;        // Time between shots
    public static int player_health = 6;      // Player's health
    public GameObject shieldPrefab;           // Reference to the shield prefab
    public GameObject speedPrefab;            // Reference to the speed power-up prefab

    private GameObject activeShield;          // The instantiated shield object
    public bool isDamageable = true;          // Whether the player can take damage
    public float shieldDuration = 5f;         // How long the shield lasts
    public float speedBoostDuration = 5f;     // How long the speed boost lasts
    private bool isSpeedBoosted = false;      // Whether the player is currently speed-boosted
    private float nextShootTime = 0f;         // Time until the next shot is allowed
    public SpriteRenderer spriteRenderer;      // Reference to the player's sprite renderer
    public Sprite[] shipSprites;
    private Camera mainCamera;
    public GameObject deathPanel;
    private bool isPaused = false;
    private Vector2 screenBounds;
    // Track the pause state

    // New variables for audio
    public AudioClip shootSound;         // Reference to the shooting sound effect
    private AudioSource audioSource;     // Reference to the AudioSource component

    private void PlayerDeath()
    {
        Debug.Log("reason1");
        // Show the death panel and pause the game
        deathPanel.SetActive(true);
        Time.timeScale = 0f; // Pause the game
        Debug.Log("Player Died!");
    }
    private void Start()
    {
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        Time.timeScale = 1f;
        deathPanel.SetActive(false);
        isPaused = false;
        spriteRenderer = GetComponent<SpriteRenderer>(); // Get the SpriteRenderer component attached to the ship
        audioSource = GetComponent<AudioSource>();       // Get the AudioSource component

        // Load the saved ship index from PlayerPrefs
        int selectedShipIndex = PlayerPrefs.GetInt("SelectedShip", 0); // Default to 0 if not set

        // Apply the chosen ship sprite
        if (selectedShipIndex >= 0 && selectedShipIndex < shipSprites.Length)
        {
            spriteRenderer.sprite = shipSprites[selectedShipIndex];
        }
    }

    private void Update()
    {
        if (BossEnemyController.isdead)
        {
            Debug.Log("reason2");
            deathPanel.SetActive(true);
        }
        Debug.Log(player_health);

        if (player_health <= 0)
        {
            PlayerDeath(); // Call the death function if health reaches 0
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause(); // Toggle pause state when Escape is pressed
        }
        else if (Input.GetKeyDown(KeyCode.Return) && isPaused)
        {
            TogglePause(); // Resume the game when Enter is pressed and the game is paused
        }

        if (SceneManager.GetActiveScene().name == "level2")
        {
            if (BossMovement.bossHealth <= 0)
            {
                Debug.Log("reason3");
                deathPanel.SetActive(true);
                player_health = 6;
                Time.timeScale = 0f;

            }
            else
            {
                deathPanel.SetActive(false);
            }
        }
        else
        {
        }

        if (!isPaused)
        {
            MovePlayer();                         // Handle player movement
            HandleShooting();                     // Handle shooting
        }
    }
    public void Next()
    {
        BossEnemyController.isdead = false;
        BossEnemyController.boss_health = 5;
        EnemySpawner.is_boss_spawn = false;
        level1_bullet.mini_enemy_destroyed = 0;
        
        SceneManager.LoadScene("levels");
    }

    public void RestartGame()
    {
        isPaused = false;
        player_health = 6;
        Time.timeScale = 1f;
        BossMovement.bossHealth = 50;
        BossEnemyController.boss_health = 5;

        if (SceneManager.GetActiveScene().name == "level1")
        {
            deathPanel.SetActive(false);
            SceneManager.LoadScene("level1");
        }
        else
        {
            SceneManager.LoadScene("level2");
        }

    }
    public void GoToMainMenu()
    {
        isPaused = false;
        player_health = 6;

        Time.timeScale = 1f;
        deathPanel.SetActive(false);// Resume normal time
        SceneManager.LoadScene("leaderboard"); // Load the Main Menu scene (make sure to name your main menu scene)
    }

    private void MovePlayer()
    {
        float horizontalInput = Input.GetAxis("Horizontal"); // Get horizontal input (A/D or Left/Right arrows)
        float verticalInput = Input.GetAxis("Vertical");     // Get vertical input (W/S or Up/Down arrows)

        float currentMoveSpeed = isSpeedBoosted ? moveSpeed * speedBoostMultiplier : moveSpeed; // Use boosted speed if active

        Vector3 movement = new Vector3(horizontalInput, verticalInput, 0f) * currentMoveSpeed * Time.deltaTime; // Calculate movement
        Vector3 newPosition = transform.position + movement; // Calculate new position

        // Clamp the player's position within the camera's bounds
        float halfPlayerWidth = spriteRenderer.bounds.extents.x;
        float halfPlayerHeight = spriteRenderer.bounds.extents.y;

        newPosition.x = Mathf.Clamp(newPosition.x, -screenBounds.x + halfPlayerWidth, screenBounds.x - halfPlayerWidth);
        newPosition.y = Mathf.Clamp(newPosition.y, -screenBounds.y + halfPlayerHeight, screenBounds.y - halfPlayerHeight);

        transform.position = newPosition; ; // Move the player ship
    }

    private void HandleShooting()
    {
        // Check if the fire button is pressed and enough time has passed since the last shot
        if (Input.GetButton("Fire1") && Time.time >= nextShootTime)
        {
            ShootBullet();
            nextShootTime = Time.time + shootInterval; // Set the time for the next shot
        }
    }

    private void ShootBullet()
    {
        // Instantiate the bullet and set its position and velocity
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, Quaternion.identity);
        Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();
        bulletRb.velocity = transform.up * bulletSpeed; // Move the bullet upwards

        PlayShootSound(); // Play shooting sound
    }

    private void PlayShootSound()
    {
        if (shootSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(shootSound); // Play the shoot sound
        }
    }

    private void OnTriggerEnter2D(Collider2D hitInfo)
    {
        if (hitInfo.CompareTag("MiniEnemy") || hitInfo.CompareTag("boss_bullet") || hitInfo.CompareTag("Level2_boss") || hitInfo.CompareTag("spider") || hitInfo.CompareTag("Boss"))
        {
            if (isDamageable) // Only apply damage if the player is damageable
            {
                player_health--;
                Debug.Log("Player hit! Health: " + player_health);
            }
        }
        else if (hitInfo.CompareTag("heal"))
        {
            player_health++;
            Debug.Log("Player healed! Health: " + player_health);
        }
        else if (hitInfo.CompareTag("shield"))
        {
            ActivateShield();
            Destroy(hitInfo.gameObject); // Destroy the shield power-up after collection
        }
        else if (hitInfo.CompareTag("speed"))
        {
            ActivateSpeedBoost();
            Destroy(hitInfo.gameObject); // Destroy the speed power-up after collection
        }
    }

    // Method to activate the shield
    private void ActivateShield()
    {
        if (activeShield == null) // Check if the shield is not already active
        {
            activeShield = Instantiate(shieldPrefab, transform.position, Quaternion.identity);
            activeShield.transform.SetParent(transform); // Make the shield follow the player

            isDamageable = false; // Make the player invulnerable
            Debug.Log("Shield Activated!");

            StartCoroutine(DeactivateShieldAfterTime()); // Deactivate after a set time
        }
    }

    // Coroutine to deactivate the shield after a certain duration
    private IEnumerator DeactivateShieldAfterTime()
    {
        yield return new WaitForSeconds(shieldDuration);
        isDamageable = true; // Make the player damageable again
        Debug.Log("Shield Deactivated!");

        if (activeShield != null)
        {
            Destroy(activeShield); // Destroy the shield visual
            activeShield = null;
        }
    }

    // Method to activate the speed boost
    private void ActivateSpeedBoost()
    {
        if (!isSpeedBoosted) // Check if speed boost is not already active
        {
            isSpeedBoosted = true;
            Debug.Log("Speed Boost Activated!");

            StartCoroutine(DeactivateSpeedBoostAfterTime()); // Deactivate after a set time
        }
    }

    // Coroutine to deactivate the speed boost after a certain duration
    private IEnumerator DeactivateSpeedBoostAfterTime()
    {
        yield return new WaitForSeconds(speedBoostDuration);
        isSpeedBoosted = false; // Revert to normal speed
        Debug.Log("Speed Boost Deactivated!");
    }

    // Method to toggle pause state
    private void TogglePause()
    {
        isPaused = !isPaused; // Toggle the pause state

        // Pause or resume the game time
        if (isPaused)
        {
            Time.timeScale = 0f; // Pause the game
            Debug.Log("Game Paused");
        }
        else
        {
            Time.timeScale = 1f; // Resume the game
            Debug.Log("Game Resumed");
        }
    }
}