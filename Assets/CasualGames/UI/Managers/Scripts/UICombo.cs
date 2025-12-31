using TMPro;
using UnityEngine;

public class UICombo : MonoBehaviour
{
    [SerializeField] private TMP_Text comboText;

    private void Start()
    {
        ComboManager.Instance.OnComboChanged += OnComboChanged;
    }

    private void OnDisable()
    {
        ComboManager.Instance.OnComboChanged -= OnComboChanged;
    }

    private void OnComboChanged(int combo)
    {
        if (combo <= 0)
        {
            comboText.text = "";
            return;
        }

        comboText.text = $"Combo x{combo}";
        comboText.transform.localScale = Vector3.one;

        float scale = combo % 5 == 0 ? 1.3f : 1.1f;

        LeanTween.scale(comboText.gameObject, Vector3.one * scale, 0.2f)
            .setEaseOutBack()
            .setOnComplete(() =>
                LeanTween.scale(comboText.gameObject, Vector3.one, 0.2f)
            );
    }
}
