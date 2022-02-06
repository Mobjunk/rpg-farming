using UnityEngine;
using UnityEngine.EventSystems;

public class ItemContainerGrid : AbstractItemContainer<GameItem>
{
    public override void UpdateItemContainer()
    {
        if (Containment == null || Containment.Item == null)
        {
            base.ClearContainment();
            return;
        }

        Icon.sprite = Containment.Item.uiSprite;
        Icon.enabled = true;

        if(ItemBarManager.Instance().SelectedSlot == SlotIndex && !AllowMoving) SetHighlighted(true);

        Amount.text = $"{(Containment.Amount > 1 ? Containment.Amount.ToString() : "")}";
        Amount.enabled = Containment.Amount > 1;

        if (Containment.MaxDurability != -1 && Containment.Durability < Containment.MaxDurability)
        {
            Slider.value = (Containment.Durability / (Containment.MaxDurability / 100f)) * (1 / 100f);
            Slider.gameObject.SetActive(true);
        }
    }

    public override void OnPointerEnter(PointerEventData pEventData)
    {
        base.OnPointerEnter(pEventData);
        if (Containment == null) return;
        ItemTooltipManager.Instance().SetTooltip(Containment.Item);        
    }

    public override void OnPointerExit(PointerEventData pEventData)
    {
        base.OnPointerExit(pEventData);
        ItemTooltipManager.Instance().SetTooltip(null);
    }
}