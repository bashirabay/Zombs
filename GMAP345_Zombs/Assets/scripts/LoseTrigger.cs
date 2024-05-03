using UnityEngine;

public class LoseTrigger : MonoBehaviour
{
    public GameObject loseScreen; // Reference to the lose screen UI canvas

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Zombie"))
        {
            // Pause the game
            Time.timeScale = 0f;

            // Activate the lose screen UI canvas
            loseScreen.SetActive(true);

           
        }
    }
}
