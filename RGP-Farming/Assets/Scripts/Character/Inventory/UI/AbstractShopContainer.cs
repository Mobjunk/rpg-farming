using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbstractShopContainer : UIShopContainment<Item>
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
            gameObject.SetActive(false);
            base.ClearContainment();
            return;
        }

        gameObject.SetActive(true);
        MainUISprite.enabled = true;
        Icon.sprite = Containment.item.uiSprite;
        Icon.enabled = true;

        ItemName.text = $"{Containment.item.itemName}";
        ItemNameShadow.text = $"{Containment.item.itemName}";
        ItemPrice.text = $"{Containment.item.itemPrice}";
        
        Amount.text = $"{(Containment.amount > 1 ? Containment.amount.ToString() : "")}";
        Amount.enabled = Containment.amount > 1;

        GoldCoin.enabled = true;

        //TODO: Update the image stuff
        /*Icon.sprite = Containment.item.uiSprite;
        Icon.enabled = true;

        if(ItemBarManager.Instance().selectedSlot == slotIndex && !AllowMoving) SetHighlighted(true);

        Amount.text = $"{(Containment.amount > 1 ? Containment.amount.ToString() : "")}";
        Amount.enabled = Containment.amount > 1;*/
    }
}