using UnityEngine;

public class SprintPowerUp : MonoBehaviour
{
    public float sprintDuration = 5f;
    public AudioSource powerUpSound; // Audio source for the power-up sound effect

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            FPSController fpsController = other.GetComponent<FPSController>();
            if (fpsController != null)
            {
                fpsController.ActivateSprintPowerUp(sprintDuration);

                // Play the power-up sound effect
                if (powerUpSound != null)
                {
                    powerUpSound.Play();
                }

                // Destroy the power-up after use
                Destroy(gameObject, powerUpSound.clip.length);
            }
        }
    }
}
