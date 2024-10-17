using UnityEngine;

public class Boss_Bullet : MonoBehaviour
{
    public float speed = 5f; // Speed of the bullet
    public float lifetime = 3f; // Time before the bullet is destroyed

    private void Start()
    {
        // Destroy the bullet after a certain time
        Destroy(gameObject, lifetime);
    }

    private void Update()
    {
        // Move the bullet downwards
        transform.position += Vector3.down * speed * Time.deltaTime;
    }
}
