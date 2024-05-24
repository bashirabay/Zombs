using UnityEngine;

public class SprintPowerUp : MonoBehaviour
{
    public float sprintDuration = 5f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            FPSController fpsController = other.GetComponent<FPSController>();
            if (fpsController != null)
            {
                fpsController.ActivateSprintPowerUp(sprintDuration);
                Destroy(gameObject); // Destroy the power-up after use
            }
        }
    }
}
