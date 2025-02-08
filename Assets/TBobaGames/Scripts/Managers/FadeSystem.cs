using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeSystem : MonoBehaviour
{
    [BoxGroup("Fade Panel Setup")]
    [Required, LabelText("Canvas Group Panel"), Tooltip("Assign the CanvasGroup component for the fade effect.")]
    [SerializeField] private CanvasGroup fadePanel;

    [BoxGroup("Fade Settings")]
    [LabelWidth(120), Tooltip("Duration of the fade transition.")]
    [SerializeField, Range(0.1f, 5f)] private float fadeDuration = 1f;

    private void Awake()
    {
        if (fadePanel == null)
        {
            Debug.LogError("Fade Panel is not assigned. Please attach a CanvasGroup.");
            return;
        }
        fadePanel.alpha = 0f; // Start fully faded in for initialization
    }

    [Button(ButtonSizes.Large), GUIColor(0.2f, 0.8f, 0.2f)]
    public void FadeIn(System.Action onComplete = null)
    {
        fadePanel.gameObject.SetActive(true);
        LeanTween.alphaCanvas(fadePanel, 1f, fadeDuration).setOnComplete(() =>
        {
            onComplete?.Invoke();
        });
    }

    [Button(ButtonSizes.Large), GUIColor(0.8f, 0.2f, 0.2f)]
    public void FadeOut(System.Action onComplete = null)
    {
        LeanTween.alphaCanvas(fadePanel, 0f, fadeDuration).setOnComplete(() =>
        {
            fadePanel.gameObject.SetActive(false);
            onComplete?.Invoke();
        });
    }

    [Button(ButtonSizes.Large), GUIColor(0.2f, 0.5f, 1f)]
    public void FadeInOut(float waitTime, System.Action onComplete = null)
    {
        FadeIn(() =>
        {
            Invoke(nameof(StartFadeOut), waitTime);
            onComplete?.Invoke();
        });
    }

    private void StartFadeOut()
    {
        FadeOut();
    }

    public void StartFadeInOut()
    {
        FadeInOut(0.5f);
    }


}
