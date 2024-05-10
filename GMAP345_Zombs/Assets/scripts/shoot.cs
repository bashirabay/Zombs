using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shoot : MonoBehaviour
{
    // Variables
    public GameObject projectile;
    public Transform muzzlePoint;
    public GameObject projectileParent;
    public float lifespan = 2f;
    public float projectileSpeed = 1000f;

    // Reference to the PauseMenu script
    public PauseMenu pauseMenu;

    // Update is called once per frame
    void Update()
    {
        // Check if the game is not paused before allowing shooting
        if (!PauseMenu.GamePaused && Input.GetMouseButtonDown(0))
        {
            // Instantiate our prefab projectile
            GameObject currentProjectile = Instantiate(projectile, muzzlePoint.position, muzzlePoint.rotation);

            // Set the parent of the projectile to a null object so it is not impacted by our character movement
            currentProjectile.transform.SetParent(projectileParent.transform);

            // Add force to the projectile
            currentProjectile.GetComponent<Rigidbody>().AddForce(muzzlePoint.up * projectileSpeed, ForceMode.Impulse);

            // Destroy the projectile after time has passed
            Destroy(currentProjectile, lifespan);
        }
    }
}
