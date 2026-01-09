using UnityEngine;
using Sirenix.OdinInspector;

public class HUEBackgroundManager : MonoBehaviour
{
    [Title("References")]
    [SerializeField, Required] private Material gradientMaterial;
    [SerializeField, Required] private GradientDatabase gradientDatabase;

    [Title("Settings")]
    [SerializeField] private float transitionSpeed = 1f;

    private Color currentUp;
    private Color currentDown;
    private Color targetUp;
    private Color targetDown;

    private bool isTransitioning;
    private float t;

    // Evita repetir el mismo gradiente
    private int lastGradientIndex = -1;

    private void Start()
    {
        // Color inicial desde el material
        currentUp = gradientMaterial.GetColor("_Up");
        currentDown = gradientMaterial.GetColor("_Down");

        targetUp = currentUp;
        targetDown = currentDown;

        // Siempre iniciar con el gradiente 0
        ApplyGradientByIndex(0);

        //GameManager.Instance.OnStartGame += ApplyGradientByIndex(0);
        GameManager.Instance.OnRestartGame += ApplyNextGradient;
        ScoreManager.Instance.OnScoreStepReached += _ => ApplyNextGradient();
    }

    private void Update()
    {
        if (!isTransitioning || gradientMaterial == null)
            return;

        t += Time.deltaTime * transitionSpeed;

        gradientMaterial.SetColor("_Up", Color.Lerp(currentUp, targetUp, t));
        gradientMaterial.SetColor("_Down", Color.Lerp(currentDown, targetDown, t));

        if (t >= 1f)
        {
            currentUp = targetUp;
            currentDown = targetDown;
            isTransitioning = false;
            t = 0f;
        }
    }

    // ===============================
    // GRADIENT LOGIC
    // ===============================

    [Button("Apply Random Gradient")]
    public void ApplyNextGradient()
    {
        if (gradientDatabase == null || gradientDatabase.gradients.Length == 0)
            return;

        int count = gradientDatabase.gradients.Length;

        // Si solo hay uno, no hay alternativa
        if (count == 1)
        {
            ApplyGradientByIndex(0);
            return;
        }

        int index;
        do
        {
            index = Random.Range(0, count);
        }
        while (index == lastGradientIndex);

        ApplyGradientByIndex(index);
    }

    public void ApplyGradientByIndex(int index)
    {
        if (gradientDatabase == null ||
            index < 0 ||
            index >= gradientDatabase.gradients.Length)
            return;

        lastGradientIndex = index;

        var data = gradientDatabase.gradients[index];
        SetTargetGradient(data);
    }

    private void SetTargetGradient(GradientDatabase.GradientData data)
    {
        targetUp = data.upColor;
        targetDown = data.downColor;

        isTransitioning = true;
        t = 0f;
    }
}
