using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class LeaderboardManager : MonoBehaviour
{
    public TextMeshProUGUI[] timeTexts;
    public TextMeshProUGUI[] timeTexts2; // Array to hold references to the TextMeshPro objects for displaying times

    private void Start()
    {
        // Load and display the best times for Level 1
        DisplayBestTimes();
        DisplayLeaderboard2();
    }

    private void DisplayBestTimes()
    {
        // Loop through the number of time slots you want to display (5 in this case)
        for (int i = 0; i < timeTexts.Length; i++)
        {
            // Load the stored time for each slot, defaulting to 0 if not found
            float time = PlayerPrefs.GetFloat($"Level1Time_{i + 1}", 0f);

            // Update the corresponding TextMeshPro object with the time
            if (time > 0)
            {
                timeTexts[i].text = $"{i + 1}. {time:F2}"; // Format the time to 2 decimal places
            }
            else
            {
                timeTexts[i].text = $"{i + 1}. ---"; // Placeholder if no time is stored
            }
        }
    }
    private void DisplayLeaderboard2()
    {
        for (int i = 0; i < timeTexts2.Length; i++)
        {
            float time = PlayerPrefs.GetFloat("Level2Time_" + i, float.MaxValue);
            if (time != float.MaxValue)
            {
                timeTexts2[i].text = $"{i + 1}. {time:F2}"; // Format time to 2 decimal places
            }
            else
            {
                timeTexts2[i].text = $"{i + 1}. ---"; // Display "N/A" if no time is stored
            }
        }
    }
    public void Inventory()
    {
        SceneManager.LoadScene("ship_selection");
    }
}
