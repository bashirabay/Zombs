using UnityEngine;

public class DoubleShotPowerUp : MonoBehaviour
{
    public float doubleShotDuration = 10f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GunfireController gunfireController = other.GetComponentInChildren<GunfireController>();
            if (gunfireController != null)
            {
                gunfireController.ActivateDoubleShot(doubleShotDuration);
                Destroy(gameObject); // Destroy the power-up after use
            }
        }
    }
}
