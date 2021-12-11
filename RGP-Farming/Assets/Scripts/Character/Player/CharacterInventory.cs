using UnityEngine;

public class CharacterInventory : AbstractItemInventory
{
    public int goldCoins;

    public void PurchaseItem(AbstractItemData item, int itemPrice, int amount = 1)
    {
        UpdateCoins(-itemPrice);
        AddItem(item, amount);
    }

    public void SellItem(AbstractItemData item, int itemPrice, int amount = 1)
    {
        UpdateCoins(itemPrice);
        RemoveItem(item, amount);
    }

    public bool HasEnoughGold(int price)
    {
        return goldCoins > price;
    }

    public void UpdateCoins(int amount)
    {
        goldCoins += amount;
        GoldIndicatorManager.Instance().UpdateCoins(goldCoins);
    }

    public void AddItem(AbstractItemData item, int itemAmount = 1, bool show = false)
    {
        if (show) ItemReceiverManager.Instance().Add(new Item(item, itemAmount));
        base.AddItem(item, itemAmount);
    }
}