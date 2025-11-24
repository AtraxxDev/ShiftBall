using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TB_Tools;


public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; set; }

    public GameState CurrentState { get; private set;}

    public Action OnGameOver;
    private bool isPaused = false;


    public static event Action OnPauseGame;


    public Action OnRevivePlayer;

    public static bool IsVibrationEnabled = true;

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
        AudioManager.Instance.PlayMenuMusic();
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
            CoinManager.Instance.ResetCoins_StarsCollected();
            SetState(GameState.Playing);
        }
    }

    public void PausedGame()
    {
        SetState(GameState.Paused);
        AudioManager.Instance.PauseMusic();
        OnPauseGame?.Invoke();       
    }

    public void ResumeGame()
    {
        AudioManager.Instance.backgroundMusicSource.UnPause();
        SetState(GameState.Playing);
    }

    public void ResumeNewLive()
    {
        SetState(GameState.Playing);
        AudioManager.Instance.backgroundMusicSource.UnPause();


    }

    public void GameOver()
    {
        if (IsVibrationEnabled)
        {
            Handheld.Vibrate();
            Debug.Log("Vibraci�n activada");
        }
        AudioManager.Instance.PauseMusic();
        AudioManager.Instance.PlayGameOverSound();
        SetState(GameState.GameOver);


        // ShowReviveButton(); // Mostrar bot�n de revivir


        GameEvents.RaiseGameOverParticles();
        OnGameOver?.Invoke();
    }

    public void Restart()
    {
        ScoreManager.Instance.ResetScore();

        // Reinicia la escena actual
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ReturnToMainMenu()
    {
        ScoreManager.Instance.ResetScore();
        AudioManager.Instance.PlayMenuMusic();
    }



    public void RevivePlayer()
    {
        OnRevivePlayer?.Invoke();
        AudioManager.Instance?.PlayReviveSound();
        ResumeNewLive();
    }




}

