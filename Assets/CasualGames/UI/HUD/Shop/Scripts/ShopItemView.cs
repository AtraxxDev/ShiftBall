using System;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[HideMonoScript]
public class ShopItemView : MonoBehaviour
{
    [Title("UI")]
    [SerializeField, Required] private Button mainButton;
    [SerializeField, Required] private Image icon;
    [SerializeField, Required] private TMP_Text costText;
    [SerializeField, Required] private GameObject checkmarkObject; 
    [SerializeField, Required] private GameObject currencyIcon; 

    [Title("Data")]
    [InlineEditor] 
    [SerializeField, Required] private ShopItemModel itemData;

    private void OnEnable()
    {
        mainButton.onClick.AddListener(OnButtonClicked);
        EventManager.StartListening("OnItemPurchased",OnItemPurchased);
        EventManager.StartListening("OnItemSelected",OnItemSelected);
        RefreshUI();
    }

    private void OnDisable()
    {
        mainButton.onClick.RemoveListener(OnButtonClicked);
        EventManager.StopListening("OnItemPurchased",OnItemPurchased);
        EventManager.StopListening("OnItemSelected",OnItemSelected);
    }
    
    

    private void OnButtonClicked()
    {
        if(!ShopManager.Instance.IsPurchased(itemData.Id))
            ShopManager.Instance.BuyItem(itemData);
        else
            ShopManager.Instance.SelectItem(itemData);
    }

    private void OnItemPurchased(object param)
    {
        // if(param is ShopItemModel purchasedItem && purchasedItem.Id == itemData.Id)
            RefreshUI();
    }

    private void OnItemSelected(object param)
    {
        RefreshUI();
    }

    private void RefreshUI()
    {
        icon.sprite = itemData.Icon;

        bool isPurchased = ShopManager.Instance.IsPurchased(itemData.Id);
        bool isSelected = ShopManager.Instance.IsSelected(itemData.Id);

        // Mostrar checkmark solo si está seleccionado
        checkmarkObject.SetActive(isSelected);

        // Mostrar moneda solo si no está comprado
        currencyIcon.SetActive(!isPurchased);

        // Lógica del texto del costo:
        // - Si está seleccionado -> vacío
        // - Si ya fue comprado pero no seleccionado -> vacío
        // - Si no está comprado -> mostrar costo
        if (isPurchased || isSelected)
        {
            costText.text = "";
            costText.color = Color.white; // el color no importa, está vacío
        }
        else
        {
            costText.text = itemData.Cost.ToString();

            // Si no hay suficiente currency, mostrar en rojo
            bool canBuy = ShopManager.Instance.currencyService.GetBalance(itemData.Currency) >= itemData.Cost;
            costText.color = canBuy ? Color.white : Color.red;
        }
    }


}
