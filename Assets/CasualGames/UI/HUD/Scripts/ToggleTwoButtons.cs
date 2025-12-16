using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ToggleTwoButtons : MonoBehaviour
{
    [Header("Buttons")]
    [SerializeField] private Button buttonOn;
    [SerializeField] private Button buttonOff;

    [Header("Images (Backgrounds)")]
    [SerializeField] private Image onBG;
    [SerializeField] private Image offBG;

    [Header("Texts")]
    [SerializeField] private TMP_Text onText;
    [SerializeField] private TMP_Text offText;

    [Header("Colors")]
    [SerializeField] private Color activeBGColor = Color.black;
    [SerializeField] private Color inactiveBGColor = Color.white;
    [SerializeField] private Color activeTextColor = Color.black;
    [SerializeField] private Color inactiveTextColor = Color.white;

    [Header("Optional PlayerPrefs Key")]
    [SerializeField] private ToggleType toggleType;  
    [SerializeField] private string prefsKey = "OnEventChange";

    [ShowInInspector, ReadOnly]
    private bool isOn;

    private void Start()
    {
        isOn = PlayerPrefs.GetInt(prefsKey, 1) == 1;

        RefreshUI();

        buttonOn.onClick.AddListener(() => SetState(true));
        buttonOff.onClick.AddListener(() => SetState(false));
    }

    private void SetState(bool value)
    {
        isOn = value;

        PlayerPrefs.SetInt(prefsKey, isOn ? 1 : 0);
        PlayerPrefs.Save();

        AudioManager.Instance.PlaySFX("SelectUI");
        RefreshUI();
        PlayClickAnimation();
        
        if (toggleType == ToggleType.Vibration && isOn)
            Handheld.Vibrate();
        
        ApplySetting();


        EventManager.TriggerEvent(prefsKey, isOn);
    }

    private void RefreshUI()
    {
        if (isOn)
        {
            onBG.color = activeBGColor;
            offBG.color = inactiveBGColor;

            onText.color = activeTextColor;
            offText.color = inactiveTextColor;
        }
        else
        {
            onBG.color = inactiveBGColor;
            offBG.color = activeBGColor;

            onText.color = inactiveTextColor;
            offText.color = activeTextColor;
        }
    }
    
    private void ApplySetting()
    {
        switch (toggleType)
        {
            case ToggleType.Music:
                AudioManager.Instance.SetMusic(isOn);
                break;

            case ToggleType.SFX:
                AudioManager.Instance.SetSFX(isOn);
                break;

            case ToggleType.Vibration:
                // No hay manager, solo guardas el prefs
                break;
        }
    }

    // ---------------------------------------------------------
    // 🔥 LEANTWEEN FEEDBACK EFFECT
    // ---------------------------------------------------------
    private void PlayClickAnimation()
    {
        RectTransform target = isOn
            ? buttonOn.GetComponent<RectTransform>()
            : buttonOff.GetComponent<RectTransform>();

        // Reset scale before animating
        target.localScale = Vector3.one;

        LeanTween.scale(target, Vector3.one * 1.1f, 0.12f)
            .setEaseOutBack()
            .setOnComplete(() => {
                LeanTween.scale(target, Vector3.one, 0.12f).setEaseOutCubic();
            });
    }
}

public enum ToggleType
{
    Music,
    SFX,
    Vibration
}

