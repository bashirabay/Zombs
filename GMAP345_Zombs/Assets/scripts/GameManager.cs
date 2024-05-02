using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public int score;
    public TextMeshProUGUI scoreText; // Reference to the TextMeshProUGUI element for displaying the score
    public TextMeshProUGUI interactionText; // Reference to the TextMeshProUGUI element for displaying interaction messages

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    public void AddScore(int points)
    {
        score += points;
        UpdateScoreText();
    }

    public void ShowInteractionText(string message)
    {
        interactionText.text = message;
        interactionText.gameObject.SetActive(true);
    }

    public void HideInteractionText()
    {
        interactionText.gameObject.SetActive(false);
    }

    void UpdateScoreText()
    {
        scoreText.text = "Score: " + score.ToString();
    }
}
