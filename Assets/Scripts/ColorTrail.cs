using System;
using UnityEngine;

public class ColorTrail : MonoBehaviour
{
    [SerializeField] private TrailRenderer trailRenderer;
    [SerializeField] private TrailData gradientTrailData;

    public event Action<int> OnTrailGradientChanged;

    public int currentTrailID;

    private void Start()
    {
        // Aplica el gradiente inicial basado en el ID actual
        ApplyTrailGradient(currentTrailID);
    }



    private void ApplyTrailGradient(int id)
    {
        if (trailRenderer != null && gradientTrailData != null)
        {
            Gradient newGradient = gradientTrailData.GetTrailGradient(id);
            trailRenderer.colorGradient = newGradient; // Aplicar el gradiente al TrailRenderer
            OnTrailGradientChanged?.Invoke(id);
        }
    }
}
