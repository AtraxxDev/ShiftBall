using Sirenix.OdinInspector;
using UnityEngine;
using System;

public class FadeSystem : MonoBehaviour
{
    [BoxGroup("Fade Panel Setup")]
    [Required, LabelText("Canvas Group Panel")]
    [SerializeField] private CanvasGroup fadePanel;

    [BoxGroup("Fade Settings")]
    [LabelWidth(120)]
    [SerializeField, Range(0.1f, 5f)] private float fadeDuration = 1f;

    private void Awake()
    {
        if (fadePanel == null)
        {
            Debug.LogError("Fade Panel is not assigned.");
            return;
        }

        fadePanel.alpha = 0f;
        fadePanel.gameObject.SetActive(false);
    }

    // ------------------ FADE IN ------------------

    [Button(ButtonSizes.Large), GUIColor(0.2f, 0.8f, 0.2f)]
    public void FadeIn(Action onComplete = null)
    {
        fadePanel.gameObject.SetActive(true);

        LeanTween.alphaCanvas(fadePanel, 1f, fadeDuration)
            .setOnComplete(() =>
            {
                onComplete?.Invoke();
            });
    }

    // ------------------ FADE OUT ------------------

    [Button(ButtonSizes.Large), GUIColor(0.8f, 0.2f, 0.2f)]
    public void FadeOut(Action onComplete = null)
    {
        fadePanel.gameObject.SetActive(true);

        LeanTween.alphaCanvas(fadePanel, 0f, fadeDuration)
            .setOnComplete(() =>
            {
                fadePanel.gameObject.SetActive(false);
                onComplete?.Invoke();
            });
    }

    // ------------------ FADE IN → WAIT → FADE OUT ------------------

    [Button(ButtonSizes.Large), GUIColor(0.2f, 0.5f, 1f)]
    public void FadeInOut(float waitTime, Action onFadeInComplete = null)
    {
        FadeIn(() =>
        {
            // After Fade In completes → run your logic
            onFadeInComplete?.Invoke();

            // Then wait before fading out
            Invoke(nameof(StartFadeOut), waitTime);
        });
    }

    private void StartFadeOut()
    {
        FadeOut();
    }
}