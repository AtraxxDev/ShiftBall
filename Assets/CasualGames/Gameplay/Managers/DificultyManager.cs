using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

public class DificultyManager : MonoBehaviour
{
    public static DificultyManager Instance { get; private set; }

     [Title("Progress Settings")] 
     public float maxScoreForFullDifficulty = 1000f;
     public AnimationCurve difficultyCurve = AnimationCurve.Linear(0,0,1,1);

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }    
    }

    public float GetDifficult()
    {
        float normalized = Mathf.Clamp01(ScoreManager.Instance.Score / maxScoreForFullDifficulty);
        return difficultyCurve.Evaluate(normalized);
    }
}
