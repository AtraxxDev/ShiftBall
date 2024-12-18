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


    [Header("UI")]
    public Button buyButton;
    public Button selectButton;
    public TMP_Text costText;

    private void Start()
    {
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
        // Cargar el estado del ítem desde PlayerPrefs
        isUnlocked = PlayerPrefs.GetInt(itemKey, 0) == 1;
    }

    private void SaveItemState()
    {
        // Guardar el estado del ítem en PlayerPrefs
        PlayerPrefs.SetInt(itemKey, isUnlocked ? 1 : 0);
        PlayerPrefs.Save();
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
                buyButton.interactable = true;

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
            // Lógica para aplicar la paleta de colores, explosión o trail render.
            Debug.Log($"Item seleccionado: {gameObject.name}");
        }
    }

    private void OnRewardedAdCompleted(bool success)
    {
        if (success)
        {
            UnlockItem();
        }
    }

    private void UpdateUI()
    {
        if (isUnlocked)
        {
            buyButton.gameObject.SetActive(false);
            selectButton.gameObject.SetActive(true);
            costText.text = "Unlocked";
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