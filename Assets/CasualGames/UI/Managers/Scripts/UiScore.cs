using TMPro;
using UnityEngine;

public class UiScore : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text highScoreText;

    private void Start()
    {
        ScoreManager.Instance.OnScoreChanged += UpdateScore;
        ScoreManager.Instance.OnHighScoreChanged += UpdateHighScore;
    }

    private void OnDisable()
    {
        ScoreManager.Instance.OnScoreChanged -= UpdateScore;
        ScoreManager.Instance.OnHighScoreChanged -= UpdateHighScore;
    }

    private void UpdateScore(int value)
    {
        scoreText.text = value.ToString();
    }

    private void UpdateHighScore(int value)
    {
        highScoreText.text = $"HIGHSCORE: {value}";
    }
}
