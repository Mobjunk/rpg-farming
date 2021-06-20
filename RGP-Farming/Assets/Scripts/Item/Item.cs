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
    
    public Item(AbstractItemData item)
    {
        this.item = item;
        this.amount = 1;
    }

    public Item(AbstractItemData item, int amount)
    {
        this.item = item;
        this.amount = amount;
    }

    public void SetAmount(int amount)
    {
        this.amount = amount;
    }

    public override string ToString()
    {
        return $"{item}, {amount}";
    }
}