using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace TB_Tools
{

    public enum ItemCategory
    {
        Palette,
        Trail,
        Explosion
    }

    public enum GameState
    {
        Playing,
        Paused,
        GameOver
    }

    public enum ColorTarget
    {
        Object,
        UI
    }

    public enum PowerUpType
    {
        None,
        Shield,
        Magnet,
        ExpandigWave,
        // A�adir m�s tipos de power-ups seg�n sea necesario
    }

    public enum CurrencyType
    {
        Coins,
        Diamonds,
        AD
    }

    public enum DataType
    {
        ColorPalette,
        TrailGradient,
        ParticleEffect
    }

}