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
    [SerializeField] private TMP_Text text_GameOverTotalCoins;

    [Header("Stars")]
    [SerializeField] private TMP_Text text_TotalStarsMenu;
    [SerializeField] private TMP_Text text_GameOverStars;

    [Header("UI")]
    [SerializeField] private GameObject P_GameOver;
    [SerializeField] private GameObject P_InGame;

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
        CoinManager.Instance.OnStarsCollectChanged += UpdateStarsCollected;

        GameManager.Instance.OnGameOver += ShowGameOverUI;
    }

    private void UnsubscribeFromEvents()
    {
        ScoreManager.Instance.OnScoreChanged -= UpdateScore;
        ScoreManager.Instance.OnHighScoreChanged -= UpdateHighScore;
        CoinManager.Instance.OnCoinsChanged -= UpdateTotalCoins;
        CoinManager.Instance.OnCoinsCollectChanged -= UpdateCoinsCollected;
        CoinManager.Instance.OnStarsChanged -= UpdateTotalStars;
        CoinManager.Instance.OnStarsCollectChanged -= UpdateStarsCollected;
        GameManager.Instance.OnGameOver -= ShowGameOverUI;
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
        UpdateText(text_GameOverTotalCoins, newCoins);
    }

    public void UpdateCoinsCollected(int newCoinsCollected)
    {
        UpdateText(text_GameOverCoins, newCoinsCollected);
    }

    public void UpdateTotalStars(int newStars)
    {
        UpdateText(text_TotalStarsMenu, newStars);
    }

    public void UpdateStarsCollected(int newStarsCollected)
    {
        UpdateText(text_GameOverStars, newStarsCollected);
    }

    public void ShowGameOverUI()
    {
        P_InGame.SetActive(false);
        P_GameOver.SetActive(true);
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
