using UnityEngine;

public class ScrollingBackground : MonoBehaviour
{
    private float scrollSpeed;
    private float destroyDelay;

    public void Initialize(float speed, float delay)
    {
        scrollSpeed = speed;
        destroyDelay = delay;
        Destroy(gameObject, destroyDelay);  // Destroy the object after the specified delay
    }

    void Update()
    {
        // Move the background down
        transform.Translate(Vector3.down * scrollSpeed * Time.deltaTime);
    }
}
