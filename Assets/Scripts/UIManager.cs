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

    [Header("Coins")]
    public TMP_Text text_Coins;
    public TMP_Text text_CoinsCollected;

    private void Start()
    {
        ScoreManager.Instance.OnScoreChanged += UpdateScore;
        ScoreManager.Instance.OnHighScoreChanged += UpdateHighScore;

        CoinManager.Instance.OnCoinsChanged += UpdateCoins;
        CoinManager.Instance.OnCoinsCollectChanged += UpdateCoinsCollected;

        UpdateHighScore(ScoreManager.Instance.HighScore);
        UpdateCoins(CoinManager.Instance.Coins);
    }

    private void OnDisable()
    {
        ScoreManager.Instance.OnScoreChanged -= UpdateScore;
        ScoreManager.Instance.OnHighScoreChanged -= UpdateHighScore;
        CoinManager.Instance.OnCoinsChanged += UpdateCoins;
        CoinManager.Instance.OnCoinsCollectChanged += UpdateCoinsCollected;
    }

    
    public void UpdateScore(int newScore)
    {
        text_Score.text = newScore.ToString();
    }

    public void UpdateHighScore(int newHighScore)
    {
        text_HighScore.text = $"HIGHSCORE: { newHighScore}";
    }

    public void UpdateCoins(int newCoins)
    {
        text_Coins.text =newCoins.ToString();
    }

    public void UpdateCoinsCollected(int newCoinsCollecetd)
    {
        text_CoinsCollected.text = newCoinsCollecetd.ToString(); ;
    }
}
