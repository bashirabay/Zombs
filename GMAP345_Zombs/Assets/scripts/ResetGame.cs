using UnityEngine;
using UnityEngine.SceneManagement; // Add this line to include SceneManager

public class ResetGame : MonoBehaviour
{
    void Update()
    {
        // Check if the player presses the "R" key
        if (Input.GetKeyDown(KeyCode.R))
        {
            // Reload the current scene
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
