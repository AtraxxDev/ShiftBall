using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{

    public static ScoreManager Instance { get; private set; }
    public int Score { get; private set; }
    public int HighScore { get; private set; }

    [SerializeField] private float scoreInterval = 1f;
    private float scoreTimer;

    public delegate void ScoreChanged(int newScore);
    public event ScoreChanged OnScoreChanged;

    public delegate void HighScoreChanged(int newHighScore);
    public event HighScoreChanged OnHighScoreChanged;


    

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

        HighScore = PlayerPrefs.GetInt("HighScore",0);
    }


    // Start is called before the first frame update
    void Start()
    {
        Score = 0;
        scoreTimer = 0;

        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance == null) return;
        if (GameManager.Instance.IsPaused) return;
        
        scoreTimer += Time.deltaTime;
        if (scoreTimer > scoreInterval)
        {
            AddScore(1);
            scoreTimer = 0;
        }
    }

    public void AddScore(int amount)
    {
        // El score va aumentando
        Score += amount;
        // Se invoca la funcion
        OnScoreChanged.Invoke(Score);


        if (Score > HighScore) // si es mayor el score que l highscore
        {
            HighScore = Score; // se iiguala 
            PlayerPrefs.SetInt("HighScore", HighScore); // se establece en highscore
            PlayerPrefs.Save(); // se guarda el pref 
            OnHighScoreChanged?.Invoke(HighScore);
        }
    }

    public void ResetScore()
    {
        Score = 0;
        OnScoreChanged?.Invoke(Score);
    }

    

}
