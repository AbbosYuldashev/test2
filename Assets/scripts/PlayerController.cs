using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f; // Speed of the spaceship
    public float boundaryPadding = 0.5f; // Padding to prevent player from touching the edge of the screen

    private Vector2 screenBounds; // To store screen limits
    private float objectWidth;
    private float objectHeight;

    void Start()
    {
        // Calculate screen boundaries
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        objectWidth = transform.GetComponent<SpriteRenderer>().bounds.size.x / 2;
        objectHeight = transform.GetComponent<SpriteRenderer>().bounds.size.y / 2;
    }

    void Update()
    {
        // Get input from the player (arrow keys or WASD)
        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");

        // Calculate the movement direction
        Vector3 moveDirection = new Vector3(moveX, moveY, 0).normalized;

        // Move the spaceship
        transform.position += moveDirection * moveSpeed * Time.deltaTime;

        // Restrict the spaceship within screen bounds
        float clampedX = Mathf.Clamp(transform.position.x, -screenBounds.x + objectWidth + boundaryPadding, screenBounds.x - objectWidth - boundaryPadding);
        float clampedY = Mathf.Clamp(transform.position.y, -screenBounds.y + objectHeight + boundaryPadding, screenBounds.y - objectHeight - boundaryPadding);
        transform.position = new Vector3(clampedX, clampedY, transform.position.z);
    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        if (hitInfo.CompareTag("Enemy"))
        {
            // Call the TakeDamage method
            GetComponent<HealthManager>().TakeDamage();

            // Optional: Pause the game if needed
            // Time.timeScale = 0f; // Uncomment if you want to pause the game
        }else if (hitInfo.CompareTag("boss_bullet"))
        {
            GetComponent<HealthManager>().TakeDamage();
        }
    }


}