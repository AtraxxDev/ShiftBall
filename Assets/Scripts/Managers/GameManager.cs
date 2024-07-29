using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TB_Tools;


public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; set; }

    public GameState CurrentState { get; private set;}

    public Action OnGameOver;
    private bool isPaused = false;

    private PlayerManager playerManager;



    private void Awake()
    {
        if (Instance == null) { Instance = this; }
        else { Destroy(gameObject); }
    }
    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;
        QualitySettings.vSyncCount = 0;
        SetState(GameState.Paused);
    }

    

    public void SetState(GameState newstate)
    {
        CurrentState = newstate;

        switch (newstate)
        {
            case GameState.Playing:
                isPaused = false;
                break;
            case GameState.Paused:
                isPaused = true;
                break;
            case GameState.GameOver:
                isPaused = true;
                break;
            default:
                break;
        }
    }

    public bool IsPaused()
    {
        return isPaused;
    }

    

    public void StartGame()
    {
        if (CurrentState == GameState.Paused)
        {
            ScoreManager.Instance.ResetScore();
            CoinManager.Instance.ResetCoins_StarsCollected();
            SetState(GameState.Playing);
        }
    }

    public void GameOver()
    {
        AudioManager.Instance.StopMusic();
        AudioManager.Instance.PlayGameOverSound();
        SetState(GameState.GameOver);

        playerManager = FindObjectOfType<PlayerManager>();
        // Llama al método del PlayerManager para reproducir el efecto de partículas
        if (playerManager != null)
        {
            playerManager.PlayGameOverParticles();
        }

        // Registrar qué métodos están suscritos al evento
        foreach (var handler in OnGameOver.GetInvocationList())
        {
            Debug.Log("GameOver invocado por: " + handler.Target.ToString());
        }
        OnGameOver?.Invoke();
    }

    public void ReturnToMainMenu()
    {
        // Reinicia la escena actual
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        AudioManager.Instance.PlayMusic();
    }

    private string GetCallerObjectName()
    {
        var stackTrace = new System.Diagnostics.StackTrace(true);
        var frame = stackTrace.GetFrame(1);
        var method = frame.GetMethod();
        var caller = method.DeclaringType;

        return caller != null ? caller.Name : "Unknown";
    }



}

