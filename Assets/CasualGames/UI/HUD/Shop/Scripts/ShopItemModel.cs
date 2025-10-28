using Sirenix.OdinInspector;
using TB_Tools;
using UnityEngine;

[CreateAssetMenu(menuName = "Shop/Item", fileName = "NewShopItem")]
[HideMonoScript]
public class ShopItemModel : ScriptableObject
{
    [TitleGroup("Basic Info", boldTitle: true)]
    [HorizontalGroup("Basic Info/Split", Width = 80)]
    [PreviewField(80), HideLabel]
    public Sprite Icon;

    [VerticalGroup("Basic Info/Split/Right")]
    [LabelWidth(100)]
    public string Id;

    [VerticalGroup("Basic Info/Split/Right")]
    [LabelWidth(100)]
    public string DisplayName;

    [TitleGroup("Shop Data", boldTitle: true)]
    [LabelWidth(100)]
    public int Cost = 50;

    [LabelWidth(100)]
    public CurrencyType Currency = CurrencyType.Coins;

    [LabelWidth(100)]
    public ItemCategory Category = ItemCategory.Palette;

    [LabelWidth(150)]
    public bool IsDefaultItem = false;
}