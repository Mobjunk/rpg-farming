public class CharacterInventory : AbstractItemInventory
{
    public int goldCoins;

    public void PurchaseItem(AbstractItemData item, int amount = 1)
    {
        
    }

    public void SellItem(AbstractItemData item, int amount = 1)
    {
        
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
}