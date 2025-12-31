using Sirenix.OdinInspector;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [Header("Panels")]
    [SerializeField] private GameObject inGamePanel;
    [SerializeField] private GameObject menuPanel;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject pauseButton;

    //[SerializeField] private UIGameOverSequence gameOverSequence;

    private void Start()
    {
        GameManager.Instance.OnGameOver += HandleGameOver;
        GameManager.Instance.OnRevivePlayer += HandleRevive;
    }

    private void OnDisable()
    {
        GameManager.Instance.OnGameOver -= HandleGameOver;
        GameManager.Instance.OnRevivePlayer -= HandleRevive;
    }

    private void HandleGameOver()
    {
        pauseButton.SetActive(false);
        inGamePanel.SetActive(false);
        gameOverPanel.SetActive(true);

        //gameOverSequence.Play(); // 🔥 delega la secuencia
    }

    private void HandleRevive()
    {
        gameOverPanel.SetActive(false);
        inGamePanel.SetActive(true);
    }

    // Botones
    public void OnStartGame()
    {
        GameManager.Instance.StartGame();
        pauseButton.SetActive(true);
        inGamePanel.SetActive(true);
        AudioManager.Instance.PlayMusic("Gameplay");
    }

    [Button]
    public void OnRestart()
    {
        GameManager.Instance.Restart();
        pauseButton.SetActive(true);
        inGamePanel.SetActive(true);
        AudioManager.Instance.PlayMusic("Gameplay");
    }
    public void OnReturnMainMenu() => GameManager.Instance.ReturnToMainMenu();
}