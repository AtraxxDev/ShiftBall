using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerParticles : MonoBehaviour
{
    [Header("Particle Systems")]
    [SerializeField] private ParticleSystem particles_HighScore; 
    [SerializeField] private int particleEffectID;

    [Header("Dependencies")]
    [SerializeField] private ShakeCamera shakeCamera;

    private bool hasPlayedHighScoreParticles = false;

    private void Start()
    {
        particleEffectID = PlayerPrefs.GetInt("ParticleKey", 0);
    }

    public void PlayHighScoreParticles()
    {
        if (!hasPlayedHighScoreParticles)
        {
            particles_HighScore.Play();
            hasPlayedHighScoreParticles = true;
        }
    }

    public void PlayGameOverParticles(Transform objectReference ,Vector3 position)
    {
        objectReference.gameObject.SetActive(false);
        ParticleManager.Instance.PlayParticleEffect(particleEffectID, position);
        
        if (shakeCamera != null)
        {
            shakeCamera.Shake();
        }
    }

    public void UpdateParticleEffectID(int newParticleEffectID)
    {
        particleEffectID = newParticleEffectID;
        PlayerPrefs.SetInt("ParticleKey", particleEffectID);
        PlayerPrefs.Save();
    }
}
