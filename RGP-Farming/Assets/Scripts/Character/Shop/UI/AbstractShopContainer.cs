using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbstractShopContainer : UIShopContainment<GameItem>
{
    
    public override void SetContainment(GameItem pContainment)
    {
        base.SetContainment(pContainment);
        UpdateItemContainer();
    }

    protected virtual void UpdateItemContainer()
    {
        if (Containment == null || Containment.Item == null)
        {
            gameObject.SetActive(false);
            base.ClearContainment();
            return;
        }

        gameObject.SetActive(true);
        MainUISprite.enabled = true;
        Icon.sprite = Containment.Item.uiSprite;
        Icon.enabled = true;
        
        ItemName.text = $"{Containment.Item.itemName}";
        ItemNameShadow.text = $"{Containment.Item.itemName}";
        
        ItemPrice.text = $"0";
        
        Amount.text = $"{Containment.Amount.ToString()}";
        Amount.enabled = true;

        GoldCoin.enabled = true;
    }

    public virtual void UpdateItemPrice(int pPrice)
    {
        ItemPrice.text = $"{pPrice}";
    }
}