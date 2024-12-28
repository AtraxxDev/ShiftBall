using UnityEngine;
using TB_Tools;

[CreateAssetMenu(fileName = "New PowerUp", menuName = "Scriptable Objects/PoweUp")]
public class PowerUPSO : ScriptableObject
{
    public PowerUpType powerUpType;
    public Sprite icon;  // �cono que se mostrar� en la UI, si lo deseas
    public float duration;  // Duraci�n del power-up

}
