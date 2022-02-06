using TMPro;
using UnityEngine;

public class ItemTooltipManager : TooltipManager<ItemTooltipManager>
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

    public override void SetTooltip(AbstractItemData pHoveredItem)
    {
        base.SetTooltip(pHoveredItem);
        if(pHoveredItem == null)
        {
            ResetTooltip();
            return;
        }
        
        _itemType.text = $"{Utility.UppercaseFirst(pHoveredItem.itemType.ToString().ToLower().Replace("_", " "))}";
    }
}
