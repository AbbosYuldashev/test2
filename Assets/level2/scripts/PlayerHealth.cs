using System.Collections;
using UnityEngine;

public class PlayerHealthManager : MonoBehaviour
{
    public GameObject fullHeartPrefab;  // Prefab for a full heart
    public GameObject halfHeartPrefab;  // Prefab for a half heart
    public GameObject emptyHeartPrefab; // Prefab for an empty heart
    public Transform[] heartPositions;  // Array of positions for the 3 hearts

    public int maxHearts = 3;           // Max number of hearts (each heart = 2 lives)
    public int playerHealth;           // Health value from your player script (total 6 lives)

    private void Start()
    {
        // Initialize health from the player ship's health value
        playerHealth = PlayerShip.player_health;

        // Create the hearts at the start of the game
        UpdateHeartsDisplay();
    }
    public GameObject shieldVisualEffect; // Drag your shield visual prefab here

    


    private void Update()
    {
        Debug.Log(playerHealth);
        // Update hearts display if player health changes
        int currentHealth = PlayerShip.player_health;
        if (currentHealth != playerHealth)
        {
            playerHealth = currentHealth;
            UpdateHeartsDisplay();
        }
    }

    private void UpdateHeartsDisplay()
    {
        
        // Loop through the heart positions and instantiate the correct prefabs
        for (int i = 0; i < maxHearts; i++)
        {
            // Clear the current heart prefab at this position (if any)
            if (heartPositions[i].childCount > 0)
            {
                Destroy(heartPositions[i].GetChild(0).gameObject);
            }

            // Calculate which heart to display at this position
            GameObject heartPrefab = null;

            if (playerHealth >= (i + 1) * 2)
            {
                // Full heart
                heartPrefab = fullHeartPrefab;
            }
            else if (playerHealth == (i * 2) + 1)
            {
                // Half heart
                heartPrefab = halfHeartPrefab;
            }
            else
            {
                // Empty heart
                heartPrefab = emptyHeartPrefab;
            }

            // Instantiate the selected heart prefab and set its position
            if (heartPrefab != null)
            {
                GameObject heart = Instantiate(heartPrefab, heartPositions[i].position, Quaternion.identity);
                heart.transform.SetParent(heartPositions[i], false); // Set the parent without altering local position
            }
        }
    }

}
