using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class UIManager : MonoBehaviour
{
    [Header("Score")]
    public TMP_Text text_Score;
    public TMP_Text text_HighScore;
    public TMP_Text text_GameOverScore;
    public TMP_Text text_GameOverHighScore;

    [Header("Coins")]
    public TMP_Text text_TotalCoinsMenu;
    public TMP_Text text_GameOverCoins;
    public TMP_Text text_GameOverTotalCoins;

    [Header("UI")]
    public GameObject P_GameOver;
    public GameObject P_InGame;

    private void Start()
    {
        ScoreManager.Instance.OnScoreChanged += UpdateScore;
        ScoreManager.Instance.OnHighScoreChanged += UpdateHighScore;

        CoinManager.Instance.OnCoinsChanged += UpdateTotalCoins;
        CoinManager.Instance.OnCoinsCollectChanged += UpdateCoinsCollected;

        GameManager.Instance.OnGameOver += ShowGameOverUI;

        UpdateHighScore(ScoreManager.Instance.HighScore);
        UpdateTotalCoins(CoinManager.Instance.TotalCoins);
    }

    private void OnDisable()
    {
        ScoreManager.Instance.OnScoreChanged -= UpdateScore;
        ScoreManager.Instance.OnHighScoreChanged -= UpdateHighScore;

        CoinManager.Instance.OnCoinsChanged -= UpdateTotalCoins;
        CoinManager.Instance.OnCoinsCollectChanged -= UpdateCoinsCollected;

        GameManager.Instance.OnGameOver -= ShowGameOverUI;

    }


    public void UpdateScore(int newScore)
    {
        text_Score.text = newScore.ToString();
        text_GameOverScore.text = newScore.ToString();
    }

    public void UpdateHighScore(int newHighScore)
    {
        text_HighScore.text = $"HIGHSCORE: { newHighScore}";
        text_GameOverHighScore.text = newHighScore.ToString();
    }

    public void UpdateTotalCoins(int newCoins)
    {
        text_TotalCoinsMenu.text = newCoins.ToString();
        text_GameOverTotalCoins.text = newCoins.ToString();
    }

    public void UpdateCoinsCollected(int newCoinsCollecetd)
    {
        text_GameOverCoins.text = newCoinsCollecetd.ToString(); ;
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
