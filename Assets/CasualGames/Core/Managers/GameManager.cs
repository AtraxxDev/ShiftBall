using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.SceneManagement;
using TB_Tools;

public class GameManager : MonoBehaviour
{
    public bool IsPaused => 
        CurrentState == GameState.Paused ||
        CurrentState == GameState.GameOver;

    public static GameManager Instance { get; private set; }

    [ShowInInspector ]
    public GameState CurrentState { get; private set; }

    public event Action<GameState> OnStateChanged;
    public event Action OnGameOver;
    public event Action OnRevivePlayer;
    public event Action OnRestartGame;

    public static bool IsVibrationEnabled = true;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        Application.targetFrameRate = 60;
        QualitySettings.vSyncCount = 0;

        AudioManager.Instance.PlayMusic("Menu");
        SetState(GameState.Paused);
    }

    public void StartGame()
    {
        if (CurrentState != GameState.Paused) return;

        CoinManager.Instance.ResetCoins_StarsCollected();
        SetState(GameState.Playing);
    }

    public void PauseGame()
    {
        SetState(GameState.Paused);
    }

    public void ResumeGame()
    {
        SetState(GameState.Playing);
    }

    public void GameOver()
    {
        if (IsVibrationEnabled)
            Handheld.Vibrate();

        SetState(GameState.GameOver);

        GameEvents.RaiseGameOverParticles();
        OnGameOver?.Invoke();
    }

    public void RevivePlayer()
    {
        OnRevivePlayer?.Invoke();
        SetState(GameState.Playing);
    }

    public void Restart()
    {
        ScoreManager.Instance.ResetScore();
        OnRestartGame?.Invoke();
        SetState(GameState.Paused);
        StartGame();
    }

    public void ReturnToMainMenu()
    {
        ScoreManager.Instance.ResetScore();
        SetState(GameState.Paused);
    }
    
    public void SetState(GameState newState)
    {
        if (CurrentState == newState) return;

        CurrentState = newState;
        OnStateChanged?.Invoke(newState);
    }

}
