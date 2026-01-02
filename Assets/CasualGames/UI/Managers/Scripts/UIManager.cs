using Sirenix.OdinInspector;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [Header("Panels")]
    [SerializeField] private GameObject inGamePanel;
    [SerializeField] private GameObject menuPanel;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject blackPanel;

    [SerializeField] private UIGameOverSequence gameOverSequence;

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
        blackPanel.SetActive(true);
        inGamePanel.SetActive(false);
        gameOverPanel.SetActive(true);

        gameOverSequence.Play(); //  delega la secuencia
    }

    private void HandleRevive()
    {
        gameOverPanel.SetActive(false);
        inGamePanel.SetActive(true);
        blackPanel.SetActive(false);
        AudioManager.Instance.PlaySFX("Revive");
        
    }

    // Botones
    public void OnStartGame()
    {
        GameManager.Instance.StartGame();
        //pauseButton.SetActive(true);
        inGamePanel.SetActive(true);
        AudioManager.Instance.PlayMusic("Gameplay");
    }

    [Button]
    public void OnRestart()
    {
        GameManager.Instance.Restart();
        blackPanel.SetActive(false);
        inGamePanel.SetActive(true);
        AudioManager.Instance.PlayMusic("Gameplay");
    }

    [Button]
    public void OnReturnMainMenu()
    {
        GameManager.Instance.ReturnToMainMenu();
        blackPanel.SetActive(false);
        menuPanel.SetActive(true);
        inGamePanel.SetActive(false);
    } 
}