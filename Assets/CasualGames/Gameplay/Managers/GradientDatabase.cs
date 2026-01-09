using UnityEngine;
using Sirenix.OdinInspector;

[CreateAssetMenu(
    fileName = "GradientDatabase",
    menuName = "Visuals/Gradient Database"
)]
public class GradientDatabase : ScriptableObject
{
    [System.Serializable]
    public class GradientData
    {
        [HorizontalGroup("Row", Width = 120)]
        [LabelText("ID")]
        public int id;

        [HorizontalGroup("Row")]
        [LabelText("Up")]
        public Color upColor;

        [HorizontalGroup("Row")]
        [LabelText("Down")]
        public Color downColor;
    }

    [Title("Gradients")]
    public GradientData[] gradients;

    public bool TryGetGradient(int id, out GradientData data)
    {
        foreach (var g in gradients)
        {
            if (g.id == id)
            {
                data = g;
                return true;
            }
        }

        data = null;
        return false;
    }
}