using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TB_Tools;

public class ColorChange : MonoBehaviour
{
    [Header("Color Data")]
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Image uiImage;
    [SerializeField] private int colorID = 0;
    [SerializeField] private ColorTarget targetType;

    private void Start()
    {
        ColorManager.Instance.OnPaletteChanged += OnPaletteChanged;
        UpdateColor(colorID);
    }

    private void OnDestroy()
    {
        ColorManager.Instance.OnPaletteChanged -= OnPaletteChanged;
    }

    private void UpdateColor(int paletteID)
    {
        Color newColor = ColorManager.Instance.GetColor(colorID);

        switch (targetType)
        {
            case ColorTarget.Object:
                if (spriteRenderer != null)
                {
                    spriteRenderer.color = newColor;
                }
                break;

            case ColorTarget.UI:
                if (uiImage != null)
                {
                    uiImage.color = newColor;
                }
                break;
        }
    }

    private void OnPaletteChanged(int paletteID)
    {
        UpdateColor(colorID);
    }
}
