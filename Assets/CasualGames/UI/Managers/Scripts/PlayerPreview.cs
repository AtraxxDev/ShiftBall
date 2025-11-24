using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPreview : MonoBehaviour
{
    [SerializeField] private Image previewImage;

    private void OnEnable()
    {
        EventManager.StartListening("OnItemSelected",SkinSelected);
    }

   

    private void OnDisable()
    {
        EventManager.StopListening("OnItemSelected",SkinSelected);

    }
    
    private void SkinSelected(object param)
    {
        if (param is ShopItemModel item)
        {
            SetSkin(item.Icon);
        }
    }

    public void SetSkin(Sprite skinSprite)
    {
        if (previewImage != null && skinSprite != null)
        {
            previewImage.sprite = skinSprite;
        }
    }
}
