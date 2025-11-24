using Sirenix.OdinInspector;
using TB_Tools;
using UnityEngine;

[CreateAssetMenu(menuName = "Shop/Item", fileName = "NewShopItem")]
[HideMonoScript]
public class ShopItemModel : ScriptableObject
{
    [HorizontalGroup("Header")]
    [PreviewField(80)]
    public Sprite Icon;

    [VerticalGroup("Header/Right")]
    [LabelWidth(100)]
    public string Id;

    [TitleGroup("Pricing")]
    [LabelWidth(100)]
    public int Cost = 50;

    [LabelWidth(100)]
    public CurrencyType Currency = CurrencyType.Coins;

    [TitleGroup("Category")]
    [LabelWidth(100)]
    public ItemCategory Category = ItemCategory.Skin;

    [TitleGroup("Status")]
    [LabelWidth(150)]
    public bool IsDefaultItem = false;
    
}