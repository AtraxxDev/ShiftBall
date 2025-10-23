using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TrailData", menuName = "Scriptable Objects/TrailDataColor", order = 3)]
public class TrailData : ScriptableObject
{
    [Header("Trail Gradients")]
    public List<TrailGradientData> trailGradients;

    [System.Serializable]
    public class TrailGradientData
    {
        public int id;
        public Gradient trailGradient;
    }

    public Gradient GetTrailGradient(int id)
    {
        TrailGradientData gradientData = trailGradients.Find(g => g.id == id);
        return gradientData != null ? gradientData.trailGradient : new Gradient(); // Return default gradient if not found
    }
}
