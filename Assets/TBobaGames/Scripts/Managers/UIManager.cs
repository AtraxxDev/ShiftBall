using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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
        UpdateText(text_GameOverCoins, newCoinsCollected);
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

            // Resetea la escala inicial para evitar acumulaci�n de animaciones
            text_Combo.transform.localScale = Vector3.one;

            // Efecto peque�o de "pum"
            float scaleMultiplier = 1.1f; // Incremento peque�o
            if (newCombo % 5 == 0) // Efecto m�s grande cada 5 combos
            {
                scaleMultiplier = 1.3f;
            }

            LeanTween.scale(text_Combo.gameObject, Vector3.one * scaleMultiplier, 0.2f)
                     .setEase(LeanTweenType.easeOutBack)
                     .setOnComplete(() =>
                     {
                         // Vuelve a su tama�o original
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
                         // Borra el texto cuando la animaci�n termine
                         text_Combo.text = "";
                     });
        }
    }


    private void StartGameOverSequence()
    {
        StartCoroutine(ShowGameOverAfterDelay(2f)); // Llama a la corrutina con un retraso de 2 segundos
    }

    private IEnumerator ShowGameOverAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        ShowGameOverUI();
    }

    private void ShowGameOverUI()
    {
        P_InGame.SetActive(false);

        // Activa el panel antes de animarlo
        P_GameOver.SetActive(true);

        // Resetea la escala inicial para evitar conflictos
        P_GameOver.transform.localScale = Vector3.zero;

        // Anima el panel con un efecto de "pop-up"
        LeanTween.scale(P_GameOver, Vector3.one, 0.5f)
                 .setEase(LeanTweenType.easeOutBack); // Efecto el�stico al aparecer
    }

    public void StartGame()
    {
        GameManager.Instance.StartGame();
    }

    public void ReturnToMainMenu()
    {
        GameManager.Instance.ReturnToMainMenu();
    }
}
