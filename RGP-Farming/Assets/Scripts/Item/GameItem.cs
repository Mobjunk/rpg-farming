using UnityEngine;

[System.Serializable]
public class GameItem
{
    public AbstractItemData Item;
    public int Amount;
    public int Durability = -1;
    public int MaxDurability = -1;

    public GameItem()
    {
        Item = null;
        Amount = 0;
        Durability = -1;
        MaxDurability = -1;
    }
    
    public GameItem(AbstractItemData pItem)
    {
        this.Item = pItem;
        this.Amount = 1;
        if (pItem.durability != -1)
        {
            Durability = pItem.durability;
            MaxDurability = pItem.durability;
        }
    }

    public GameItem(AbstractItemData pItem, int pAmount)
    {
        this.Item = pItem;
        this.Amount = pAmount;
        if (pItem != null && pItem.durability != -1)
        {
            Durability = pItem.durability;
            MaxDurability = pItem.durability;
        }
    }

    public void SetAmount(int pAmount)
    {
        this.Amount = pAmount;
    }

    public void SetDurability(int pDurability)
    {
        this.Durability = pDurability;
    }

    public override string ToString()
    {
        return $"{Item}, {Amount}, {Durability}, {MaxDurability}";
    }
}