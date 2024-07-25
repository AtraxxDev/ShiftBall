using System.Collections.Generic;
using UnityEngine;

public class ColorManager : MonoBehaviour
{
    public static ColorManager Instance { get; private set; }

    [SerializeField] public ColorData colorData;
    public int currentPaletteID;

    public delegate void PaletteChanged(int paletteID);
    public event PaletteChanged OnPaletteChanged;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        ChangePalette(currentPaletteID);
    }

    public void ChangePalette(int newPaletteID)
    {
        currentPaletteID = newPaletteID;
        // Invoca el evento pasando el color completo
        OnPaletteChanged?.Invoke(currentPaletteID); // Asegúrate de que colorID está definido o usa otro método para obtener el color
        Debug.Log($"Paleta cambiada a ID: {newPaletteID}");
    }

    public Color GetColor(int colorID)
    {
        return colorData.GetColor(currentPaletteID, colorID);
    }
}
