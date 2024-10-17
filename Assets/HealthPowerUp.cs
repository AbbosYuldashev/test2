using UnityEngine;

public class HealthPowerUp : MonoBehaviour
{
    // How much health this power-up will restore

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the object that triggered the power-up is the player or player's bullet
        if (other.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}
