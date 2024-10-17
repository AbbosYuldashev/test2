using UnityEngine;

public class ShieldPowerUp : MonoBehaviour
{
  // How long the shield will last

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the player is the one who collided with the power-up
        if (other.CompareTag("Player"))
        {
            Destroy(gameObject);
            
        }
    }
}
