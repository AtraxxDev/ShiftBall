using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Color Data",menuName ="Scriptable Objects/ColorData")]
public class ColorData : ScriptableObject
{
    [BoxGroup("Color Pallete")]
    public List<ColorPalette> colorPaletteList;

    [System.Serializable]
    public class ColorPalette
    {
        public int ID_Palette;
        public Color[] colors; // Each index corresponds to an ID
    }

    public Color GetColor(int paletteID, int colorID)
    {
        ColorPalette palette = colorPaletteList.Find(p => p.ID_Palette == paletteID);
        if (palette != null && colorID >= 0 && colorID < palette.colors.Length)
        {
            return palette.colors[colorID];
        }
        return Color.white; // Default color if not found
    }



}
