using System;
using System.Collections;
using System.Collections.Generic;
using TB_Tools;
using UnityEngine;

public class PlayerParticles : MonoBehaviour
{
    [Header("Particle Systems (UI Feedback)")]
    [SerializeField] private ParticleSystem particles_HighScore;
    [SerializeField] private ParticleSystem particles_Revive;
    [SerializeField] private GameObject player;

    [Header("Dependencies")]
    [SerializeField] private ShakeCamera shakeCamera;

    private bool hasPlayedHighScoreParticles = false;

    private void OnEnable()
    {
    }

    private void Start()
    {
        GameEvents.OnGameOverParticles += PlayGameOverParticles;
        ScoreManager.Instance.OnHighScoreChanged += (_) => PlayHighScoreParticles();
        GameManager.Instance.OnRevivePlayer += PlayReviveParticles;
    }

    private void OnDisable()
    {
        GameManager.Instance.OnRevivePlayer -= PlayReviveParticles;
        GameEvents.OnGameOverParticles -= PlayGameOverParticles;
        ScoreManager.Instance.OnHighScoreChanged -= (_) => PlayHighScoreParticles();
    }

    // =============================
    //  HIGH SCORE PARTICLES
    // =============================
    public void PlayHighScoreParticles()
    {
        if (!hasPlayedHighScoreParticles)
        {
            hasPlayedHighScoreParticles = true;
            particles_HighScore.Play();
        }
    }

    // =============================
    //  REVIVE PARTICLES
    // =============================
    public void PlayReviveParticles()
    {
        if (particles_Revive != null)
            particles_Revive.Play();
    }

    // =============================
    //  GAME OVER EXPLOSION (TIENDA)
    // =============================
    public void PlayGameOverParticles()
    {
        // Apaga sprite del jugador
        player.gameObject.SetActive(false);

        // Obtener item seleccionado desde ShopManager
        var selectedExplosion = ShopManager.Instance.GetSelectedItemByCategory(ItemCategory.Explosion);

        string explosionID = "Explosion_Default";

        if (selectedExplosion != null)
        {
            explosionID = selectedExplosion.Id;
        }
        else
        {
            Debug.Log("<color=yellow>No hay explosión seleccionada, usando default.</color>");
        }


        // Reproducir explosión
        ParticleManager.Instance.PlayParticleEffect(explosionID, player.transform.position);

        // Shake
        if (shakeCamera != null)
            shakeCamera.Shake();
    }
}
