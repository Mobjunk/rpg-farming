using UnityEngine;

[RequireComponent(typeof(ShopUIManager), typeof(ShopInteraction))]
public class ShopInventory : AbstractItemInventory
{
    public ShopStock shopStock;

    public override void Awake()
    {
        base.Awake();

        if (shopStock == null) return;

        foreach (Item item in shopStock.items)
            AddItem(item.item, item.amount);
    }

    /// <summary>
    /// Handles purchasing a item from a player
    /// </summary>
    /// <param name="item"></param>
    /// <param name="amount"></param>
    public void PurchaseItem(AbstractItemData item, int amount = 1)
    {
        AddItem(item, amount);
    }

    /// <summary>
    /// Handles selling a item to a player
    /// </summary>
    /// <param name="item"></param>
    /// <param name="amount"></param>
    public void SellItem(AbstractItemData item, int amount = 1)
    {
        RemoveItem(item, amount, true);
    }

    /// <summary>
    /// Checks if the shop can purchase a item
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    public bool CanPurchase(AbstractItemData item)
    {
        if (!shopStock.isGeneralStore)
            if (!HasItem(item, 0)) return false;
        if (!ItemFitsInventory()) return false;
        return true;
    }

    /// <summary>
    /// Checks if the store has stock in a specific slot
    /// </summary>
    /// <param name="slot"></param>
    /// <returns></returns>
    public bool HasStock(int slot)
    {
        return Items[slot].amount > 0;
    }

    /// <summary>
    /// Calculates the buy price of a item to a store
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    public int GetSellPrice(AbstractItemData item)
    {
        return Mathf.FloorToInt(item.itemPrice * shopStock.sellRatio);
    }

    /// <summary>
    /// Calculates the sell price of a item to a store
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    public int GetBuyPrice(AbstractItemData item)
    {
        return Mathf.FloorToInt(item.itemPrice * shopStock.buyRatio);
    }
}
