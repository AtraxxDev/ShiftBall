using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

public class Page : MonoBehaviour
{
    [SerializeField] private Button button;
    [SerializeField] private Image image;
    [SerializeField] private GameObject areaShop;
    
    public bool isSelected;
    

    private void Awake()
    {
        button.onClick.AddListener(OnClick);
    }

    private void OnClick()
    {
        EventManager.TriggerEvent("OnPageSelected",this);
    }

    public void SetColor(Color color)
    {
        if (image != null)
            image.color = color;
    }
    
    public void SetAreaShop(bool active)
    {
        if (areaShop != null)
            areaShop.SetActive(active);
    }

   
}
