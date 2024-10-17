using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelTimer : MonoBehaviour
{
    private float levelStartTime;  // The time when the level starts
    private float levelEndTime;    // The time when the player finishes the level
    public string levelKey;        // Key to store the best times for each level

    public static bool isLevelComplete = false; // To ensure the timer stops when the level ends

    private void Start()
    {
        // Start the timer when the level starts
        levelStartTime = Time.time;
    }

    private void Update()
    {
        // Calculate and display the timer (optional, for UI purposes)
        if (!isLevelComplete)
        {
            float currentTime = Time.time - levelStartTime;
            Debug.Log("Time: " + currentTime);
        }
    }

    // This method will be called when the player defeats the boss (or finishes the level)
    public void CompleteLevel()
    {
        if (isLevelComplete)
        {
            levelEndTime = Time.time - levelStartTime; // Calculate the total time spent on the level
            isLevelComplete = true;
            Debug.Log("Level Complete! Time: " + levelEndTime);

            // Save the time to the leaderboard
            SaveTime(levelEndTime);

            // Load the next scene (Level 2 or Congratulations)
            LoadNextScene();
        }
    }

    private void SaveTime(float time)
    {
        // Load the saved times from PlayerPrefs
        float[] bestTimes = new float[5];
        for (int i = 0; i < 5; i++)
        {
            bestTimes[i] = PlayerPrefs.GetFloat(levelKey + "_BestTime" + i, float.MaxValue);
        }

        // Check if the new time is better than any of the saved times
        for (int i = 0; i < bestTimes.Length; i++)
        {
            if (time < bestTimes[i])
            {
                // Shift the lower times down and insert the new time
                for (int j = bestTimes.Length - 1; j > i; j--)
                {
                    bestTimes[j] = bestTimes[j - 1];
                }
                bestTimes[i] = time;
                break;
            }
        }

        // Save the updated times back to PlayerPrefs
        for (int i = 0; i < 5; i++)
        {
            PlayerPrefs.SetFloat(levelKey + "_BestTime" + i, bestTimes[i]);
        }

        PlayerPrefs.Save();
    }

    private void LoadNextScene()
    {
        if (SceneManager.GetActiveScene().name == "Level1")
        {
            SceneManager.LoadScene("Level2"); 
        }
        else if (SceneManager.GetActiveScene().name == "Level2")
        {
            SceneManager.LoadScene("Congratulations");
        }
    }
}
