using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ShopContainerInteraction : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    private Player _player = Player.Instance();
    
    private ShopContainerGrid _shopContainerGrid;
    private Image _image;
    [SerializeField] private Color _defaultColor;
    [SerializeField] private Color _hoverColor;

    private void Awake()
    {
        _shopContainerGrid = GetComponent<ShopContainerGrid>();
        _image = GetComponent<Image>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _image.color = _hoverColor;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _image.color = _defaultColor;
    }


    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            AbstractItemData item = _shopContainerGrid.Containment.item;
            //Checks if the container you clicked is a shop
            if (_shopContainerGrid.Container.GetType() == typeof(ShopInventory))
            {
                ShopInventory shopInventory = ((ShopInventory) _shopContainerGrid.Container);
                //Checks if the player has room for the item
                if (_player.CharacterInventory.ItemFitsInventory())
                {
                    if (shopInventory.HasStock(_shopContainerGrid.SlotIndex))
                    {
                        if (_player.CharacterInventory.HasEnoughGold(item.itemPrice))
                        {
                            _player.CharacterInventory.PurchaseItem(item, shopInventory.GetBuyPrice(item));
                            shopInventory.SellItem(item);
                        } else Debug.LogError("Has not enough gold for this item...");
                    } else Debug.LogError("Shop has no stock...");
                } else Debug.LogError("Has no room for this item...");
            }
            //Checks if the container that was clicked was a character inventory
            else if (_shopContainerGrid.Container.GetType() == typeof(CharacterInventory))
            {
                //Grabs the shop inventory from the current ui being opened
                ShopInventory shopInventory = _player.CharacterUIManager.CurrentUIOpened.GetComponent<ShopInventory>();
                if (shopInventory != null)
                {
                    //Checks if the shop can purchase this item
                    if (shopInventory.CanPurchase(item))
                    {
                        ((CharacterInventory) _shopContainerGrid.Container).SellItem(item, shopInventory.GetSellPrice(item));
                        shopInventory.PurchaseItem(item);
                    } else Debug.LogError("Cannot purchase does not belong in current stock...");
                }
            }
        }
    }
}
