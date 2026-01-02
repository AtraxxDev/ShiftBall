using TMPro;
using UnityEngine;

public class UICombo : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private RectTransform comboRoot; // PADRE EN CANVAS
    [SerializeField] private TMP_Text comboText;      // TEXTO

    [Header("Colors (cada 10 combos)")]
    [SerializeField] private Color[] comboColors;

    [Header("Slide Settings")]
    [SerializeField] private float slideOffsetY = 120f;
    [SerializeField] private float slideTime = 0.35f;

    [Header("Punch Settings")]
    [SerializeField] private float punchScale = 0.25f;
    [SerializeField] private float punchTime = 0.2f;

    private Vector2 initialRootPos;
    private bool isVisible;

    private Color baseColor;

    // =============================
    // LIFECYCLE
    // =============================
    private void Awake()
    {
        initialRootPos = comboRoot.anchoredPosition;
        baseColor = Color.white;

        ResetUI();
    }

    private void Start()
    {
        ComboManager.Instance.OnComboChanged += OnComboChanged;
    }

    private void OnDisable()
    {
        if (ComboManager.Instance != null)
            ComboManager.Instance.OnComboChanged -= OnComboChanged;
    }

    // =============================
    // COMBO EVENT
    // =============================
    private void OnComboChanged(int combo)
    {
        if (combo <= 0)
        {
            BreakCombo();
            return;
        }

        comboText.text = $"Combo x{combo}";
        UpdateColor(combo);

        if (!isVisible)
            SlideIn();

        Punch();
    }

    // =============================
    // ANIMATIONS
    // =============================
    private void SlideIn()
    {
        isVisible = true;

        comboRoot.anchoredPosition = initialRootPos + Vector2.down * slideOffsetY;
        comboText.alpha = 1f;

        
        LeanTween.move(comboRoot, initialRootPos, slideTime)
            .setEaseOutBack();

    }

    private void Punch()
    {
        LeanTween.cancel(comboText.gameObject);

        comboText.transform.localScale = Vector3.one;

        LeanTween.scale(comboText.gameObject,
                Vector3.one + Vector3.one * punchScale,
                punchTime)
            .setEasePunch();
    }

    private void UpdateColor(int combo)
    {
        if (comboColors == null || comboColors.Length == 0)
            return;

        int index = Mathf.Clamp((combo - 1) / 10, 0, comboColors.Length - 1);
        comboText.color = comboColors[index];
    }

    // =============================
    // BREAK COMBO
    // =============================
    private void BreakCombo()
    {
        if (!isVisible) return;

        isVisible = false;

        LeanTween.cancel(comboText.gameObject);
        LeanTween.cancel(comboRoot.gameObject);
        

        LeanTween.move(comboRoot,
            initialRootPos + Vector2.down * slideOffsetY,
            0.3f).setEaseInBack();

        LeanTween.alphaText(comboText.rectTransform, 0f, 0.3f)
            .setOnComplete(() => ResetUI());
    }

    // =============================
    // RESET
    // =============================
    private void ResetUI()
    {
        comboText.text = "";
        comboText.color = baseColor;
        comboText.alpha = 0f;
        comboText.transform.localScale = Vector3.one;

        comboRoot.anchoredPosition = initialRootPos + Vector2.down * slideOffsetY;

        isVisible = false;
    }
}
