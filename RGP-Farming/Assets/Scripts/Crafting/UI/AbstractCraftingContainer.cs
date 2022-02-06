using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class AbstractCraftingContainer : AbstractItemContainer<GameItem>
{
    
    public override void SetContainment(GameItem pContainment)
    {
        base.SetContainment(pContainment);
        UpdateItemContainer();
    }

    public override void UpdateItemContainer()
    {
        if (Containment == null || Containment.Item == null)
        {
            base.ClearContainment();
            return;
        }

        Icon.sprite = Containment.Item.uiSprite;
        Icon.enabled = true;

        if (!Player.Instance().CharacterInventory.HasItems(Containment.Item.craftingRecipe.requiredItems))
        {
            Color currentColor = Icon.color;
            Icon.color = new Color(currentColor.r, currentColor.g, currentColor.b, 0.5f);
        }
    }

    public override void OnPointerDown(PointerEventData pEventData)
    {
        if (pEventData.button == PointerEventData.InputButton.Left || pEventData.button == PointerEventData.InputButton.Middle)
        {
            Debug.Log("Crafting debug...");
            if (Containment.Item.craftingRecipe == null) return;
            
            Player player = Player.Instance();

            if (player.CharacterInventory.HasItems(Containment.Item.craftingRecipe.requiredItems))
            {
                foreach(GameItem item in Containment.Item.craftingRecipe.requiredItems)
                    player.CharacterInventory.RemoveItem(item.Item, item.Amount);
                player.CharacterInventory.AddItem(Containment.Item);

                UpdateItemContainer();

            } else Debug.LogError("Player is missing some of the items...");
        }
    }

    public override void OnPointerEnter(PointerEventData pEventData)
    {
        base.OnPointerEnter(pEventData);
        CraftingTooltipManager.Instance().SetTooltip(Containment.Item);
    }

    public override void OnPointerExit(PointerEventData pEventData)
    {
        base.OnPointerExit(pEventData);
        CraftingTooltipManager.Instance().SetTooltip(null);
    }
}