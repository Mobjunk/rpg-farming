using UnityEngine;

public class CharacterInventory : AbstractItemInventory
{
    public int GoldCoins;

    public void PurchaseItem(AbstractItemData pItem, int pItemPrice, int pAmount = 1)
    {
        UpdateCoins(-pItemPrice);
        AddItem(pItem, pAmount);
    }

    public void SellItem(AbstractItemData pItem, int pItemPrice, int pAmount = 1)
    {
        UpdateCoins(pItemPrice);
        RemoveItem(pItem, pAmount);
    }

    public bool HasEnoughGold(int pPrice)
    {
        return GoldCoins > pPrice;
    }

    public void UpdateCoins(int pAmount)
    {
        GoldCoins += pAmount;
        GoldIndicatorManager.Instance().UpdateCoins(GoldCoins);
    }

    public void AddItem(AbstractItemData pItem, int pItemAmount = 1, bool pShow = false)
    {
        if (pShow) ItemReceiverManager.Instance().Add(new GameItem(pItem, pItemAmount));
        base.AddItem(pItem, pItemAmount);
    }
}