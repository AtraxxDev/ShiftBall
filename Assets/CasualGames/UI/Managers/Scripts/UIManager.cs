using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class UIManager : MonoBehaviour
{
    [Header("Score")]
    [SerializeField] private TMP_Text text_Score;
    [SerializeField] private TMP_Text text_HighScore;
    [SerializeField] private TMP_Text text_GameOverScore;
    [SerializeField] private TMP_Text text_GameOverHighScore;

    [Header("Coins")]
    [SerializeField] private TMP_Text text_TotalCoinsMenu;
    [SerializeField] private TMP_Text text_GameOverCoins;

    [Header("Stars")]
    [SerializeField] private TMP_Text text_TotalStarsMenu;

    [Header("UI")]
    [SerializeField] private GameObject P_GameOver;
    [SerializeField] private GameObject P_InGame;
    [SerializeField] private GameObject ButtonPause;

    [SerializeField] private GameObject P_Lives;
    [SerializeField] private GameObject P_Score;

    [Header("Countdown Slider")]
    [SerializeField] private Image filledImage;
    [SerializeField] private TMP_Text countdownText;
    [SerializeField] private CountdownSlider timer;

    [Header("Combo")]
    [SerializeField] private TMP_Text text_Combo;


    private void Start()
    {
        SubscribeToEvents();
        InitializeUI();

    }

    private void OnDisable()
    {
        UnsubscribeFromEvents();
    }

    private void SubscribeToEvents()
    {
       

        ScoreManager.Instance.OnScoreChanged += UpdateScore;
        ScoreManager.Instance.OnHighScoreChanged += UpdateHighScore;

        CoinManager.Instance.OnCoinsChanged += UpdateTotalCoins;
        CoinManager.Instance.OnCoinsCollectChanged += UpdateCoinsCollected;
        CoinManager.Instance.OnStarsChanged += UpdateTotalStars;

        GameManager.Instance.OnGameOver += StartGameOverSequence;

        ComboManager.Instance.OnComboChanged += UpdateComboText;

        GameManager.Instance.OnRevivePlayer += CloseGameOver;

        timer.OnTimeChanged += UpdateCounter;
        timer.OnTimerCompleted += OnCountdownComplete;
    }

    private void UnsubscribeFromEvents()
    {
        ScoreManager.Instance.OnScoreChanged -= UpdateScore;
        ScoreManager.Instance.OnHighScoreChanged -= UpdateHighScore;
        CoinManager.Instance.OnCoinsChanged -= UpdateTotalCoins;
        CoinManager.Instance.OnCoinsCollectChanged -= UpdateCoinsCollected;
        CoinManager.Instance.OnStarsChanged -= UpdateTotalStars;
        GameManager.Instance.OnGameOver -= StartGameOverSequence;

        ComboManager.Instance.OnComboChanged -= UpdateComboText;

        timer.OnTimeChanged -= UpdateCounter;
        timer.OnTimerCompleted -= OnCountdownComplete;
    }

    private void InitializeUI()
    {
        UpdateHighScore(ScoreManager.Instance.HighScore);
        UpdateTotalCoins(CoinManager.Instance.Coins);
        UpdateTotalStars(CoinManager.Instance.Stars);
    }

    private void UpdateText(TMP_Text textElement, int newValue, string prefix = "")
    {
        textElement.text = $"{prefix}{newValue.ToString()}";
    }

    public void UpdateScore(int newScore)
    {
        UpdateText(text_Score, newScore);
        UpdateText(text_GameOverScore, newScore);
    }

    public void UpdateHighScore(int newHighScore)
    {
        UpdateText(text_HighScore, newHighScore, "HIGHSCORE: ");
        UpdateText(text_GameOverHighScore, newHighScore);
    }

    public void UpdateTotalCoins(int newCoins)
    {
        UpdateText(text_TotalCoinsMenu, newCoins);
    }

    public void UpdateCoinsCollected(int newCoinsCollected)
    {
        UpdateText(text_GameOverCoins, newCoinsCollected, "+ ");
    }

    public void UpdateTotalStars(int newStars)
    {
        UpdateText(text_TotalStarsMenu, newStars);
    }

    private void UpdateComboText(int newCombo)
    {
        if (newCombo > 0)
        {
            text_Combo.text = "Combo x" + newCombo;

            // Resetea la escala inicial para evitar acumulación de animaciones
            text_Combo.transform.localScale = Vector3.one;

            // Efecto pequeño de "pum"
            float scaleMultiplier = 1.1f; // Incremento pequeño
            if (newCombo % 5 == 0) // Efecto más grande cada 5 combos
            {
                scaleMultiplier = 1.3f;
            }

            LeanTween.scale(text_Combo.gameObject, Vector3.one * scaleMultiplier, 0.2f)
                     .setEase(LeanTweenType.easeOutBack)
                     .setOnComplete(() =>
                     {
                         // Vuelve a su tamaño original
                         LeanTween.scale(text_Combo.gameObject, Vector3.one, 0.2f)
                                  .setEase(LeanTweenType.easeInOutQuad);
                     });
        }
        else
        {
            // Efecto "pop-up end" cuando el combo llega a 0
            LeanTween.scale(text_Combo.gameObject, Vector3.zero, 0.3f)
                     .setEase(LeanTweenType.easeInBack)
                     .setOnComplete(() =>
                     {
                         // Borra el texto cuando la animación termine
                         text_Combo.text = "";
                     });
        }
    }


    private void UpdateCounter(float normalizedTime)
    {
        filledImage.fillAmount = 1 - normalizedTime;
        countdownText.text = Mathf.Ceil(normalizedTime * timer.countdownDuration).ToString();
    }

    private void OnCountdownComplete()
    {
        countdownText.text = "0";
        Debug.Log("¡Cuenta regresiva finalizada!");
        DesInitilizePopUp();
    }


    public void CloseGameOver()
    {
        P_Lives.SetActive(false);
        P_Score.SetActive(false);
        P_GameOver.SetActive(false);
        P_InGame.SetActive(true);
    }

    private void StartGameOverSequence()
    {
        StartCoroutine(ShowGameOverAfterDelay(2f)); // Llama a la corrutina con un retraso de 2 segundos
    }

    private IEnumerator ShowGameOverAfterDelay(float delay)
    {
        ButtonPause.SetActive(false);
        yield return new WaitForSeconds(delay);
        ShowGameOverUI();

  

    }

    private void ShowGameOverUI()
    {
        // Ocultar el panel de juego
        P_InGame.SetActive(false);

        P_GameOver.SetActive(true);

        if (timer.GetCounter() >= timer.GetMaxCounter())
        {
            timer.StopCoroutine(timer.countdownCoroutine);
            // Mostrar panel de puntaje si ya se alcanzaron los intentos
            Debug.Log("Contador máximo alcanzado. Mostrando panel de puntaje.");
            P_Score.SetActive(true);
        }
        else
        {
            // Mostrar panel de vidas y reiniciar la cuenta regresiva
            Debug.Log("Mostrando panel de vidas.");
            timer.StartCountdown();
            P_Lives.SetActive(true);
        }

        // Animación del panel Game Over
        P_GameOver.transform.localScale = Vector3.zero;
        LeanTween.scale(P_GameOver, Vector3.one, 0.5f)
                 .setEase(LeanTweenType.easeOutBack);
    }

    public void StartGame()
    {
        GameManager.Instance.StartGame();
        ButtonPause.SetActive(true);
    }

    public void Restart()
    {
        GameManager.Instance.Restart();
    }

    public void ReturnMainMenu()
    {
        GameManager.Instance.ReturnToMainMenu();
    }


    public void DesInitilizePopUp()
    {
        timer.StopCoroutine(timer.countdownCoroutine);
        PopUpEnd(P_Lives);
        PopUpStart(P_Score);
    }

    public void InitilizePopUp()
    {
        PopUpStart(P_Score);
        PopUpEnd(P_Lives);
    }

    public void PopUpStart(GameObject _object)
    {
        StartCoroutine(startPopUpAnim(_object));

    }

    public void PopUpEnd(GameObject _object)
    {
        StartCoroutine(endPopUpAnim(_object));
    }







    private IEnumerator startPopUpAnim(GameObject _object)
    {
        _object.transform.localScale = Vector3.zero;
        yield return new WaitForSeconds(0.8f);
        _object.SetActive(true);
        LeanTween.scale(_object, Vector3.one, 0.3f)
            .setEase(LeanTweenType.easeOutBack);


    }

    private IEnumerator endPopUpAnim(GameObject _object)
    {
        LeanTween.scale(_object, Vector3.zero, 0.3f)
            .setEase(LeanTweenType.easeInBack);

        yield return new WaitForSeconds(0.3f);
        _object.SetActive(false);

    }

}
