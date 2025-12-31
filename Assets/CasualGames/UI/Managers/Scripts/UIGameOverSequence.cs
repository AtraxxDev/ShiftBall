using System.Collections;
using UnityEngine;

public class UIGameOverSequence: MonoBehaviour
{
    [Header("Panels")]
    [SerializeField] private GameObject livesPanel;
    [SerializeField] private GameObject scorePanel;

    [Header("Timer")]
    [SerializeField] private CountdownSlider timer;

    [Header("Settings")]
    [SerializeField] private float delayBeforeShow = 2f;

    public void Play()
    {
        StopAllCoroutines();
        StartCoroutine(Sequence());
    }

    private IEnumerator Sequence()
    {
        yield return new WaitForSeconds(delayBeforeShow);

        AudioManager.Instance.PlaySFX("Lose");
        
        if (timer.Counter >= timer.MaxCounter)
        {
            timer.StopCountdown();
            ShowScore();
        }
        else
        {
            timer.StartCountdown();
            ShowLives();
        }

    }

    private void ShowLives()
    {
        livesPanel.SetActive(true);
        scorePanel.SetActive(false);
        timer.StartCountdown();

        Animate(livesPanel);
    }

    private void ShowScore()
    {
        timer.StopCountdown();
        livesPanel.SetActive(false);
        scorePanel.SetActive(true);

        Animate(scorePanel);
    }

    private void Animate(GameObject panel)
    {
        panel.transform.localScale = Vector3.zero;
        LeanTween.scale(panel, Vector3.one, 0.4f)
            .setEase(LeanTweenType.easeOutBack);
    }
}