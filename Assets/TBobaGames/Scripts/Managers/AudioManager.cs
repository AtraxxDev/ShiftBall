using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [Header("Audio Sources")]
    [SerializeField] private AudioSource backgroundMusicSource;
    [SerializeField] private AudioSource sfxSource;

    [Header("Audio Clips")]
    [SerializeField] private AudioClip menuMusic;
    [SerializeField] private AudioClip gameplayMusic;
    [SerializeField] private AudioClip wallHitSound;
    [SerializeField] private AudioClip coinPickupSound;
    [SerializeField] private AudioClip gameOverSound;
    [SerializeField] private AudioClip CelebrateSound;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

   

    private void Start()
    {
        PlayMenuMusic();
    }

    public void PlayMenuMusic()
    {
        if (backgroundMusicSource != null && menuMusic != null)
        {
            backgroundMusicSource.clip = menuMusic;
            backgroundMusicSource.loop = true;
            backgroundMusicSource.Play();
        }
    }

    public void PlayGameplayMusic()
    {
        if (backgroundMusicSource != null && gameplayMusic != null)
        {
            backgroundMusicSource.clip = gameplayMusic;
            backgroundMusicSource.loop = true;
            backgroundMusicSource.Play();
        }
    }



    public void PlaySFX(AudioClip clip)
    {
        if (sfxSource != null && clip != null)
        {
            sfxSource.PlayOneShot(clip);
        }
    }

    public void PlayWallHitSound()
    {
        PlaySFX(wallHitSound);
    }

    public void PlayCelebrationSFX()
    {
        PlaySFX(CelebrateSound);
    }

    public void PlayCoinPickupSound()
    {
        PlaySFX(coinPickupSound);
    }

    public void PlayGameOverSound()
    {
        PlaySFX(gameOverSound);
    }

    public void PlayMusic()
    {
        backgroundMusicSource.clip = menuMusic;
        backgroundMusicSource.Play();

    }

    public void StopMusic()
    {
        backgroundMusicSource.Stop();

    }

}
