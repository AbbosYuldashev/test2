using UnityEngine;

public class LoopingBackground : MonoBehaviour
{
    public float scrollSpeed = 2f;  // Speed at which the background moves
    public float backgroundHeight;  // Height of the background sprite

    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;  // Store the initial position of the background
        backgroundHeight = GetComponent<SpriteRenderer>().bounds.size.y;  // Get the height of the sprite
    }

    void Update()
    {
        // Move the background down based on the scroll speed and time
        transform.Translate(Vector3.down * scrollSpeed * Time.deltaTime);

        // If the background moves out of view, reposition it to the top
        if (transform.position.y < -backgroundHeight)
        {
            RepositionBackground();
        }
    }

    void RepositionBackground()
    {
        // Move the background back to the top
        transform.position = new Vector3(transform.position.x, transform.position.y + 2 * backgroundHeight, transform.position.z);
    }
}
