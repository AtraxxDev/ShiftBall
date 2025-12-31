using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UICountdown : MonoBehaviour
{
    [SerializeField] private Image fillImage;
    [SerializeField] private TMP_Text countdownText;
    [SerializeField] private CountdownSlider timer;

    private void OnEnable()
    {
        timer.OnTimeChanged += UpdateUI;
        timer.OnTimerCompleted += OnCompleted;
    }

    private void OnDisable()
    {
        timer.OnTimeChanged -= UpdateUI;
        timer.OnTimerCompleted -= OnCompleted;
    }

    private void UpdateUI(float normalized)
    {
        fillImage.fillAmount = 1 - normalized;

        float remainingSeconds = (1 - normalized) * timer.Duration;
        countdownText.text = Mathf.Ceil(remainingSeconds).ToString();
    }



    private void OnCompleted()
    {
        countdownText.text = "0";
    }
}
