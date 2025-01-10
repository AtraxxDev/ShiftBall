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

    private PlayerParticles playerParticles;



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
            AudioManager.Instance.PlayGameplayMusic();
            SetState(GameState.Playing);
        }
    }

    public void GameOver()
    {
        AudioManager.Instance.StopMusic();
        AudioManager.Instance.PlayGameOverSound();
        SetState(GameState.GameOver);

        playerParticles = FindObjectOfType<PlayerParticles>();
        // Llama al método del PlayerManager para reproducir el efecto de partículas
        if (playerParticles != null)
        {
            var objectPos = playerParticles.transform;
            playerParticles.PlayGameOverParticles(objectPos, objectPos.position);
        }

        OnGameOver?.Invoke();
    }

    public void ReturnToMainMenu()
    {
        // Reinicia la escena actual
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        AudioManager.Instance.PlayMusic();
    }



}

