using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ShopContainerInteraction : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    private Player player = Player.Instance();
    
    private ShopContainerGrid shopContainerGrid;
    private Image image;
    [SerializeField] private Color defaultColor;
    [SerializeField] private Color hoverColor;

    private void Awake()
    {
        shopContainerGrid = GetComponent<ShopContainerGrid>();
        image = GetComponent<Image>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        image.color = hoverColor;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        image.color = defaultColor;
    }


    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            AbstractItemData item = shopContainerGrid.Containment.item;
            //Checks if the container you clicked is a shop
            if (shopContainerGrid.Container.GetType() == typeof(ShopInventory))
            {
                ShopInventory shopInventory = ((ShopInventory) shopContainerGrid.Container);
                //Checks if the player has room for the item
                if (player.CharacterInventory.ItemFitsInventory())
                {
                    if (shopInventory.HasStock(shopContainerGrid.SlotIndex))
                    {
                        if (player.CharacterInventory.HasEnoughGold(item.itemPrice))
                        {
                            player.CharacterInventory.PurchaseItem(item, shopInventory.GetBuyPrice(item));
                            shopInventory.SellItem(item);
                        } else Debug.LogError("Has not enough gold for this item...");
                    } else Debug.LogError("Shop has no stock...");
                } else Debug.LogError("Has no room for this item...");
            }
            //Checks if the container that was clicked was a character inventory
            else if (shopContainerGrid.Container.GetType() == typeof(CharacterInventory))
            {
                //Grabs the shop inventory from the current ui being opened
                ShopInventory shopInventory = player.CharacterUIManager.CurrentUIOpened.GetComponent<ShopInventory>();
                if (shopInventory != null)
                {
                    //Checks if the shop can purchase this item
                    if (shopInventory.CanPurchase(item))
                    {
                        ((CharacterInventory) shopContainerGrid.Container).SellItem(item, shopInventory.GetSellPrice(item));
                        shopInventory.PurchaseItem(item);
                    } else Debug.LogError("Cannot purchase does not belong in current stock...");
                }
            }
        }
    }
}
