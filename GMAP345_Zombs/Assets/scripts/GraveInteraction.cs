using UnityEngine;
using System.Collections;

public class GraveInteraction : MonoBehaviour
{
    public GameObject graveObject; // Reference to the grave object
    public Material blueMaterial; // Material for the blue glow effect
    public GameObject dugObject; // Reference to the DUG object
    public GameManager gameManager; // Reference to the GameManager script
    public GameObject healthPowerUpPrefab; // Prefab for the health power-up
    public GameObject sprintPowerUpPrefab; // Prefab for the sprint power-up
    public GameObject doubleShotPowerUpPrefab; // Prefab for the double shot power-up
    public GameObject objectToDeactivate; // Game object to deactivate during interaction (e.g., player's gun)
    public GameObject objectToActivate; // Game object to activate during interaction (e.g., digging tool)
    public Transform powerUpSpawnPoint; // Transform for the power-up spawn location
    public AudioSource[] audioSources; // Array of audio sources for the sounds
    public string uiTextTag = "PowerUpText"; // Tag for the UI text objects to display power-up activated messages

    private bool isGraveBlue = false; // Flag to track if the grave is blue
    private bool isGraveInteractive = true; // Flag to track if the grave is still interactive
    private bool isDigging = false; // Flag to track if the player is currently digging the grave
    private bool isCooldownActive = false; // Flag to track if the cooldown is active
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

        // Ensure power-up UI texts are inactive at the start
        GameObject[] uiTextObjects = GameObject.FindGameObjectsWithTag(uiTextTag);
        if (uiTextObjects != null && uiTextObjects.Length > 0)
        {
            foreach (GameObject uiTextObject in uiTextObjects)
            {
                uiTextObject.SetActive(false);
            }
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (isGraveInteractive && other.CompareTag("Player") && !isDigging && !isCooldownActive)
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
                StartCoroutine(DigGrave(other.GetComponent<FPSController>()));
            }
        }
    }

    private IEnumerator DigGrave(FPSController player)
    {
        isDigging = true; // Set the flag to true to prevent multiple simultaneous dig actions

        // Play a random sound from the audio sources
        if (audioSources.Length > 0)
        {
            int randomIndex = Random.Range(0, audioSources.Length);
            audioSources[randomIndex].Play();
        }

        // Temporarily deactivate the specified object (e.g., player's gun)
        if (objectToDeactivate != null)
        {
            objectToDeactivate.SetActive(false);
        }

        // Activate the specified object (e.g., digging tool)
        if (objectToActivate != null)
        {
            objectToActivate.SetActive(true);
        }

        yield return new WaitForSeconds(0.5f); // Simulate time taken to dig

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

        // Spawn a power-up (always spawns one of the three)
        SpawnPowerUp(player);

        // Deactivate the digging tool after the interaction
        if (objectToActivate != null)
        {
            objectToActivate.SetActive(false);
        }

        // Reactivate the previously deactivated object (e.g., player's gun)
        if (objectToDeactivate != null)
        {
            objectToDeactivate.SetActive(true);
        }

        isDigging = false; // Reset the flag after digging is complete

        // Start the cooldown to prevent multiple power-up spawns
        StartCoroutine(Cooldown());
    }

    private IEnumerator Cooldown()
    {
        isCooldownActive = true;
        yield return new WaitForSeconds(2f); // Adjust the cooldown time as needed
        isCooldownActive = false;
    }

    void SpawnPowerUp(FPSController player)
    {
        int powerUpType = Random.Range(0, 3);
        GameObject powerUpPrefab = null;

        switch (powerUpType)
        {
            case 0:
                Debug.Log("Health power-up activated!");
                powerUpPrefab = healthPowerUpPrefab;
                break;
            case 1:
                Debug.Log("Sprint power-up activated!");
                powerUpPrefab = sprintPowerUpPrefab;
                break;
            case 2:
                Debug.Log("Double shot power-up activated!");
                powerUpPrefab = doubleShotPowerUpPrefab;
                break;
        }

        // Instantiate the power-up prefab at the specified spawn point or at the grave's position if spawn point is not set
        if (powerUpPrefab != null)
        {
            Vector3 spawnPosition = powerUpSpawnPoint != null ? powerUpSpawnPoint.position : graveObject.transform.position;
            GameObject powerUp = Instantiate(powerUpPrefab, spawnPosition, Quaternion.identity);

            // Display power-up activated message
            ShowPowerUpActivatedMessage(powerUp);
        }
    }

    void ShowPowerUpActivatedMessage(GameObject powerUp)
    {
        GameObject[] uiTextObjects = GameObject.FindGameObjectsWithTag(uiTextTag);
        if (uiTextObjects != null && uiTextObjects.Length > 0)
        {
            foreach (GameObject uiTextObject in uiTextObjects)
            {
                // Activate the UI text component to display the power-up activated message
                uiTextObject.GetComponent<TextMesh>().text = powerUp.name + " activated!";
                uiTextObject.SetActive(true);

                // Deactivate the UI text component after a short delay
                StartCoroutine(DeactivateUIText(uiTextObject));
            }
        }
    }

    IEnumerator DeactivateUIText(GameObject uiTextObject)
    {
        yield return new WaitForSeconds(2f); // Adjust the delay time as needed
        uiTextObject.SetActive(false);
    }
}
