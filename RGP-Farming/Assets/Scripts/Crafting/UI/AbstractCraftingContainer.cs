using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class AbstractCraftingContainer : AbstractItemContainer<Item>
{
    
    public override void SetContainment(Item containment)
    {
        base.SetContainment(containment);
        UpdateItemContainer();
    }

    public override void UpdateItemContainer()
    {
        if (Containment == null || Containment.item == null)
        {
            base.ClearContainment();
            return;
        }

        Icon.sprite = Containment.item.uiSprite;
        Icon.enabled = true;

        if (!Player.Instance().CharacterInventory.HasItems(Containment.item.craftingRecipe.requiredItems))
        {
            Color currentColor = Icon.color;
            Icon.color = new Color(currentColor.r, currentColor.g, currentColor.b, 0.5f);
        }
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left || eventData.button == PointerEventData.InputButton.Middle)
        {
            Debug.Log("Crafting debug...");
            if (Containment.item.craftingRecipe == null) return;
            
            Player player = Player.Instance();

            if (player.CharacterInventory.HasItems(Containment.item.craftingRecipe.requiredItems))
            {
                foreach(Item item in Containment.item.craftingRecipe.requiredItems)
                    player.CharacterInventory.RemoveItem(item.item, item.amount);
                player.CharacterInventory.AddItem(Containment.item);

                UpdateItemContainer();

            } else Debug.LogError("Player is missing some of the items...");
        }
    }

    public override void OnPointerEnter(PointerEventData eventData)
    {
        base.OnPointerEnter(eventData);
        CraftingTooltipManager.Instance().SetTooltip(Containment.item);
    }

    public override void OnPointerExit(PointerEventData eventData)
    {
        base.OnPointerExit(eventData);
        CraftingTooltipManager.Instance().SetTooltip(null);
    }
}