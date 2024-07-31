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
        currentPaletteID = PlayerPrefs.GetInt("PaletteID", 0);
        // ChangePalette(currentPaletteID);
    }

    public void ChangePalette(int newPaletteID)
    {
        currentPaletteID = newPaletteID;
        // Guardar el ID de la paleta en PlayerPrefs
        PlayerPrefs.SetInt("PaletteID", newPaletteID);
        PlayerPrefs.Save(); // Asegura que los datos se guarden inmediatamente

        // Invoca el evento pasando el ID de la paleta
        OnPaletteChanged?.Invoke(currentPaletteID);
        Debug.Log($"Paleta cambiada a ID: {newPaletteID}");
    }

    public Color GetColor(int colorID)
    {
        return colorData.GetColor(currentPaletteID, colorID);
    }
}
