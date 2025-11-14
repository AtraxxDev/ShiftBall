using UnityEngine;
using Sirenix.OdinInspector;

public class HUEBackgroundManager : MonoBehaviour
{
    [SerializeField, Required] private Material gradientMaterial;
    [SerializeField, Range(0f, 1f)] private float transitionSpeed = 1f;
    [Range(0f, 1f)] public float saturation = 0.6f;
    [Range(0f, 1f)] public float brightness = 0.8f;


    [ColorUsage(true, true)]
    [SerializeField, ReadOnly] private Color currentColor;
    private Color targetColor;
    private float hue;

    private bool isTransitioning = false;
    private float t = 0f;

    private void Start()
    {
        // Inicializa con el color actual del material
        if (gradientMaterial != null)
        {
            currentColor = gradientMaterial.GetColor("_Color2");
            targetColor = currentColor;
        }
    }

    private void Update()
    {
        if (!isTransitioning || gradientMaterial == null)
            return;

        // Lerp suave entre color actual y el objetivo
        t += Time.deltaTime * transitionSpeed;
        Color newColor = Color.Lerp(currentColor, targetColor, t);
        gradientMaterial.SetColor("_Down", newColor);

        if (t >= 1f)
        {
            currentColor = targetColor;
            isTransitioning = false;
            t = 0f;
        }
    }

    [Button("Cambiar Color (Hue)")]
    private void ChangeToNextHue()
    {
        // Genera un nuevo color en base al HUE
        hue += 0.15f; // cambia el paso de color (0.1 - 0.3 es ideal)
        if (hue > 1f) hue -= 1f;

        targetColor = Color.HSVToRGB(hue, saturation,brightness);
        isTransitioning = true;
        t = 0f;
    }
}
