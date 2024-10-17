using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    public Image[] hearts; // Array to hold heart images
    public Sprite fullHeart; // Full heart sprite
    public Sprite emptyHeart; // Empty heart sprite
    public int maxHealth = 3; // Maximum health (number of hearts)
    private int currentHealth; // Current health

    void Start()
    {
        currentHealth = maxHealth; // Set current health to maximum health
        UpdateHealthUI();
    }

    public void TakeDamage()
    {
        if (currentHealth > 0) // Only take damage if health is above zero
        {
            currentHealth--; // Reduce health by one
            UpdateHealthUI();

            if (currentHealth <= 0) // Check if health reached zero
            {
                GameOver(); // Call game over function
            }
        }
    }

    private void UpdateHealthUI()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < currentHealth)
            {
                hearts[i].sprite = fullHeart; // Set full heart sprite
            }
            else
            {
                hearts[i].sprite = emptyHeart; // Set empty heart sprite
            }
        }
    }

    private void GameOver()
    {
        Debug.Log("Game Over!"); // Implement game over logic
        Time.timeScale = 0f; // Pause the game (optional)
    }
}
