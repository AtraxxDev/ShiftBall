using UnityEngine;
using UnityEngine.UI;
using TB_Tools;

public class ChangeData : MonoBehaviour
{
    [SerializeField] private int dataID; // ID para seleccionar el dato
    [SerializeField] private DataType dataType; // Tipo de dato
    [SerializeField] private Button btn; // Bot�n para cambiar el dato

    private void Start()
    {
        btn.onClick.AddListener(OnButtonClick);
    }

    private void OnButtonClick()
    {
        switch (dataType)
        {
            case DataType.ColorPalette:
                ColorManager.Instance.ChangePalette(dataID);
                break;

            case DataType.TrailGradient:
                TrailManager.Instance.ChangeColorTrailID(dataID);
                break;

            case DataType.ParticleEffect:
                // Asumiendo que ParticleManager es una clase que maneja los efectos de part�culas
                //ParticleManager.Instance.ChangeParticleEffectID(dataID);
                break;
        }
    }


}
