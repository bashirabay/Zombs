using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public int score = 0;
    public TextMeshProUGUI scoreText; // Reference to the TextMeshProUGUI element for displaying the score
    public TextMeshProUGUI interactionText; // Reference to the TextMeshProUGUI element for displaying interaction messages
    public TextMeshProUGUI zombiesKilledText; // Reference to the TextMeshProUGUI element for displaying the number of zombies killed

    private int zombiesKilled = 0;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded; // Subscribe to the sceneLoaded event
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    void OnDestroy()
    {
        if (instance == this)
        {
            SceneManager.sceneLoaded -= OnSceneLoaded; // Unsubscribe from the sceneLoaded event
        }
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Reassign the TextMeshProUGUI references after the scene is loaded
        scoreText = GameObject.Find("ScoreText")?.GetComponent<TextMeshProUGUI>();
        interactionText = GameObject.Find("InteractionText")?.GetComponent<TextMeshProUGUI>();
        zombiesKilledText = GameObject.Find("ZombiesKilledText")?.GetComponent<TextMeshProUGUI>();

        // Update the UI with the current values
        UpdateScoreText();
        UpdateZombiesKilledText();
    }

    void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score.ToString();
            StartCoroutine(FlashText(scoreText));
        }
    }

    public void ShowInteractionText(string message)
    {
        if (interactionText != null)
        {
            interactionText.text = message;
            interactionText.gameObject.SetActive(true);
        }
    }

    public void HideInteractionText()
    {
        if (interactionText != null)
        {
            interactionText.gameObject.SetActive(false);
        }
    }

    public void AddScore(int points)
    {
        score += points;
        UpdateScoreText();
    }

    public void SetScore(int newScore)
    {
        score = newScore;
        UpdateScoreText();
    }

    public void AddZombieKill()
    {
        zombiesKilled += 1;
        UpdateZombiesKilledText();
        StartCoroutine(FlashText(zombiesKilledText));
    }

    private void UpdateZombiesKilledText()
    {
        if (zombiesKilledText != null)
        {
            zombiesKilledText.text = "Zombies Killed: " + zombiesKilled.ToString();
        }
    }

    private IEnumerator FlashText(TextMeshProUGUI text)
    {
        if (text != null)
        {
            Color originalColor = text.color;
            for (int i = 0; i < 3; i++)
            {
                text.color = Color.yellow; // Change to any color you want for flashing
                yield return new WaitForSeconds(0.1f);
                text.color = originalColor;
                yield return new WaitForSeconds(0.1f);
            }
        }
    }

    // Method to reset the game state
    public void ResetGame()
    {
        score = 0;
        zombiesKilled = 0;
        UpdateScoreText();
        UpdateZombiesKilledText();
    }
}
