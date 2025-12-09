using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[HideMonoScript]
public class ShopItemView : MonoBehaviour
{
    [Title("UI")]
    [SerializeField] private Button mainButton;
    [SerializeField] private Image icon;
    [SerializeField] private TMP_Text costText;
    [SerializeField] private GameObject checkmarkObject;
    [SerializeField] private GameObject currencyIcon;

    [Title("Data")]
    [SerializeField] private ShopItemModel itemData;

    private void OnEnable()
    {
        mainButton.onClick.AddListener(OnClicked);

        EventManager.StartListening("OnItemPurchased", OnItemPurchased);
        EventManager.StartListening("OnItemSelected", OnItemSelected);

        RefreshUI();
    }

    private void OnDisable()
    {
        mainButton.onClick.RemoveListener(OnClicked);

        EventManager.StopListening("OnItemPurchased", OnItemPurchased);
        EventManager.StopListening("OnItemSelected", OnItemSelected);
    }

    private void OnClicked()
    {
        var shop = ShopManager.Instance;

        if (!shop.IsPurchased(itemData.Id))
            shop.BuyItem(itemData);
        else
            shop.SelectItem(itemData);
    }

    private void OnItemPurchased(object param) => RefreshUI();
    private void OnItemSelected(object param) => RefreshUI();

    private void RefreshUI()
    {
        icon.sprite = itemData.Icon;

        bool purchased = ShopManager.Instance.IsPurchased(itemData.Id);
        bool selected = ShopManager.Instance.IsSelected(itemData.Id);

        checkmarkObject.SetActive(selected);
        currencyIcon.SetActive(!purchased);

        if (purchased)
        {
            costText.text = "";
        }
        else
        {
            costText.text = itemData.Cost.ToString();
            bool canBuy = ShopManager.Instance.currencyService.GetBalance(itemData.Currency) >= itemData.Cost;
            costText.color = canBuy ? Color.white : Color.red;
        }
    }
}