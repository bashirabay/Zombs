using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{

    public void StartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        FindObjectOfType<PauseMenu>().UnpauseGame(); // Unpause the game
    }

    public void replay()
    {
        SceneManager.LoadScene(1);
    }


    public void startMenu()
    {
        SceneManager.LoadScene(0);
    }


    public void QuitGame()
    {
        Debug.Log("QUIT");
        Application.Quit();
    }

}
