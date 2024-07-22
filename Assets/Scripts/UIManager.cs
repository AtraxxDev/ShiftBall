using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public TMP_Text text_Score;
    public TMP_Text text_HighScore;


    private void Start()
    {
        ScoreManager.Instance.OnScoreChanged += UpdateScore;
        ScoreManager.Instance.OnHighScoreChanged += UpdateHighScore;

        UpdateHighScore(ScoreManager.Instance.HighScore);
    }

    private void OnDisable()
    {
        ScoreManager.Instance.OnScoreChanged -= UpdateScore;
        ScoreManager.Instance.OnHighScoreChanged -= UpdateHighScore;
    }



    public void UpdateScore(int newScore)
    {
        text_Score.text = "" + newScore;
    }

    public void UpdateHighScore(int newHighScore)
    {
        text_HighScore.text = "HIGHSCORE: " + newHighScore;
    }
}
