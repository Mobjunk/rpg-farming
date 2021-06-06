[System.Serializable]
public class Item
{
    public AbstractItemData item;
    public int amount;

    public Item()
    {
        item = null;
        amount = 0;
    }
    
    public Item(AbstractItemData itemData)
    {
        this.item = itemData;
        this.amount = 1;
    }

    public Item(AbstractItemData itemData, int amount)
    {
        this.item = itemData;
        this.amount = amount;
    }
}