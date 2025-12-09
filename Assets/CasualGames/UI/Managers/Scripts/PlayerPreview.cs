using System;
using TB_Tools;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPreview : MonoBehaviour
{
    [SerializeField] private Image previewImage;
    
    private void OnEnable()
    {
        EventManager.StartListening("OnItemSelected", OnSkinSelected);
    }

    private void OnDisable()
    {
        EventManager.StopListening("OnItemSelected", OnSkinSelected);
    }

    private void Start()
    {
        // Al activarse, aplicar la skin guardada
        ApplySavedSkin();
    }

    private void OnSkinSelected(object param)
    {
        if (param is ShopItemModel item)
        {
            // Solo reaccionar si la categoría es Skin
            if (item.Category == ItemCategory.Skin)
                SetSkin(item.Icon);
        }
    }

    private void ApplySavedSkin()
    {
        // Pedimos al ShopManager la skin actualmente seleccionada
        var selected = ShopManager.Instance.GetSelectedItemByCategory(ItemCategory.Skin);

        if (selected != null && selected.Icon != null)
        {
            SetSkin(selected.Icon);
        }
        else
        {
            // fallback por si no encuentra nada
            var defaultSkin = ShopManager.Instance.GetDefaultItemByCategory(ItemCategory.Skin);
            if (defaultSkin != null)
                SetSkin(defaultSkin.Icon);
        }
    }

    public void SetSkin(Sprite skinSprite)
    {
        if (previewImage != null && skinSprite != null)
            previewImage.sprite = skinSprite;
    }
}