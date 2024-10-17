using UnityEngine;
using TMPro;

public class StarWarsIntroCustom : MonoBehaviour
{
    public TextMeshProUGUI introText;  // Reference to the TextMeshPro object
    public Vector3 startPosition = new Vector3(0, -10, -50); // Customize this to set the start position
    public Vector3 endPosition = new Vector3(0, 50, 100);    // Customize this to set the end position
    public float tiltAngle = 15f;      // Tilt angle for the backward tilt (rotation on X-axis)
    public float totalDuration = 10f;  // Time it will take to move from start to end (in seconds)

    private RectTransform rectTransform; // The RectTransform of the text
    private float elapsedTime = 0f;      // To track time for movement

    void Start()
    {
        rectTransform = introText.GetComponent<RectTransform>();

        // Set the initial position of the text
        rectTransform.localPosition = startPosition;

        // Apply the initial rotation to tilt the text slightly backward (rotation on the X-axis)
        rectTransform.localRotation = Quaternion.Euler(tiltAngle, 0, 0);
    }

    void Update()
    {
        // Increase elapsed time based on how much time has passed since last frame
        elapsedTime += Time.deltaTime;

        // Calculate the percentage of time passed relative to the total duration
        float progress = Mathf.Clamp01(elapsedTime / totalDuration);

        // Lerp from start to end position based on the progress value
        rectTransform.localPosition = Vector3.Lerp(startPosition, endPosition, progress);

        // Optionally, check if the text has reached the end point
        if (progress >= 1f)
        {
            Debug.Log("Intro finished!");
        }
    }
}
