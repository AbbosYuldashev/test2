using UnityEngine;

public class level1_bullet : MonoBehaviour
{
    public float lifetime = 3f;
    public static int mini_enemy_destroyed = 0;// Time before the bullet is destroyed
    public static int healinfo = 0;
    private void Start()
    {
        // Destroy the bullet after a certain time if it doesn't hit anything

        Destroy(gameObject, lifetime);
    }
    private void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D hitInfo)
    {
        if (hitInfo.CompareTag("spider"))
        {
            mini_enemy_destroyed++;
            Destroy(gameObject); // Destroy the bullet



        }

    }
}
