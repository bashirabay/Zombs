using UnityEngine;

public class HealthPowerUp : MonoBehaviour
{
    public float healthAmount = 20f; // Amount of health to restore
    public AudioSource powerUpSound; // Audio source for the power-up sound effect

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Health playerHealth = other.GetComponent<Health>();
            if (playerHealth != null)
            {
                playerHealth.AddHealth(healthAmount);

                // Play the power-up sound effect
                if (powerUpSound != null)
                {
                    powerUpSound.Play();
                }

                // Immediately hide the power-up object
                gameObject.SetActive(false);
                Destroy(gameObject, powerUpSound.clip.length);
            }
        }
    }
}
