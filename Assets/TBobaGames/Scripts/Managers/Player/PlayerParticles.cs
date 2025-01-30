using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerParticles : MonoBehaviour
{
    [Header("Particle Systems")]
    [SerializeField] private ParticleSystem particles_HighScore; 
    [SerializeField] private ParticleSystem particles_Revive;
    [SerializeField] private int particleEffectID;

    [Header("Dependencies")]
    [SerializeField] private ShakeCamera shakeCamera;

    private bool hasPlayedHighScoreParticles = false;



    private void Start()
    {
        particleEffectID = PlayerPrefs.GetInt("ParticleKey", 0);

        GameManager.Instance.OnRevivePlayer += PlayReviveParticles;
    }

    private void OnDisable()
    {
        GameManager.Instance.OnRevivePlayer -= PlayReviveParticles;

    }

    public void PlayHighScoreParticles()
    {
        if (!hasPlayedHighScoreParticles)
        {
            hasPlayedHighScoreParticles = true;
            particles_HighScore.Play();
        }
    }


    public void PlayReviveParticles()
    {
        particles_Revive.Play();
    }

    public void PlayGameOverParticles(Transform objectReference ,Vector3 position)
    {
        objectReference.transform.GetChild(0).gameObject.SetActive(false);
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
