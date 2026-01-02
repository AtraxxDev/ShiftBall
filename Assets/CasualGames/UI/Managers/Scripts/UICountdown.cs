using System;
using UnityEngine;
using UnityEngine.UI;

public class UICountdown : MonoBehaviour
{
    [SerializeField] private Image fillImage;
    [SerializeField] private CountdownSlider timer;

    private Button _button;

    private void OnEnable()
    {
        if (timer == null) return;

        timer.OnTimeChanged += UpdateFill;
        timer.OnTimerCompleted += OnCompleted;

        // Sincroniza el estado actual al activarse
        UpdateFill(1f - (timer.RemainingTime / timer.Duration));
    }

    private void OnDisable()
    {
        if (timer == null) return;

        timer.OnTimeChanged -= UpdateFill;
        timer.OnTimerCompleted -= OnCompleted;
    }

    private void Awake()
    {
        fillImage.TryGetComponent(out _button);
    }

    private void UpdateFill(float normalized)
    {
        // Countdown: lleno → vacío
        fillImage.fillAmount = 1f - normalized;
        
        if (_button != null)
            _button.interactable = true;
       
    }

    private void OnCompleted()
    {
        fillImage.fillAmount = 0f;
        if (_button != null)
            _button.interactable = false;
    }
}