using System;
using System.Collections;
using Sirenix.OdinInspector;
using UnityEngine;

public class UIGameOverSequence : MonoBehaviour
{
    [Header("Root Panel")]
    [SerializeField] private RectTransform rootPanel;

    [Header("Sub Panels")]
    [SerializeField] private GameObject livesPanel;
    [SerializeField] private GameObject scorePanel;

    [Header("Timer")]
    [SerializeField] private CountdownSlider timer;
    [SerializeField] private UIGameOverScore UIGameOverScore;

    [Header("Settings")]
    [SerializeField] private float delayBeforeShow = 1.5f;
    [SerializeField] private float slideDuration = 0.4f;
    

    private Vector2 hiddenPos;
    private Vector2 visiblePos;

    private void Awake()
    {
        visiblePos = rootPanel.anchoredPosition;
        hiddenPos = visiblePos + Vector2.right * 1125f; // fuera de pantalla
        rootPanel.anchoredPosition = hiddenPos;
        rootPanel.gameObject.SetActive(false);
        timer.OnTimerCompleted += Hide;
    }

    private void Start()
    {
        GameManager.Instance.OnStartGame += ResetCounter;
    }


    private void OnDisable()
    {
        timer.OnTimerCompleted -= Hide;
        GameManager.Instance.OnStartGame -= ResetCounter;

    }
    private void ResetCounter()
    {
        timer.ResetCounter();
    }

    [Button]
    public void Play()
    {
        StopAllCoroutines();
        StartCoroutine(Sequence());
    }

    private IEnumerator Sequence()
    {
        
        yield return new WaitForSeconds(delayBeforeShow); // tiempo de espera cunado muere el jugador
        
        AudioManager.Instance.PlaySFX("Lose");
        rootPanel.gameObject.SetActive(true);
        SlideIn();

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
    }

    private void ShowScore()
    {
        livesPanel.SetActive(false);
        scorePanel.SetActive(true);
        UIGameOverScore.UpdateResult();
    }

    private void SlideIn()
    {
        rootPanel.anchoredPosition = hiddenPos;
        LeanTween.move(rootPanel, visiblePos, slideDuration)
                 .setEase(LeanTweenType.easeOutCubic);
    }

    [Button]
    public void Hide()
    {
        timer.StopCountdown();
        LeanTween.move(rootPanel, hiddenPos, slideDuration)
                 .setEase(LeanTweenType.easeInCubic)
                 .setOnComplete(() =>
                 {
                     //rootPanel.gameObject.SetActive(false);
                     SlideIn();
                     ShowScore();
                 });
    }
}
