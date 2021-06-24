using UnityEngine;
using UnityEngine.EventSystems;

public class ItemContainerGrid : AbstractItemContainer<Item>
{
    public override void UpdateItemContainer()
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

        if (Containment.maxDurability != -1 && Containment.durability < Containment.maxDurability)
        {
            Slider.value = (Containment.durability / (Containment.maxDurability / 100f)) * (1 / 100f);
            Slider.gameObject.SetActive(true);
        }
    }

    public override void OnPointerEnter(PointerEventData eventData)
    {
        base.OnPointerEnter(eventData);
        if (Containment == null) return;
        ItemTooltipManager.Instance().SetTooltip(Containment.item);        
    }

    public override void OnPointerExit(PointerEventData eventData)
    {
        base.OnPointerExit(eventData);
        ItemTooltipManager.Instance().SetTooltip(null);
    }
}