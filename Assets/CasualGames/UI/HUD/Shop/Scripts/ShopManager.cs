using System;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using TB_Tools;
using UnityEditor.Overlays;
using UnityEngine.Serialization;

[HideMonoScript]
public class ShopManager : MonoBehaviour
{
    public static ShopManager Instance {  get; private set; }
    
    [Title("References")]
    public ShopDatabase shopDatabase;
    public CurrencyService currencyService;
    [FormerlySerializedAs("playerPreview")] [SerializeField] private PlayerPreview previewPlayer;
    
    private HashSet<string> _purchasedItems = new HashSet<string>();
    private string _selectedItemId;

    
    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(this);
        
        LoadData();
    }
    
    public void PrintPurchasedItems()
    {
        Debug.Log("Items comprados:");
        foreach (var id in _purchasedItems)
        {
            Debug.Log(id);
        }
    }


    private void LoadData()
    {
        print("Loading Shop Data...");
        
        _selectedItemId = PlayerPrefs.GetString("SelectedItemId",GetDefaultItem().Id);
        string savedPurchased = PlayerPrefs.GetString("PurchasedItemId","");
        if (!string.IsNullOrEmpty(savedPurchased))
        {
            _purchasedItems = new  HashSet<string>(savedPurchased.Split(',')); 
        }

        if (!_purchasedItems.Contains(_selectedItemId))
            _purchasedItems.Add(_selectedItemId);
        
        print("Shop Data loaded successfully.");
        
        PrintPurchasedItems();
        
    }

    private void SaveData()
    {
        PlayerPrefs.SetString("SelectedItemId", _selectedItemId);
        PlayerPrefs.SetString("PurchasedItemId", string.Join(",", _purchasedItems));
        PlayerPrefs.Save();
    }

    public ShopItemModel GetDefaultItem()
    {
        foreach (var item in shopDatabase.items)
        {
            if(item.IsDefaultItem) return item;
        }

        return shopDatabase.items[0];
    }
    
    public bool IsPurchased(string itemId) => _purchasedItems.Contains(itemId);
    public bool IsSelected(string itemId) => _selectedItemId == itemId;

    public void BuyItem(ShopItemModel item)
    {
        if (IsPurchased(item.Id)) return;

        // Validacion si el jugador tiene suficiente currency

        bool canBuy = currencyService.TrySpend(item.Currency, item.Cost);
        
        if (!canBuy)
        {
            Debug.Log("No tienes suficiente " + item.Currency + " para comprar " + item.Id);
            return;
        }
        
        _purchasedItems.Add(item.Id);
        SaveData();
        EventManager.TriggerEvent("OnItemPurchased",item);
    }

    public void SelectItem(ShopItemModel item)
    {
        if(!IsPurchased(item.Id)) return;
        _selectedItemId = item.Id;
        SaveData();
        
        EventManager.TriggerEvent("OnItemSelected",item);
    }
    
    
}
