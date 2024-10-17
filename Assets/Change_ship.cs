using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ChangeShip : MonoBehaviour
{
    public Image shipPreviewImage;   // The UI Image that shows the currently selected ship
    public Sprite[] shipSprites;     // Array of available ship sprites
    private int currentIndex = 0;    // Index to track the currently selected ship

    private void Start()
    {
        // Initialize the preview image with the first ship sprite
        shipPreviewImage.sprite = shipSprites[currentIndex];
    }

    // Called when the "Next" button is pressed
    public void NextShip()
    {
        currentIndex = (currentIndex + 1) % shipSprites.Length;
        shipPreviewImage.sprite = shipSprites[currentIndex];
    }

    // Called when the "Previous" button is pressed
    public void PreviousShip()
    {
        currentIndex--;
        if (currentIndex < 0) currentIndex = shipSprites.Length - 1;
        shipPreviewImage.sprite = shipSprites[currentIndex];
    }

    // Called when the "Save" button is pressed
    public void SaveSelection()
    {
        // Save the current ship sprite's index to PlayerPrefs
        PlayerPrefs.SetInt("SelectedShip", currentIndex);
        PlayerPrefs.Save();  // Save the data
        Debug.Log("Ship saved: " + currentIndex);
    }

    // Called when the "Start" button is pressed
    public void StartGame()
    {
        // Save the selected ship before starting the game
        SaveSelection();

        // Load the game scene (replace "GameScene" with your actual scene name)
        SceneManager.LoadScene("level1");
    }
}
