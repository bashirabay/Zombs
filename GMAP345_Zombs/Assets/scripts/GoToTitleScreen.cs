using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToTitleScreen : MonoBehaviour
{
    public string titleScreenSceneName; // Set this in the inspector to the name of your title screen scene

    public void GoToTitle()
    {
        SceneManager.LoadScene(titleScreenSceneName);
    }
}
