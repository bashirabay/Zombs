using UnityEngine;
using TMPro;

public class GraveInteraction : MonoBehaviour
{
    public int minPoints = 10;
    public int maxPoints = 50;
    public int pointIncrement = 5;

    bool interacted = false;
    bool canInteract = false;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canInteract = true;
            GameManager.instance.ShowInteractionText("Right click to rob the grave!");
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canInteract = false;
            GameManager.instance.HideInteractionText();
        }
    }

    void OnMouseOver()
    {
        if (canInteract && Input.GetMouseButtonDown(1) && !interacted)
        {
            OpenGrave();
        }
    }

    void OpenGrave()
    {
        interacted = true;
        int points = Random.Range(minPoints, maxPoints + 1);
        points -= points % pointIncrement; // Ensure points are in sequences of five
        GameManager.instance.AddScore(points);
        Destroy(gameObject);
    }
}
