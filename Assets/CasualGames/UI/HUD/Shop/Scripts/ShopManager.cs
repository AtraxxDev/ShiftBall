using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

[HideMonoScript]
public class ShopManager : MonoBehaviour
{
    [Title("Shop Setup")]
    [SerializeField, Required, LabelText("Currency Service")]
    private CurrencyService currencyService;

    [SerializeField, Required, LabelText("Save Service (Optional)")]
    private PlayerPrefsSaveService saveService;

    [Title("Registered Views")]
    [SerializeField, Required, LabelText("Shop Item Views")]
    private List<ShopItemView> shopItemViews = new();

    private readonly Dictionary<string, ShopItemModel> items = new();
    private string selectedItemId;

    public bool IsSelected(string id) => selectedItemId == id;

    private void Awake()
    {
        InitializeShop();
    }

    [Button("Initialize Shop", ButtonSizes.Large)]
    private void InitializeShop()
    {
        items.Clear();

        foreach (var view in shopItemViews)
        {
            if (view == null) continue;

            var data = view.GetItemData();
            if (data == null) continue;

            items[data.Id] = data;
            view.Init(this);
        }

        // ---- Persistencia de selección ----
        selectedItemId = PlayerPrefs.GetString("SelectedItem", null);

        if (string.IsNullOrEmpty(selectedItemId))
        {
            foreach (var item in items.Values)
            {
                if (item.IsDefaultItem)
                {
                    selectedItemId = item.Id;
                    PlayerPrefs.SetString("SelectedItem", item.Id);
                    break;
                }
            }
        }

        // Refrescar todas las vistas
        foreach (var view in shopItemViews)
        {
            view.Refresh();
        }

        Debug.Log($"Shop initialized with {items.Count} items.");
    }


    public bool TryBuy(string id)
    {
        if (!items.ContainsKey(id)) return false;

        if (IsUnlocked(id)) return false;

        if (currencyService.TrySpend(items[id].Currency, items[id].Cost))
        {
            saveService?.SaveBool(id, true); // marca desbloqueado en runtime/save
            //RefreshAllViews();
            return true;
        }
        return false;
    }


    public void SelectItem(string id)
    {
        if (!IsUnlocked(id))
        {
            Debug.Log($"Cannot select locked item: {id}");
            return;
        }

        selectedItemId = id;
        PlayerPrefs.SetString("SelectedItem", id);
        PlayerPrefs.Save();

        foreach (var view in shopItemViews)
        {
            view.Refresh();
        }
        Debug.Log($"Selected item: {id}");
    }

    public bool IsUnlocked(string id)
    {
        return items.ContainsKey(id) && (items[id].IsDefaultItem || PlayerPrefs.GetInt(id, 0) == 1);
    }
}
