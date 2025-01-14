using UnityEngine;
using UnityEngine.SceneManagement;

public class Level1Timer : MonoBehaviour
{
    public static bool isfinishedlevel1=false;
    private float startTime; // To store the time when the level starts
     // To track if the boss is defeated
    private float elapsedTime; // To store the time when the boss is defeated

    private void Start()
    {
        // Record the starting time when the level begins
        startTime = Time.time;
    }

    private void Update()
    {
        
        // Check if the boss is dead and the level is not complete
        if (BossEnemyController.isdead)
        {
            isfinishedlevel1 = true;
            // Calculate elapsed time and store it in PlayerPrefs
            elapsedTime = Time.time - startTime;
           

            // Update the best times
            UpdateBestTimes(elapsedTime);

            Debug.Log("Level 1 completed in: " + elapsedTime + " seconds.");
        }
    }

    private void UpdateBestTimes(float newTime)
    {
        
        // Store the new time if it's better than any existing times
        for (int i = 1; i <= 5; i++)
        {
            float currentBestTime = PlayerPrefs.GetFloat($"Level1Time_{i}", float.MaxValue);

            if (newTime < currentBestTime)
            {
                // Shift times down to make room for the new best time
                for (int j = 5; j > i; j--)
                {
                    PlayerPrefs.SetFloat($"Level1Time_{j}", PlayerPrefs.GetFloat($"Level1Time_{j - 1}"));
                }
                // Save the new best time in the correct slot
                PlayerPrefs.SetFloat($"Level1Time_{i}", newTime);
                PlayerPrefs.Save();
                


                break;
            }
        }
    }
}
