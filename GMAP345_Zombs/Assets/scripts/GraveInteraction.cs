using UnityEngine;

public class GraveInteraction : MonoBehaviour
{
    public GameObject graveObject; // Reference to the grave object
    public Material blueMaterial; // Material for the blue glow effect
    public GameObject dugObject; // Reference to the DUG object
    public GameManager gameManager; // Reference to the GameManager script

    private bool isGraveBlue = false; // Flag to track if the grave is blue
    private bool isGraveInteractive = true; // Flag to track if the grave is still interactive
    private Material originalMaterial; // To store the original material of the grave

    void Start()
    {
        // Store the original material of the grave
        Renderer renderer = graveObject.GetComponent<Renderer>();
        if (renderer != null)
        {
            originalMaterial = renderer.material;
        }

        // Ensure the DUG object is initially inactive
        if (dugObject != null)
        {
            dugObject.SetActive(false);
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (isGraveInteractive && other.CompareTag("Player"))
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

                // Remove the blue material (restore the original material)
                Renderer renderer = graveObject.GetComponent<Renderer>();
                if (renderer != null)
                {
                    renderer.material = originalMaterial;
                }

                // Activate the specified DUG object
                if (dugObject != null)
                {
                    dugObject.SetActive(true);
                }

                // Set the grave as no longer interactive
                isGraveInteractive = false;

                // Reset the blue flag
                isGraveBlue = false;
            }
        }
    }
}
