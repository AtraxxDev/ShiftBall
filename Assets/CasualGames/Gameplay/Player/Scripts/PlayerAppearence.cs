using System;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

public class PlayerAppearence : MonoBehaviour
{
   [SerializeField] private SpriteRenderer playerRenderer;

   [ShowInInspector,ReadOnly]
   private string currentSkinId;


   private void OnEnable()
   {
      EventManager.StartListening("OnItemSelected",OnSkinSelected);
   }

   private void OnDisable()
   {
      EventManager.StopListening("OnItemSelected",OnSkinSelected);

   }

   private void OnSkinSelected(object param)
   {
      if (param is ShopItemModel item)
      {
         ApplySkin(item.Id);
      }
        
   }

   private void Start()
   {
      string savedSkinId = PlayerPrefs.GetString("SelectedItemId", null);

      // Si no hay skin guardado, tomar el default del ShopManager
      if (string.IsNullOrEmpty(savedSkinId))
      {
         var defaultItem = ShopManager.Instance?.GetDefaultItem();
         if(defaultItem != null)
            savedSkinId = defaultItem.Id;
         currentSkinId = savedSkinId;
         print("Default SkinId: " + savedSkinId);
      }

      ApplySkin(savedSkinId);
   }

   private void ApplySkin(string skinId)
   {
      // Si no hay skinId válido, usar el default
      if (string.IsNullOrEmpty(skinId))
      {
         var defaultItem = ShopManager.Instance.GetDefaultItem();
         skinId = defaultItem?.Id;
         if (string.IsNullOrEmpty(skinId)) return; // No hay default asignado
      }

      Debug.Log($"Applying skinId: {skinId}");

      // Buscar el item en la base de datos
      var item = ShopManager.Instance.shopDatabase.items
         .FirstOrDefault(i => i.Id == skinId);

      // Si no se encuentra, usar default
      if (item == null)
      {
         Debug.LogError($"SkinId '{skinId}' no encontrado en la base de datos, aplicando default.");
         item = ShopManager.Instance.GetDefaultItem();
      }

      // Asignar sprite si existe
      if (item != null && item.Icon != null)
      {
         playerRenderer.sprite = item.Icon;
         currentSkinId = item.Id;
         Debug.Log("Assign skin: " + item.Id);
      }
      else
      {
         Debug.LogError("No hay sprite disponible para la skin: " + skinId);
      }
   }

   
   public string GetCurrentSkinId() => currentSkinId;
    
}