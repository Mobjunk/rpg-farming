using System;
using TMPro;
using UnityEngine;

public class CollectionTooltipManager : TooltipManager<CollectionTooltipManager, CollectionLogEntry>
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

    public override bool SetTooltip(CollectionLogEntry pHoveredItem)
    {
        if (!base.SetTooltip(pHoveredItem)) return false;
        
        if(pHoveredItem == null)
        {
            ResetTooltip();
            return false;
        }
        
        ItemName.text = $"{Utility.UppercaseFirst(pHoveredItem.Item.itemName.ToLower())}";
        _itemType.text = $"{Utility.UppercaseFirst(pHoveredItem.Item.itemType.ToString().ToLower().Replace("_", " "))}";
        ItemDescription.text = $"Date Acquired:\n{(pHoveredItem.DateAcquired.Equals(DateTime.MinValue) ? "N/A" : pHoveredItem.DateAcquired.ToString("ddd, dd MMM yyyy HH:mm:ss"))}";

        return true;
    }
}
