using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Sirenix.OdinInspector;
using TB_Tools;

[HideMonoScript]
public class ShopManager : MonoBehaviour
{
    public static ShopManager Instance { get; private set; }

    [Title("References")]
    public ShopDatabase shopDatabase;
    public CurrencyService currencyService;

    private HashSet<string> _purchasedItems = new();
    [SerializeField] private List<CategorySelection> selectedItems = new();

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(this);

        LoadData();
    }

    // ---------------------------
    //   PURCHASE / SELECTION
    // ---------------------------

    public bool IsPurchased(string id) => _purchasedItems.Contains(id);

    public bool IsSelected(string id)
    {
        var item = shopDatabase.items.FirstOrDefault(i => i.Id == id);
        if (item == null) return false;

        return GetSelectedItemId(item.Category) == id;
    }

    public string GetSelectedItemId(ItemCategory category)
    {
        var entry = selectedItems.Find(s => s.Category == category);
        return entry?.SelectedId;
    }

    public ShopItemModel GetSelectedItemByCategory(ItemCategory category)
    {
        string id = GetSelectedItemId(category);
        return shopDatabase.items.FirstOrDefault(i => i.Id == id);
    }

    public ShopItemModel GetDefaultItemByCategory(ItemCategory category)
    {
        return shopDatabase.items.FirstOrDefault(i => i.Category == category && i.IsDefaultItem);
    }

    // ---------------------------
    //          BUY
    // ---------------------------
    public void BuyItem(ShopItemModel item)
    {
        if (IsPurchased(item.Id)) return;

        bool canBuy = currencyService.TrySpend(item.Currency, item.Cost);
        if (!canBuy)
        {
            Debug.Log("No tienes suficiente " + item.Currency);
            AudioManager.Instance.PlaySFX("Error_UI");
            return;
        }

        _purchasedItems.Add(item.Id);
        AudioManager.Instance.PlaySFX("Spend_Shop");
        SaveData();

        EventManager.TriggerEvent("OnItemPurchased", item);
    }

    // ---------------------------
    //         SELECT
    // ---------------------------
    public void SelectItem(ShopItemModel item)
    {
        if (!IsPurchased(item.Id)) return;

        var entry = selectedItems.Find(s => s.Category == item.Category);

        if (entry == null)
        {
            selectedItems.Add(new CategorySelection
            {
                Category = item.Category,
                SelectedId = item.Id
            });
        }
        else
        {
            entry.SelectedId = item.Id;
        }

        SaveData();
        AudioManager.Instance.PlaySFX("SelectUI");
        EventManager.TriggerEvent("OnItemSelected", item);
    }

    // ---------------------------
    //       SAVE / LOAD
    // ---------------------------
    private void LoadData()
    {
        Debug.Log("Loading shop data...");

        // Load purchased items
        string purchased = PlayerPrefs.GetString("PurchasedItemId", "");
        if (!string.IsNullOrEmpty(purchased))
            _purchasedItems = new HashSet<string>(purchased.Split(','));

        // Load per-category selected items
        selectedItems.Clear();
        foreach (ItemCategory cat in Enum.GetValues(typeof(ItemCategory)))
        {
            string savedId = PlayerPrefs.GetString($"Selected_{cat}", "");

            if (string.IsNullOrEmpty(savedId))
            {
                var def = GetDefaultItemByCategory(cat);
                if (def != null)
                {
                    savedId = def.Id;
                    _purchasedItems.Add(def.Id);
                }
            }

            selectedItems.Add(new CategorySelection
            {
                Category = cat,
                SelectedId = savedId
            });
        }

        Debug.Log("Shop data loaded.");
    }

    private void SaveData()
    {
        PlayerPrefs.SetString("PurchasedItemId", string.Join(",", _purchasedItems));

        foreach (var entry in selectedItems)
            PlayerPrefs.SetString($"Selected_{entry.Category}", entry.SelectedId);

        PlayerPrefs.Save();
    }
}



[System.Serializable]
public class CategorySelection
{
    public ItemCategory Category;
    public string SelectedId;
}

[System.Serializable]
public class SerializationWrapper<T>
{
    public List<T> list;
    public SerializationWrapper(List<T> list) => this.list = list;
}
