using TMPro;
using UnityEngine;

public class ItemTooltipManager : TooltipManager<ItemTooltipManager, AbstractItemData>
{
    [SerializeField] private TextMeshProUGUI _itemType;
    
    public override Vector2 MinSize()
    {
        return new Vector2(115, 80);
    }

    public override Vector2 StartPosition()
    {
        return new Vector2(45, 80);
    }

    public override bool SetTooltip(AbstractItemData pHoveredItem)
    {
        if (!base.SetTooltip(pHoveredItem))
        {
            return false;
        }
        if(pHoveredItem == null)
        {
            ResetTooltip();
            return false;
        }
        
        ItemName.text = $"{Utility.UppercaseFirst(pHoveredItem.itemName.ToLower())}";
        ItemDescription.text = $"{pHoveredItem.itemDescription}";
        _itemType.text = $"{Utility.UppercaseFirst(pHoveredItem.itemType.ToString().ToLower().Replace("_", " "))}";
        return true;
    }
}
