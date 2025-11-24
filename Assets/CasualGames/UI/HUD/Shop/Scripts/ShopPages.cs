using System;
using Sirenix.OdinInspector;
using UnityEngine;

public class ShopPages : MonoBehaviour
{
    [Title("Pages")]
    [SerializeField] private Page[] pages;
    [SerializeField] private Color[] colors;
    
    [ShowInInspector,ReadOnly]
    private Page _currentPage;

    private void Start()
    {
        ChangePage(pages[0]);
    }

    private void OnEnable()
    {
       EventManager.StartListening("OnPageSelected", OnPageSelected);
    }

    private void OnDisable()
    {
       EventManager.StopListening("OnPageSelected", OnPageSelected); }

    private void OnPageSelected(object param)
    {
        if (param is Page page)
        {
            ChangePage(page);
        }
    }

    private void ChangePage(Page selectedPage)
    {
        foreach (var page in pages)
        {
            bool isSelected = page == selectedPage;
            page.isSelected = isSelected;
            
            Color newColor = isSelected ? colors[0] : colors[1];
            page.SetAreaShop(isSelected);
            page.SetColor(newColor);
        }
        
        _currentPage = selectedPage;
    }
    
    
}
