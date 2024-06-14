using UnityEngine;

public class DoubleShotPowerUp : MonoBehaviour
{
    public float doubleShotDuration = 10f;
    public AudioSource powerUpSound; // Audio source for the power-up sound effect

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GunfireController gunfireController = other.GetComponentInChildren<GunfireController>();
            if (gunfireController != null)
            {
                gunfireController.ActivateDoubleShot(doubleShotDuration);

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
