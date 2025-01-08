using UnityEngine;
using UnityEngine.UI;
using TMPro;
using TB_Tools;

public class ShopItem : MonoBehaviour
{
    [Header("Data Currency")]
    public int cost;
    public CurrencyType currencyType;
    public bool isUnlocked;
    public string itemKey; // Clave única para este ítem en PlayerPrefs

    [Space(5)]
    [SerializeField] private ItemCategory category; // Cambiado de string a ItemCategory
    [SerializeField] private string defaultNameKey;

    [Header("UI")]
    public Button buyButton;
    public Button selectButton;
    public TMP_Text costText;

    private static ShopItem currentSelectedItemPalette;
    private static ShopItem currentSelectedItemTrail;
    private static ShopItem currentSelectedItemExplosion;

    private Image selectButtonImage;

    private void Start()
    {
        selectButtonImage = selectButton.GetComponent<Image>(); // Asignar la imagen del botón

        selectButton.onClick.AddListener(SelectItem);
        buyButton.onClick.AddListener(BuyItem); // Asegúrate de agregar el listener al botón de compra

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
        if (PlayerPrefs.HasKey(itemKey))
        {
            isUnlocked = PlayerPrefs.GetInt(itemKey, 0) == 1;
        }
        else
        {
            if (itemKey == defaultNameKey)
            {
                isUnlocked = true;
                PlayerPrefs.SetInt(itemKey, 1);
                PlayerPrefs.Save();
            }
            else
            {
                isUnlocked = false;
            }
        }
    }

    private void SetSelectedItem()
    {
        string selectedItemKey = PlayerPrefs.GetString(GetSelectedKeyForCategory(), string.Empty);

        if (itemKey == selectedItemKey && isUnlocked)
        {
            selectButtonImage.color = new Color32(95, 100, 236, 255); // Color verde
            SetCurrentSelectedItem(this);
        }
        else
        {
            selectButtonImage.color = Color.red; // Color rojo si no es seleccionado
        }
    }

    private void AutoSelectDefaultItem()
    {
        string selectedItemKey = PlayerPrefs.GetString(GetSelectedKeyForCategory(), string.Empty);
        if (string.IsNullOrEmpty(selectedItemKey))
        {
            if (itemKey == defaultNameKey && isUnlocked)
            {
                selectButtonImage.color = new Color32(95, 100, 236, 255); // Color azul
                SetCurrentSelectedItem(this);
                PlayerPrefs.SetString(GetSelectedKeyForCategory(), itemKey);
                PlayerPrefs.Save();
            }
        }
    }

    private string GetSelectedKeyForCategory()
    {
        return $"Selected_{category}"; // Ejemplo: "Selected_Palette", "Selected_Trail", "Selected_Explosion"
    }

    private void SetCurrentSelectedItem(ShopItem item)
    {
        switch (category)
        {
            case ItemCategory.Palette:
                currentSelectedItemPalette = item;
                break;
            case ItemCategory.Trail:
                currentSelectedItemTrail = item;
                break;
            case ItemCategory.Explosion:
                currentSelectedItemExplosion = item;
                break;
        }
    }

    private ShopItem GetCurrentSelectedItem()
    {
        return category switch
        {
            ItemCategory.Palette => currentSelectedItemPalette,
            ItemCategory.Trail => currentSelectedItemTrail,
            ItemCategory.Explosion => currentSelectedItemExplosion,
            _ => null
        };
    }

    public void BuyItem()
    {
        if (!isUnlocked)
        {
            bool purchaseSuccessful = false;

            if (currencyType == CurrencyType.Coins)
            {
                purchaseSuccessful = CoinManager.Instance.SpendCoins(cost);
            }
            else if (currencyType == CurrencyType.Stars)
            {
                purchaseSuccessful = CoinManager.Instance.SpendStars(cost);
            }
            else if (currencyType == CurrencyType.AD)
            {
                Rewarded.Instance.ShowRewardedAd();
                purchaseSuccessful = true;
            }

            if (purchaseSuccessful)
            {
                UnlockItem();
            }
        }
    }

    public void UnlockItem()
    {
        isUnlocked = true;
        SaveItemState();
        UpdateUI();
    }

    public void SelectItem()
    {
        if (isUnlocked)
        {
            var currentItem = GetCurrentSelectedItem();
            if (currentItem != null && currentItem != this)
            {
                currentItem.DeselectItem();
            }

            SetCurrentSelectedItem(this);

            selectButtonImage.color = new Color32(95, 100, 236, 255);
            PlayerPrefs.SetString(GetSelectedKeyForCategory(), itemKey);
            PlayerPrefs.Save();
        }
    }

    private void DeselectItem()
    {
        selectButtonImage.color = Color.red;
        SetCurrentSelectedItem(null);
    }

    private void SaveItemState()
    {
        PlayerPrefs.SetInt(itemKey, isUnlocked ? 1 : 0);
        PlayerPrefs.Save();
    }

    private void UpdateUI()
    {
        if (isUnlocked)
        {
            buyButton.gameObject.SetActive(false);
            selectButton.gameObject.SetActive(true);
            costText.text = "Unlocked";

            if (GetCurrentSelectedItem() == this)
            {
                selectButtonImage.color = new Color32(95, 100, 236, 255);
            }
            else
            {
                selectButtonImage.color = Color.red;
            }
        }
        else
        {
            if (currencyType == CurrencyType.AD)
            {
                costText.text = "ADS";
                buyButton.gameObject.SetActive(true);
            }
            else
            {
                selectButton.gameObject.SetActive(false);
                costText.text = $"Cost: {cost} {(currencyType == CurrencyType.Coins ? "Coins" : "Stars")}";
            }

            buyButton.interactable = (currencyType == CurrencyType.Coins && CoinManager.Instance.Coins >= cost) ||
                                      (currencyType == CurrencyType.Stars && CoinManager.Instance.Stars >= cost);
        }
    }

    private void OnCurrencyChanged(int _)
    {
        UpdateUI();
    }
}
