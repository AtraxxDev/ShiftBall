using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }

    public int Score { get; private set; }
    public int HighScore { get; private set; }

    [SerializeField] private float scoreInterval = 1f;
    [SerializeField] private int scoreStep = 200; // 🔥 cada 200 puntos

    private float scoreTimer;
    private int lastStepReached = 0;

    public delegate void ScoreChanged(int newScore);
    public event ScoreChanged OnScoreChanged;

    public delegate void HighScoreChanged(int newHighScore);
    public event HighScoreChanged OnHighScoreChanged;

    // 🔔 NUEVO EVENTO
    public event System.Action<int> OnScoreStepReached;

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

        HighScore = PlayerPrefs.GetInt("HighScore", 0);
    }

    private void Start()
    {
        ResetScore();
    }

    private void Update()
    {
        if (GameManager.Instance == null) return;
        if (GameManager.Instance.IsPaused) return;

        scoreTimer += Time.deltaTime;
        if (scoreTimer >= scoreInterval)
        {
            AddScore(1);
            scoreTimer = 0f;
        }
    }

    public void AddScore(int amount)
    {
        Score += amount;
        OnScoreChanged?.Invoke(Score);

        // 🔔 CHECK DE STEP (200, 400, 600, etc.)
        int currentStep = Score / scoreStep;
        if (currentStep > lastStepReached)
        {
            lastStepReached = currentStep;
            OnScoreStepReached?.Invoke(Score);
        }

        if (Score > HighScore)
        {
            HighScore = Score;
            PlayerPrefs.SetInt("HighScore", HighScore);
            PlayerPrefs.Save();
            OnHighScoreChanged?.Invoke(HighScore);
        }
    }

    public void ResetScore()
    {
        Score = 0;
        lastStepReached = 0;
        scoreTimer = 0f;

        OnScoreChanged?.Invoke(Score);
    }
}
