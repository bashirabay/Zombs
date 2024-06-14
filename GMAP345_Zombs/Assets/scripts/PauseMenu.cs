using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GamePaused = false;
    public GameObject pauseMenuUI;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (GamePaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        if (pauseMenuUI != null)
        {
            pauseMenuUI.SetActive(false);
            Time.timeScale = 1f;
            GamePaused = false;
            Cursor.lockState = CursorLockMode.Locked; // Lock cursor
            Cursor.visible = false; // Hide cursor
        }
        else
        {
            Debug.LogWarning("PauseMenuUI is not assigned in the inspector.");
        }
    }

    private void Pause()
    {
        if (pauseMenuUI != null)
        {
            pauseMenuUI.SetActive(true);
            Time.timeScale = 0f;
            GamePaused = true;
            Cursor.lockState = CursorLockMode.None; // Unlock cursor
            Cursor.visible = true; // Show cursor
        }
        else
        {
            Debug.LogWarning("PauseMenuUI is not assigned in the inspector.");
        }
    }

    public void Retry()
    {
        Time.timeScale = 1f; // Unpause the game directly
        Scene current = SceneManager.GetActiveScene();
        SceneManager.LoadScene(current.name);
    }

    public void QuitGame()
    {
        Debug.Log("Quitting game to main menu");
        SceneManager.LoadScene(0);
    }

    // Method to explicitly unpause the game
    public void UnpauseGame()
    {
        Resume();
    }
}
