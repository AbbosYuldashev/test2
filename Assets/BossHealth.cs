using UnityEngine;
using UnityEngine.UI;

public class BossHealth : MonoBehaviour
{
    public Slider healthBar;  // Reference to the UI slider for the health bar
    private BossEnemyController boss; // Reference to the BossMovement script

    private void Start()
    {
        // Initially hide the health bar
        healthBar.gameObject.SetActive(false);
    }

    private void Update()
    {
        // Check if the boss exists in the scene
        // Find the boss in the scene

        if (FindObjectOfType<BossEnemyController>() != null) // If the boss is found
        {
            if (!healthBar.gameObject.activeInHierarchy)
            {
                // Activate the health bar when the boss appears
                healthBar.gameObject.SetActive(true);
                healthBar.maxValue = BossEnemyController.boss_health; // Set max value to boss health
            }

            // Update the health bar's value to match the boss's current health
            healthBar.value = BossEnemyController.boss_health;
        }
        else
        {
            // Optionally hide the health bar when the boss is not in the scene
            healthBar.gameObject.SetActive(false);
        }
    }
}