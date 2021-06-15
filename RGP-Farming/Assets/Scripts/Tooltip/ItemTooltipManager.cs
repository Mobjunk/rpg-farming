using TMPro;
using UnityEngine;

public class ItemTooltipManager : TooltipManager<ItemTooltipManager>
{
    [SerializeField] private TextMeshProUGUI itemType;
    
    public override Vector2 MinSize()
    {
        return new Vector2(115, 80);
    }

    public override Vector2 StartPosition()
    {
        return new Vector2(45, 80);
    }

    public override void SetTooltip(AbstractItemData hoveredItem)
    {
        base.SetTooltip(hoveredItem);
        if(hoveredItem == null)
        {
            ResetTooltip();
            return;
        }
        
        itemType.text = $"{Utility.UppercaseFirst(hoveredItem.itemType.ToString().ToLower().Replace("_", " "))}";
    }
}
