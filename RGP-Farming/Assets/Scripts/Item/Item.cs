using UnityEngine;

[System.Serializable]
public class Item
{
    public AbstractItemData item;
    public int amount;
    public int durability = -1;
    public int maxDurability = -1;

    public Item()
    {
        item = null;
        amount = 0;
        durability = -1;
        maxDurability = -1;
    }
    
    public Item(AbstractItemData item)
    {
        this.item = item;
        this.amount = 1;
        if (item.durability != -1)
        {
            durability = item.durability;
            maxDurability = item.durability;
        }
    }

    public Item(AbstractItemData item, int amount)
    {
        this.item = item;
        this.amount = amount;
        if (item != null && item.durability != -1)
        {
            durability = item.durability;
            maxDurability = item.durability;
        }
    }

    public void SetAmount(int amount)
    {
        this.amount = amount;
    }

    public void SetDurability(int durability)
    {
        this.durability = durability;
    }

    public override string ToString()
    {
        return $"{item}, {amount}, {durability}, {maxDurability}";
    }
}