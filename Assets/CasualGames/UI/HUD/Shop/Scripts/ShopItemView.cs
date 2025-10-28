using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[HideMonoScript]
public class ShopItemView : MonoBehaviour
{
    [TitleGroup("UI Elements", boldTitle: true)]
    [BoxGroup("UI Elements/Buttons", centerLabel: true)]
    [SerializeField, Required, LabelText("Buy Button")] private Button buyButton;

    [BoxGroup("UI Elements/Buttons")]
    [SerializeField, Required, LabelText("Select Button")] private Button selectButton;

    [BoxGroup("UI Elements/Text & Icon", centerLabel: true)]
    [SerializeField, Required, LabelText("Cost Text")] private TMP_Text costText;

    [BoxGroup("UI Elements/Text & Icon")]
    [SerializeField, Required, LabelText("Item Icon")] private Image icon;

    [TitleGroup("Data", boldTitle: true)]
    [InlineEditor(InlineEditorModes.GUIOnly)]
    [SerializeField, Required, LabelText("Item Data (Scriptable)")]
    private ShopItemModel itemData;

    [TitleGroup("Configuration", boldTitle: true)]
    [BoxGroup("Configuration/Colors", centerLabel: true)]
    [SerializeField, ColorUsage(false), LabelText("Selected Color")]
    private Color selectedColor = new Color32(100, 200, 255, 255);

    [BoxGroup("Configuration/Colors")]
    [SerializeField, ColorUsage(false), LabelText("Default Color")]
    private Color defaultColor = Color.white;

    private ShopManager shopManager;
    
    // --- Runtime State (Visible but ReadOnly) ---
    [TitleGroup("Runtime State", boldTitle: true)]
    [ShowInInspector, ReadOnly, LabelText("Is Unlocked")]
    private bool IsUnlocked => shopManager != null && shopManager.IsUnlocked(itemData.Id);

    [ShowInInspector, ReadOnly, LabelText("Is Selected")]
    private bool IsSelected => shopManager != null && shopManager.IsSelected(itemData.Id);

    // ------------------------------ Public API ------------------------------
    public ShopItemModel GetItemData() => itemData;

    [Button("Initialize Item", ButtonSizes.Large)]
    public void Init(ShopManager manager)
    {
        shopManager = manager;

        buyButton.onClick.RemoveAllListeners();
        buyButton.onClick.AddListener(OnBuyClicked);

        selectButton.onClick.RemoveAllListeners();
        selectButton.onClick.AddListener(OnSelectClicked);

        Refresh();
    }

    [Button("Refresh UI", ButtonSizes.Medium)]
    public void Refresh()
    {
        if (itemData == null || shopManager == null)
        {
            Debug.LogWarning($"[{name}] ShopItemView not initialized correctly.");
            return;
        }

        bool unlocked = IsUnlocked;
        bool selected = IsSelected;

        buyButton.gameObject.SetActive(!unlocked);
        selectButton.gameObject.SetActive(unlocked);

        costText.text = selected ? "Selected" : (unlocked ? "Unlocked" : $"{itemData.Cost} {itemData.Currency}");
        icon.sprite = itemData.Icon;
        selectButton.image.color = selected ? selectedColor : defaultColor;
    }

    private void OnBuyClicked()
    {
        if (shopManager.TryBuy(itemData.Id))
        {
            Refresh();
        }
        else
        {
            Debug.Log($"[ShopItemView] Purchase failed for {itemData.Id}");
        }
    }

    private void OnSelectClicked()
    {
        shopManager.SelectItem(itemData.Id);
        Refresh();
    }
}
