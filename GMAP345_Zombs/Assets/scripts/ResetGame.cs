using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetGame : MonoBehaviour
{
    public void ResetGameScene()
    {
        // Reset the time scale in case it was changed (e.g., if the game was paused)
        Time.timeScale = 1f;

        // Reset game state in GameManager
        if (GameManager.instance != null)
        {
            GameManager.instance.ResetGame();
        }

        // Reload the current active scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        // Optionally log the scene reset action for debugging purposes
        Debug.Log("Game scene has been reset.");
    }
}
