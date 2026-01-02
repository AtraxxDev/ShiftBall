using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIGameOverScore : MonoBehaviour
{
    [SerializeField] private TMP_Text titleText;
    [SerializeField] private TMP_Text valueText;
    [SerializeField] private Image _image;
    
    public void UpdateResult()
    {
        int score = ScoreManager.Instance.Score;
        int highScore = ScoreManager.Instance.HighScore;

        if (score >= highScore)
        {
            titleText.text = "Best Score";
            valueText.text = highScore.ToString();
            _image.enabled = true;
        }
        else
        {
            titleText.text = "Score";
            _image.enabled = false;
            valueText.text = score.ToString();
        }
    }
}