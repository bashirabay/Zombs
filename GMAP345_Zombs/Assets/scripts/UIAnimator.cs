using System;
using UnityEngine;
using UnityEngine.EventSystems; // Required for the event system
using DG.Tweening; // Import the DOTween namespace

public class UIAnimator : MonoBehaviour
{
    //menu
    public RectTransform menuPanel; // Assign this to your menu panel
    public Vector2 offScreenPosition; // Set this to the position where the menu starts (off-screen)
    public Vector2 onScreenPosition; // Set this to the position where the menu ends up on screen
    public float animationDuration = 0.5f; // Duration of the fly in animation
    private bool isMenuActive = false;
    
    //buttons
    public RectTransform[] buttons; // Assign your button RectTransforms in the inspector
    public float shakeDuration = 0.5f; // Duration of the shake animation
    public float shakeStrength = 1f; // Strength of the shake animation
    public int shakeVibrato = 10; // Vibrato of the shake animation
    public float shakeRandomness = 90f; // Randomness of the shake animation

    

    private void Start()
    {
        // Start off-screen
        menuPanel.anchoredPosition = offScreenPosition;

        // Set up the EventTriggers for each button
        foreach (RectTransform button in buttons)
        {
            EventTrigger trigger = button.gameObject.GetComponent<EventTrigger>() ?? button.gameObject.AddComponent<EventTrigger>();

            EventTrigger.Entry entryEnter = new EventTrigger.Entry { eventID = EventTriggerType.PointerEnter };
            entryEnter.callback.AddListener((data) => { OnButtonEnter(button); });
            trigger.triggers.Add(entryEnter);

            EventTrigger.Entry entryExit = new EventTrigger.Entry { eventID = EventTriggerType.PointerExit };
            entryExit.callback.AddListener((data) => { OnButtonExit(button); });
            trigger.triggers.Add(entryExit);
        }
    }

    private void OnButtonEnter(RectTransform button)
    {
        // Shake the button using DOTween
        button.DOShakeAnchorPos(shakeDuration, shakeStrength, shakeVibrato, shakeRandomness, false, true).SetUpdate(true);
    }

    private void OnButtonExit(RectTransform button)
    {
        // Optionally add this if you want to stop the animation on exit
        button.DOComplete();
    }
    public void ToggleMenu()
    {
        if (isMenuActive)
        {
            // Animate back off-screen
            menuPanel.DOAnchorPos(offScreenPosition, animationDuration).SetEase(Ease.InBack).SetUpdate(true);
            isMenuActive = false;
        }
        else
        {
            // Animate to the on-screen position
            menuPanel.DOAnchorPos(onScreenPosition, animationDuration).SetEase(Ease.OutBack).SetUpdate(true);
            isMenuActive = true;
        }
    }
}
