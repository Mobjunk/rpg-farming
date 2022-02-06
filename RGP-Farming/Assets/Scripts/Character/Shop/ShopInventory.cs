using UnityEngine;

[RequireComponent(typeof(ShopUIManager), typeof(ShopInteraction))]
public class ShopInventory : AbstractItemInventory
{
    public ShopStock ShopStock;

    public override void Awake()
    {
        base.Awake();

        if (ShopStock == null) return;

        foreach (GameItem item in ShopStock.items)
            AddItem(item.Item, item.Amount);
    }

    /// <summary>
    /// Handles purchasing a item from a player
    /// </summary>
    /// <param name="pItem"></param>
    /// <param name="pAmount"></param>
    public void PurchaseItem(AbstractItemData pItem, int pAmount = 1)
    {
        AddItem(pItem, pAmount);
    }

    /// <summary>
    /// Handles selling a item to a player
    /// </summary>
    /// <param name="pItem"></param>
    /// <param name="pImount"></param>
    public void SellItem(AbstractItemData pItem, int pImount = 1)
    {
        RemoveItem(pItem, pImount, true);
    }

    /// <summary>
    /// Checks if the shop can purchase a item
    /// </summary>
    /// <param name="pItem"></param>
    /// <returns></returns>
    public bool CanPurchase(AbstractItemData pItem)
    {
        if (!ShopStock.isGeneralStore)
            if (!HasItem(pItem, 0)) return false;
        if (!ItemFitsInventory()) return false;
        return true;
    }

    /// <summary>
    /// Checks if the store has stock in a specific slot
    /// </summary>
    /// <param name="pSlot"></param>
    /// <returns></returns>
    public bool HasStock(int pSlot)
    {
        return Items[pSlot].Amount > 0;
    }

    /// <summary>
    /// Calculates the buy price of a item to a store
    /// </summary>
    /// <param name="pItem"></param>
    /// <returns></returns>
    public int GetSellPrice(AbstractItemData pItem)
    {
        return Mathf.FloorToInt(pItem.itemPrice * ShopStock.sellRatio);
    }

    /// <summary>
    /// Calculates the sell price of a item to a store
    /// </summary>
    /// <param name="pItem"></param>
    /// <returns></returns>
    public int GetBuyPrice(AbstractItemData pItem)
    {
        return Mathf.FloorToInt(pItem.itemPrice * ShopStock.buyRatio);
    }
}
