using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangePallete : MonoBehaviour
{
    [SerializeField] private int palleteValue;
    [SerializeField] private Button btn;

    private void Start()
    {
        btn.onClick.AddListener(() => ColorManager.Instance.ChangePalette(palleteValue));
    }

    
}
