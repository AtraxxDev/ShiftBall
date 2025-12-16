    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using Sirenix.OdinInspector;

    public class AudioManager : MonoBehaviour
    {
        public static AudioManager Instance;

        // ----------------------------------------------------------------------
        // VARIABLES
        // ----------------------------------------------------------------------
        [FoldoutGroup("Sources")]
        [SerializeField] private AudioSource musicSource;

        [FoldoutGroup("Sources")]
        [SerializeField] private AudioSource sfxSourcePrefab;

        [FoldoutGroup("Settings"), Range(0f, 1f)]
        [SerializeField] private float musicVolume = 1f;

        [FoldoutGroup("Settings"), Range(0f, 1f)]
        [SerializeField] private float sfxVolume = 1f;

        [FoldoutGroup("Audio Clips")]
        [SerializeField] private List<AudioClip> audioClips;

        private Dictionary<string, AudioClip> clipDictionary;
        private Queue<AudioSource> sfxPool = new Queue<AudioSource>();

        private const string MUSIC_PREF = "MusicOn";
        private const string SFX_PREF = "SfxOn";

        public bool MusicOn => PlayerPrefs.GetInt(MUSIC_PREF, 1) == 1;
        public bool SfxOn => PlayerPrefs.GetInt(SFX_PREF, 1) == 1;

        // ----------------------------------------------------------------------
        // UNITY
        // ----------------------------------------------------------------------
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
                BuildDictionary();
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void BuildDictionary()
        {
            clipDictionary = new Dictionary<string, AudioClip>();
            foreach (var clip in audioClips)
            {
                if (!clipDictionary.ContainsKey(clip.name))
                    clipDictionary.Add(clip.name, clip);
            }
        }

        // ----------------------------------------------------------------------
        // MUSIC
        // ----------------------------------------------------------------------
        public void PlayMusic(string clipName, bool fade = true, float fadeTime = 0.6f)
        {
            //if (!MusicOn) return;
            if (!clipDictionary.ContainsKey(clipName)) return;

            AudioClip newClip = clipDictionary[clipName];
            
            // Si la música está apagada, no reproduzcas nada
            if (!MusicOn)
            {
                musicSource.mute = true; 
            }

            // Si está ON, reproduce normalmente
            //musicSource.mute = false;

            if (fade)
                StartCoroutine(FadeMusic(newClip, fadeTime));
            else
            {
                musicSource.clip = newClip;
                musicSource.volume = musicVolume;
                musicSource.Play();
            }
        }

        private IEnumerator FadeMusic(AudioClip newClip, float fadeTime)
        {
            float startVolume = musicSource.volume;

            // Fade OUT
            for (float t = 0; t < fadeTime; t += Time.deltaTime)
            {
                musicSource.volume = Mathf.Lerp(startVolume, 0f, t / fadeTime);
                yield return null;
            }

            musicSource.clip = newClip;
            musicSource.Play();

            // Fade IN
            for (float t = 0; t < fadeTime; t += Time.deltaTime)
            {
                musicSource.volume = Mathf.Lerp(0f, musicVolume, t / fadeTime);
                yield return null;
            }
        }

        public void PauseGameplayMusic()
        {
            if (musicSource.isPlaying)
                musicSource.Pause();
        }

        public void ResumeGameplayMusic()
        {
            if (!musicSource.isPlaying)
                musicSource.UnPause();
        }

        // Cambia el estado de la música
        public void SetMusic(bool value)
        {
            PlayerPrefs.SetInt(MUSIC_PREF, value ? 1 : 0);
            PlayerPrefs.Save();

            musicSource.mute = !value; // value = ON? entonces mute = false
        }


        // ----------------------------------------------------------------------
        // SFX
        // ----------------------------------------------------------------------
        public void PlaySFX(string clipName)
        {
            if (!SfxOn) return;                       // NO sona si está OFF
            if (!clipDictionary.ContainsKey(clipName)) return;

            AudioSource src = GetSfxSource();
            src.clip = clipDictionary[clipName];
            src.volume = sfxVolume;
            src.Play();

            StartCoroutine(ReturnToPool(src, src.clip.length));
        }

        private AudioSource GetSfxSource()
        {
            if (sfxPool.Count > 0)
            {
                AudioSource src = sfxPool.Dequeue();
                src.gameObject.SetActive(true);
                return src;
            }

            // Crear uno nuevo si el pool está vacío
            return Instantiate(sfxSourcePrefab, transform);
        }

        private IEnumerator ReturnToPool(AudioSource src, float delay)
        {
            yield return new WaitForSeconds(delay);
            src.Stop();
            src.gameObject.SetActive(false);
            sfxPool.Enqueue(src);
        }

        public void SetSFX(bool value)
        {
            PlayerPrefs.SetInt(SFX_PREF, value ? 1 : 0);
        }
    }
