using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [Header("Audio Sources")]
    public AudioSource backgroundMusicSource;
    public AudioSource sfxSource;

    [FoldoutGroup("Audio Clips")]
    [SerializeField] private AudioClip menuMusic;
    [FoldoutGroup("Audio Clips")]
    [SerializeField] private AudioClip gameplayMusic;
    [FoldoutGroup("Audio Clips")]
    [SerializeField] private AudioClip wallHitSound;
    [FoldoutGroup("Audio Clips")]
    [SerializeField] private AudioClip coinPickupSound;
    [FoldoutGroup("Audio Clips")]
    [SerializeField] private AudioClip gameOverSound;
    [FoldoutGroup("Audio Clips")]
    [SerializeField] private AudioClip CelebrateSound;
    [FoldoutGroup("Audio Clips")]
    [SerializeField] private AudioClip reviveSound;
    [FoldoutGroup("Audio Clips")]
    [SerializeField] private AudioClip selectSound;


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

    public void PlaySelectSFX()
    {
        PlaySFX(selectSound);
    }

    public void PlayCelebrationSFX()
    {
        PlaySFX(CelebrateSound);
    }

    public void PlayCoinPickupSound()
    {
        PlaySFX(coinPickupSound);
    }

    public void PlayReviveSound()
    {
        PlaySFX(reviveSound);
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
