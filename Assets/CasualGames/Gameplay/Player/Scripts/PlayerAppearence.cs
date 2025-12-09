using System.Linq;
using UnityEngine;
using Sirenix.OdinInspector;
using TB_Tools;

public class PlayerAppearence : MonoBehaviour
{
    [SerializeField] private SpriteRenderer playerRenderer;

    [ShowInInspector, ReadOnly]
    private string currentSkinId;

    private const ItemCategory TARGET_CATEGORY = ItemCategory.Skin;

    private void OnEnable()
    {
        EventManager.StartListening("OnItemSelected", OnItemSelected);
    }

    private void OnDisable()
    {
        EventManager.StopListening("OnItemSelected", OnItemSelected);
    }

    private void Start()
    {
        // Cargar selección guardada para la categoría Skin
        string savedSkinId = PlayerPrefs.GetString("Selected_Skin", "");
        print(savedSkinId);

        if (string.IsNullOrEmpty(savedSkinId))
        {
            var defaultItem = ShopManager.Instance.GetDefaultItemByCategory(TARGET_CATEGORY);
            if (defaultItem != null)
                savedSkinId = defaultItem.Id;
                print(savedSkinId);
            
        }

        ApplySkin(savedSkinId);
    }

    private void OnItemSelected(object param)
    {
        if (param is not ShopItemModel item) return;

        // Ignorar selecciones de otras categorías
        if (item.Category != TARGET_CATEGORY) return;

        ApplySkin(item.Id);
    }

    private void ApplySkin(string skinId)
    {
        if (string.IsNullOrEmpty(skinId))
        {
            var def = ShopManager.Instance.GetDefaultItemByCategory(TARGET_CATEGORY);
            if (def != null) skinId = def.Id;
        }

        var item = ShopManager.Instance.shopDatabase.items
            .FirstOrDefault(i => i.Id == skinId);

        if (item == null)
        {
            Debug.LogError($"Skin '{skinId}' no encontrada, aplicando default.");

            item = ShopManager.Instance.GetDefaultItemByCategory(TARGET_CATEGORY);
            if (item == null) return;
        }

        playerRenderer.sprite = item.Icon;
        currentSkinId = item.Id;

        Debug.Log("Skin aplicada: " + currentSkinId);
    }

    public string GetCurrentSkinId() => currentSkinId;
}
