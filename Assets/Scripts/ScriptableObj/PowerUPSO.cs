using UnityEngine;
using TB_Tools;

[CreateAssetMenu(fileName = "New PowerUp", menuName = "Scriptable Objects/PoweUp")]
public class PowerUPSO : ScriptableObject
{
    public PowerUpType powerUpType;
    public Sprite icon;  // Ícono que se mostrará en la UI, si lo deseas
    public float duration;  // Duración del power-up

}
