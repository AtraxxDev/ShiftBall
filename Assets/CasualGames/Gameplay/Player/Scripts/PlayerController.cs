using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TB_Tools;
using System.Runtime.Serialization;

public class PlayerController : MonoBehaviour
{
    [Header("Player Components")]
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private PlayerParticles playerParticles;
    [SerializeField] private ShieldPowerUp shieldPowerUp;

    [Header("Visuals")]
    [SerializeField] private SpriteRenderer playerSprite;

    [Header("Invulnerability")]
    [SerializeField] private float invulnerabilityDuration = 3f;
    [SerializeField] private float blinkInterval = 0.2f;

    public bool isInvencible;

    private void OnEnable()
    {
        SuscribeEvents();
    }

    private void Start()
    {
        GameManager.Instance.OnRevivePlayer += HandlePlayerRevival;
    }

    private void OnDisable()
    {
        UnsuscribeEvents();
    }

    private void Update()
    {
        if (GameManager.Instance.IsPaused()) return;

        // Cambia la dirección del jugador al tocar la pantalla
        playerMovement.HandleInput();
    }

    private void FixedUpdate()
    {
        if (GameManager.Instance.IsPaused()) return;

        playerMovement.MovePlayer();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        playerMovement.HandleCollision(collision);
    }

    public void TakeDamage(GameObject visual)
    {
        // Si el escudo absorbió el golpe → no daño
        if (shieldPowerUp != null && shieldPowerUp.AbsorbHit())
        {
            visual.SetActive(false);
            return;
        }

        if (isInvencible) return;

        GameManager.Instance.GameOver();
    }

    private void SuscribeEvents()
    {
        if (ScoreManager.Instance != null)
        {
            ScoreManager.Instance.OnHighScoreChanged += (_) => playerParticles.PlayHighScoreParticles();
        }
    }

    public void PausedGame()
    {
        GameManager.Instance.SetState(GameState.Paused);
        print("Esta en pausa 2");
    }

    private void UnsuscribeEvents()
    {

        if (ScoreManager.Instance != null)
        {
            ScoreManager.Instance.OnHighScoreChanged -= (_) => playerParticles.PlayHighScoreParticles();
        }

        if (GameManager.Instance != null)
        {
            GameManager.OnPauseGame -= PausedGame;
        }

        GameManager.Instance.OnRevivePlayer -= HandlePlayerRevival;
    }

    private void HandlePlayerRevival()
    {
        transform.GetChild(0).gameObject.SetActive(true);

        isInvencible = true;
        StartCoroutine(InvulnerabilityCoroutine());
    }

    private IEnumerator InvulnerabilityCoroutine()
    {
        float elapsedTime = 0f;

        // Parpadeo del sprite
        while (elapsedTime < invulnerabilityDuration)
        {
            playerSprite.enabled = !playerSprite.enabled;
            elapsedTime += blinkInterval;
            yield return new WaitForSeconds(blinkInterval);
        }

        playerSprite.enabled = true;
        isInvencible = false;
    }
}
