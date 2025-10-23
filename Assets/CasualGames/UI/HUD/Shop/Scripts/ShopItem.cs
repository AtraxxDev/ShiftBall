using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Sirenix.OdinInspector;
using TB_Tools;

public class ShopItem : MonoBehaviour
{
    [BoxGroup("Currency Data",centerLabel: true)]
    [SerializeField] private int cost;
    [BoxGroup("Currency Data")]
    [SerializeField] private CurrencyType currencyType;
    [BoxGroup("Currency Data")]
    [SerializeField] private bool isUnlocked;
    [BoxGroup("Currency Data")]
    [SerializeField] private string itemKey;
    [BoxGroup("Currency Data")]
    [SerializeField] private ItemCategory category;
    [BoxGroup("Currency Data")]
    [SerializeField] private string defaultNameKey;

    [BoxGroup("UI Elements", centerLabel: true)]
    [SerializeField] private Button buyButton;
    [BoxGroup("UI Elements")]
    [SerializeField] private Button selectButton;
    [BoxGroup("UI Elements")]
    [SerializeField] private TMP_Text costText;
    [BoxGroup("UI Elements")]
    [SerializeField] private Image spriteADS;

    [FoldoutGroup("Colors")]
    [SerializeField, ColorUsage(false)] private Color selectedColor = new Color32(95, 100, 236, 255);
    [FoldoutGroup("Colors")]
    [SerializeField, ColorUsage(false)] private Color deselectedColor = new Color32(95, 237, 110, 255);
    [FoldoutGroup("Colors")]
    [SerializeField, ColorUsage(false)] private Color adButtonColor = new Color32(155, 95, 236, 255);

    private static ShopItem _currentSelectedItemPalette;
    private static ShopItem _currentSelectedItemTrail;
    private static ShopItem _currentSelectedItemExplosion;

    private Image _selectButtonImage;

    private void Start()
    {
        _selectButtonImage = selectButton.GetComponent<Image>();
        selectButton.onClick.AddListener(SelectItem);
        buyButton.onClick.AddListener(BuyItem);

        LoadItemState();
        UpdateUI();

        CoinManager.Instance.OnCoinsChanged += OnCurrencyChanged;
        CoinManager.Instance.OnStarsChanged += OnCurrencyChanged;
    }

    private void OnDestroy()
    {
        CoinManager.Instance.OnCoinsChanged -= OnCurrencyChanged;
        CoinManager.Instance.OnStarsChanged -= OnCurrencyChanged;
    }

    private void LoadItemState()
    {
        LoadUnlockState();
        SetSelectedItem();
        AutoSelectDefaultItem();
    }

    private void LoadUnlockState()
    {
        isUnlocked = PlayerPrefs.GetInt(itemKey, itemKey == defaultNameKey ? 1 : 0) == 1;

        if (!PlayerPrefs.HasKey(itemKey) && isUnlocked)
        {
            PlayerPrefs.SetInt(itemKey, 1);
            PlayerPrefs.Save();
        }
    }

    private void SetSelectedItem()
    {
        string selectedItemKey = PlayerPrefs.GetString(GetSelectedKeyForCategory(), string.Empty);
        if (itemKey == selectedItemKey && isUnlocked)
        {
            HighlightSelectButton(selectedColor);
            SetCurrentSelectedItem(this);
        }
        else
        {
            HighlightSelectButton(deselectedColor);
        }
    }

    private void AutoSelectDefaultItem()
    {
        if (string.IsNullOrEmpty(PlayerPrefs.GetString(GetSelectedKeyForCategory(), string.Empty)) && itemKey == defaultNameKey && isUnlocked)
        {
            HighlightSelectButton(selectedColor);
            SetCurrentSelectedItem(this);
            PlayerPrefs.SetString(GetSelectedKeyForCategory(), itemKey);
            PlayerPrefs.Save();
        }
    }

    private string GetSelectedKeyForCategory() => $"Selected_{category}";

    private void SetCurrentSelectedItem(ShopItem item)
    {
        switch (category)
        {
            case ItemCategory.Palette:
                _currentSelectedItemPalette = item;
                break;
            case ItemCategory.Trail:
                _currentSelectedItemTrail = item;
                break;
            case ItemCategory.Explosion:
                _currentSelectedItemExplosion = item;
                break;
        }
    }

    private ShopItem GetCurrentSelectedItem() => category switch
    {
        ItemCategory.Palette => _currentSelectedItemPalette,
        ItemCategory.Trail => _currentSelectedItemTrail,
        ItemCategory.Explosion => _currentSelectedItemExplosion,
        _ => null
    };

    public void BuyItem()
    {
        if (isUnlocked) return;

        bool purchaseSuccessful = currencyType switch
        {
            CurrencyType.Coins => CoinManager.Instance.SpendCoins(cost),
            CurrencyType.Stars => CoinManager.Instance.SpendStars(cost),
            CurrencyType.AD => HandleAdPurchase(),
            _ => false
        };

        if (purchaseSuccessful) UnlockItem();
    }

    private bool HandleAdPurchase()
    {
        return false; // Defer purchase until the ad is watched
    }

    private void OnAdWatched() => UnlockItem();

    public void UnlockItem()
    {
        isUnlocked = true;
        SaveItemState();
        UpdateUI();
    }

    public void SelectItem()
    {
        if (!isUnlocked) return;

        GetCurrentSelectedItem()?.DeselectItem();
        SetCurrentSelectedItem(this);

        HighlightSelectButton(selectedColor);
        PlayerPrefs.SetString(GetSelectedKeyForCategory(), itemKey);
        PlayerPrefs.Save();
    }

    private void DeselectItem()
    {
        HighlightSelectButton(deselectedColor);
        SetCurrentSelectedItem(null);
    }

    private void SaveItemState()
    {
        PlayerPrefs.SetInt(itemKey, isUnlocked ? 1 : 0);
        PlayerPrefs.Save();
    }

    private void UpdateUI()
    {
        buyButton.gameObject.SetActive(!isUnlocked);
        selectButton.gameObject.SetActive(isUnlocked);

        if (isUnlocked)
        {
            costText.text = "Unlocked";
            HighlightSelectButton(GetCurrentSelectedItem() == this ? selectedColor : deselectedColor);
        }
        else
        {
            HandleLockedStateUI();
        }
    }

    private void HandleLockedStateUI()
    {
        if (currencyType == CurrencyType.AD)
        {
            spriteADS?.gameObject.SetActive(true);
            costText.gameObject.SetActive(false);
            buyButton.GetComponent<Image>().color = adButtonColor;
            buyButton.interactable = true; // Aseguramos que el bot�n siempre sea interactuable para anuncios
        }
        else
        {
            spriteADS?.gameObject.SetActive(false);
            costText.text = $"{cost} {(currencyType == CurrencyType.Coins ? "Coins" : "Stars")}";
            buyButton.interactable = (currencyType == CurrencyType.Coins && CoinManager.Instance.Coins >= cost) ||
                                      (currencyType == CurrencyType.Stars && CoinManager.Instance.Stars >= cost);
        }
    }


    private void HighlightSelectButton(Color color) => _selectButtonImage.color = color;

    private void OnCurrencyChanged(int _) => UpdateUI();
}
