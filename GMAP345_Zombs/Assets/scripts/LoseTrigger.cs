using UnityEngine;

public class LoseTrigger : MonoBehaviour
{
    public GameObject loseScreen; // Reference to the lose screen UI canvas
    public Health health; // Reference to the HealthController script

    void Update()
    {
        // Check if health is 0 or less
        if (health.health <= 0)
        {
            // Pause the game
            Time.timeScale = 0f;

            // Activate the lose screen UI canvas
            loseScreen.SetActive(true);
        }
    }
}
