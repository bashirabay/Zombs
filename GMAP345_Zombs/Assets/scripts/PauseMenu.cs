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
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GamePaused = false;
        Cursor.lockState = CursorLockMode.Locked; // Lock cursor
        Cursor.visible = false; // Hide cursor
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GamePaused = true;
        Cursor.lockState = CursorLockMode.None; // Unlock cursor
        Cursor.visible = true; // Show cursor
    }

    public void Retry()
    {
        Scene current = SceneManager.GetActiveScene();
        SceneManager.LoadScene(current.name);
        Resume(); // Unpause the game
    }

    public void QuitGame()
    {
        SceneManager.LoadScene(0);
        Debug.Log("Quitting game to main menu");
    }

    // Method to explicitly unpause the game
    public void UnpauseGame()
    {
        Resume();
    }
}
