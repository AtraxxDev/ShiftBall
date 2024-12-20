using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private PlayerParticles playerParticles;

    private void OnEnable()
    {
        SuscribeEvents();

    }

    private void OnDisable()
    {
        UnsuscribeEvents();
    }

    private void Start()
    {
    }

    private void Update()
    {
        if (GameManager.Instance.IsPaused()) return;
        playerMovement.HandleInput(); // Se refiere al darle click a la pantalla cambia la direccion
    }

    private void FixedUpdate()
    {
        if (GameManager.Instance.IsPaused()) return;
        playerMovement.MovePlayer();
    }

    private void SuscribeEvents()
    {
        if (ParticleManager.Instance != null)
        {
            ParticleManager.Instance.OnParticleEffectChanged += playerParticles.UpdateParticleEffectID;
        }
        if (ScoreManager.Instance != null)
        {
            ScoreManager.Instance.OnHighScoreChanged += (_) => playerParticles.PlayHighScoreParticles();
        }
    }

    private void UnsuscribeEvents()
    {
        if (ParticleManager.Instance != null)
        {
            ParticleManager.Instance.OnParticleEffectChanged -= playerParticles.UpdateParticleEffectID;
        }
        if (ScoreManager.Instance != null)
        {
            ScoreManager.Instance.OnHighScoreChanged -= (_) => playerParticles.PlayHighScoreParticles();
        }
    }
}
