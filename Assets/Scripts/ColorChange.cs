using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChange : MonoBehaviour
{
    [Header("Color Data")]
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private int colorID = 0;


    // Start is called before the first frame update
    void Start()
    {
        ColorManager.Instance.OnPaletteChanged += OnPaletteChanged;
        UpdateColor(colorID);
    }

    void OnDestroy()
    {
        ColorManager.Instance.OnPaletteChanged -= OnPaletteChanged;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void UpdateColor(int paletteID)
    {
        Color newColor = ColorManager.Instance.GetColor(colorID);
        spriteRenderer.color = newColor;
    }

    private void OnPaletteChanged(int paletteID)
    {
        // Actualiza el color usando el colorID actual y la nueva paleta
        UpdateColor(colorID);
    }
}
