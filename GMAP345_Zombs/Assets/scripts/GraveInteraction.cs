using UnityEngine;

public class GraveInteraction : MonoBehaviour
{
    public GameObject graveObject; // Reference to the grave object
    public Material blueMaterial; // Material for the blue glow effect
    public GameManager gameManager; // Reference to the GameManager script

    private bool isGraveBlue = false; // Flag to track if the grave is blue

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Vector3 directionToGrave = graveObject.transform.position - other.transform.position;
            float dotProduct = Vector3.Dot(other.transform.forward, directionToGrave.normalized);

            if (dotProduct > 0.9f) // Player is facing the grave within a certain angle threshold
            {
                // Change the material of the grave to make it glow blue
                Renderer renderer = graveObject.GetComponent<Renderer>();
                if (renderer != null && !isGraveBlue)
                {
                    renderer.material = blueMaterial;
                    isGraveBlue = true;
                }
            }

            // Check for right-click input and interaction with the grave
            if (Input.GetMouseButtonDown(1) && isGraveBlue)
            {
                // Increase score by 20 points
                gameManager.AddScore(20);

                // Destroy the grave object
                Destroy(graveObject);

                // Reset the blue flag
                isGraveBlue = false;
            }
        }
    }
}
