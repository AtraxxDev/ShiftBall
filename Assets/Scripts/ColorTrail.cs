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
        currentTrailID = PlayerPrefs.GetInt("TrailKey", 0);
        ApplyTrailGradient();
    }

    public void SetTrailGradient(int id)
    {
        currentTrailID = id;
        ApplyTrailGradient();
        PlayerPrefs.SetInt("TrailKey", currentTrailID);
        PlayerPrefs.Save();
    }



    public void ApplyTrailGradient()
    {
        if (trailRenderer != null && gradientTrailData != null)
        {
            Gradient newGradient = gradientTrailData.GetTrailGradient(currentTrailID);
            trailRenderer.colorGradient = newGradient; // Aplicar el gradiente al TrailRenderer
            OnTrailGradientChanged?.Invoke(currentTrailID);
        }
    }
}
