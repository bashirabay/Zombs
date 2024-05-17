using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FlashController : MonoBehaviour
{
    public Image flashImage;
    public float flashDuration = 0.5f;

    private void Start()
    {
        if (flashImage != null)
        {
            flashImage.color = new Color(flashImage.color.r, flashImage.color.g, flashImage.color.b, 0);
        }
    }

    public void Flash()
    {
        StartCoroutine(FlashRoutine());
    }

    private IEnumerator FlashRoutine()
    {
        float elapsedTime = 0f;
        while (elapsedTime < flashDuration)
        {
            float alpha = Mathf.PingPong(elapsedTime * 2f / flashDuration, 1f);
            flashImage.color = new Color(flashImage.color.r, flashImage.color.g, flashImage.color.b, alpha);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        flashImage.color = new Color(flashImage.color.r, flashImage.color.g, flashImage.color.b, 0);
    }
}