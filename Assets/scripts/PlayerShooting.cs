using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public GameObject bulletPrefab; // Reference to the bullet prefab
    public Transform firePoint; // The position from which the bullet is fired
    public float bulletSpeed = 10f; // Speed of the bullet

    void Update()
    {
        if (Input.GetButtonDown("Fire1")) // Default is left mouse button or Ctrl
        {
            Shoot();
        }
    }

    void Shoot()
    {
        // Create a bullet instance at the firePoint position
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

        // Set the bullet's velocity to move in the direction of the firePoint
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.velocity = firePoint.up * bulletSpeed; // Fire in the direction of firePoint's "up" vector
    }
}
