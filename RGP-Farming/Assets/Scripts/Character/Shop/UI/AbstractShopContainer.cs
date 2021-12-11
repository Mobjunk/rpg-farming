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
        
        ItemPrice.text = $"0";
        
        Amount.text = $"{Containment.amount.ToString()}";
        Amount.enabled = true;

        GoldCoin.enabled = true;
    }

    public virtual void UpdateItemPrice(int price)
    {
        ItemPrice.text = $"{price}";
    }
}