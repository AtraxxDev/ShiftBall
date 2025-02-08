using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

public class AccesibilitySettings : MonoBehaviour
{
    private const string MuteAudioKey = "MuteAudio";
    private const string MuteSFXKey = "MuteSFX";
    private const string VibrationKey = "Vibration";

    [BoxGroup("Mute Indicators")]
    public Image[] muteIndicators;

    [BoxGroup("Panel")]
    public GameObject panelSettings;

    private void Awake()
    {
        panelSettings.SetActive(false);
    }

    private void Start()
    {
        InitializeDefaultSettings();
        LoadSettings();


    }

    private void InitializeDefaultSettings()
    {
        if (!PlayerPrefs.HasKey(MuteAudioKey))
        {
            PlayerPrefs.SetInt(MuteAudioKey, (int)SettingState.Off);  // No muteado por defecto
        }

        if (!PlayerPrefs.HasKey(MuteSFXKey))
        {
            PlayerPrefs.SetInt(MuteSFXKey, (int)SettingState.Off);  // No muteado por defecto
        }

        if (!PlayerPrefs.HasKey(VibrationKey))
        {
            PlayerPrefs.SetInt(VibrationKey, (int)SettingState.On);  // Vibración activada por defecto
        }

        PlayerPrefs.Save();
    }

    public void ToggleMuteAudio()
    {
        bool isMuted = !AudioManager.Instance.backgroundMusicSource.mute;
        AudioManager.Instance.backgroundMusicSource.mute = isMuted;

        SetIndicatorOpacity(muteIndicators[0], isMuted);
        PlayerPrefs.SetInt(MuteAudioKey, isMuted ? (int)SettingState.On : (int)SettingState.Off);
        PlayerPrefs.Save();
    }

    public void ToggleMuteSFX()
    {
        bool isMuted = !AudioManager.Instance.sfxSource.mute;
        AudioManager.Instance.sfxSource.mute = isMuted;

        SetIndicatorOpacity(muteIndicators[1], isMuted);
        PlayerPrefs.SetInt(MuteSFXKey, isMuted ? (int)SettingState.On : (int)SettingState.Off);
        PlayerPrefs.Save();
    }

    public void ToggleVibration()
    {
        bool isEnabled = !GameManager.IsVibrationEnabled;
        GameManager.IsVibrationEnabled = isEnabled;

        // Invertir el manejo visual para que el indicador de vibración sea claro cuando esté activado
        SetIndicatorOpacity(muteIndicators[2], !isEnabled);

        PlayerPrefs.SetInt(VibrationKey, isEnabled ? (int)SettingState.On : (int)SettingState.Off);
        PlayerPrefs.Save();
    }


    private void LoadSettings()
    {
        bool isAudioMuted = PlayerPrefs.GetInt(MuteAudioKey) == (int)SettingState.On;
        AudioManager.Instance.backgroundMusicSource.mute = isAudioMuted;
        SetIndicatorOpacity(muteIndicators[0], isAudioMuted);

        bool isSFXMuted = PlayerPrefs.GetInt(MuteSFXKey) == (int)SettingState.On;
        AudioManager.Instance.sfxSource.mute = isSFXMuted;
        SetIndicatorOpacity(muteIndicators[1], isSFXMuted);

        bool isVibrationEnabled = PlayerPrefs.GetInt(VibrationKey) == (int)SettingState.On;
        GameManager.IsVibrationEnabled = isVibrationEnabled;
        SetIndicatorOpacity(muteIndicators[2], !isVibrationEnabled);
    }

    private void SetIndicatorOpacity(Image indicator, bool isActive)
    {
        Color color = indicator.color;
        color.a = isActive ? 0.3f : 1f;
        indicator.color = color;
    }
}

public enum SettingState
{
    Off = 0,
    On = 1
}
