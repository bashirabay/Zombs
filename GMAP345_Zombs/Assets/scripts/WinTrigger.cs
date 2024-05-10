using UnityEngine;

public class WinTrigger : MonoBehaviour
{
    public GameObject winScreen; // Reference to the win screen UI canvas

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Pause the game
            Time.timeScale = 0f;

            // Activate the win screen UI canvas
            winScreen.SetActive(true);

            // Unlock the cursor
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}
