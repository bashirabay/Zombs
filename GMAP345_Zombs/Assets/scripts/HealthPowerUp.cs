using UnityEngine;

public class HealthPowerUp : MonoBehaviour
{
    public float healthAmount = 20f; // Amount of health to restore

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Health playerHealth = other.GetComponent<Health>();
            if (playerHealth != null)
            {
                playerHealth.AddHealth(healthAmount);
                Destroy(gameObject); // Destroy the power-up after use
            }
        }
    }
}
