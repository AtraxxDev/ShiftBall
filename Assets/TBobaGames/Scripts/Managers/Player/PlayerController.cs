using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private PlayerParticles playerParticles;
    [SerializeField] private ShieldPowerUp shieldPowerUp;


    private void OnEnable()
    {
        SuscribeEvents();

    }

    private void OnDisable()
    {
        UnsuscribeEvents();
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        playerMovement.HandleCollision(collision);
    }


    public void TakeDamage(GameObject visual)
    {
        if (shieldPowerUp != null && shieldPowerUp.AbsorbHit())
        {
            Debug.Log("Golpe absorbido por el escudo");
            visual.SetActive(false);
            return; // Interrumpir el daño si el golpe fue absorbido
        }

        Debug.Log("El jugador recibió daño. No había escudo activo.");
        GameManager.Instance.GameOver();
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
