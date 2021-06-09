using UnityEngine;

public abstract class AbstractItemContainer : UIContainerbase<Item>
{
    
    public override void SetContainment(Item containment)
    {
        base.SetContainment(containment);
        UpdateItemContainer();
    }

    protected virtual void UpdateItemContainer()
    {
        if (Containment == null || Containment.item == null)
        {
            base.ClearContainment();
            return;
        }

        Icon.sprite = Containment.item.uiSprite;
        Icon.enabled = true;

        if(ItemBarManager.Instance().selectedSlot == slotIndex && !AllowMoving) SetHighlighted(true);

        Amount.text = $"{(Containment.amount > 1 ? Containment.amount.ToString() : "")}";
        Amount.enabled = Containment.amount > 1;
    }
}