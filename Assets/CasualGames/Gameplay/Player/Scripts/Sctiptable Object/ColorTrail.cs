using UnityEngine;

public class ColorTrail : MonoBehaviour
{
    [SerializeField] private TrailRenderer trailRenderer;
    [SerializeField] private TrailData trailData;

    private void OnEnable()
    {
        ApplyRandomTrail();
    }

    private void ApplyRandomTrail()
    {
        if (trailRenderer == null || trailData == null || trailData.trailGradients.Count == 0)
            return;

        int randomIndex = Random.Range(0, trailData.trailGradients.Count);
        Gradient randomGradient = trailData.trailGradients[randomIndex].trailGradient;

        trailRenderer.colorGradient = randomGradient;
    }
}