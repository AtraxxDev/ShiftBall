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
    public string itemKey; // Clave �nica para este �tem en PlayerPrefs

    [Header("UI")]
    public Button buyButton;
    public Button selectButton;
    public TMP_Text costText;

    private static ShopItem currentSelectedItem;
    private Image selectButtonImage;

    private void Start()
    {
        selectButtonImage = selectButton.GetComponent<Image>(); // Asignar la imagen del bot�n

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
        // Cargar el estado del �tem desde PlayerPrefs
        if (PlayerPrefs.HasKey(itemKey))
        {
            isUnlocked = PlayerPrefs.GetInt(itemKey, 0) == 1;
        }
        else
        {
            // Si es el primer �tem (por ejemplo, el predeterminado), lo marcamos como desbloqueado
            if (itemKey == "Palette_0")  // Aqu� puedes verificar la clave de tu �tem predeterminado
            {
                isUnlocked = true;
                PlayerPrefs.SetInt(itemKey, 1);  // Guardamos el estado de desbloqueo
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
        // Cargar el �tem seleccionado
        string selectedItemKey = PlayerPrefs.GetString("SelectedColorPalette", string.Empty);

        // Verificar si este �tem es el seleccionado
        if (itemKey == selectedItemKey && isUnlocked)
        {
            selectButtonImage.color = new Color32(95, 100, 236, 255); // Color verde
            currentSelectedItem = this; // Establecer este �tem como el �tem seleccionado
        }
        else
        {
            selectButtonImage.color = Color.red; // Color rojo si no es seleccionado
        }
    }

    private void AutoSelectDefaultItem()
    {
        // Si no hay un �tem seleccionado en PlayerPrefs, marcar el predeterminado como seleccionado
        string selectedItemKey = PlayerPrefs.GetString("SelectedColorPalette", string.Empty);
        if (string.IsNullOrEmpty(selectedItemKey))
        {
            if (itemKey == "Palette_0" && isUnlocked)
            {
                selectButtonImage.color = new Color32(95, 100, 236, 255); // Color azul (seleccionado)
                currentSelectedItem = this; // Establecer el �tem predeterminado como el seleccionado
                PlayerPrefs.SetString("SelectedColorPalette", itemKey); // Guardar la selecci�n
                PlayerPrefs.Save();
            }
        }
    }

    private void SaveItemState()
    {
        // Guardar el estado del �tem en PlayerPrefs
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
            // Primero, deselecciona el �tem actualmente seleccionado si existe
            if (currentSelectedItem != null && currentSelectedItem != this)
            {
                Debug.Log($"Deseleccionando �tem anterior: {currentSelectedItem.gameObject.name}");
                currentSelectedItem.DeselectItem(); // Deseleccionar el �tem anterior
            }

            // Establecer este �tem como el seleccionado
            currentSelectedItem = this;
            Debug.Log($"�tem seleccionado: {gameObject.name}");

            selectButtonImage = selectButton.GetComponent<Image>(); // Asignar la imagen del bot�n

            // Cambiar el color del bot�n a verde
            selectButtonImage.color = new Color32(95, 100, 236, 255);

            // Guardar el �tem seleccionado en PlayerPrefs
            PlayerPrefs.SetString("SelectedColorPalette", itemKey);
            PlayerPrefs.Save();
        }
    }

    private void DeselectItem()
    {
        selectButtonImage.color = Color.red; // Cambiar el color del bot�n a rojo
        // Resetear currentSelectedItem si es necesario
        if (currentSelectedItem == this)
        {
            currentSelectedItem = null;
        }
    }

    private void UpdateUI()
    {
        if (isUnlocked)
        {
            buyButton.gameObject.SetActive(false);
            selectButton.gameObject.SetActive(true);
            costText.text = "Unlocked";

            // Si este �tem es el seleccionado actualmente, establece su color en verde
            if (currentSelectedItem == this)
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
