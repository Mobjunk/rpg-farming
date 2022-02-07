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
        Item = pItem;
        Amount = 1;
        if (pItem.durability != -1)
        {
            Durability = pItem.durability;
            MaxDurability = pItem.durability;
        }
    }

    public GameItem(AbstractItemData pItem, int pAmount)
    {
        Item = pItem;
        Amount = pAmount;
        if (pItem != null && pItem.durability != -1)
        {
            Durability = pItem.durability;
            MaxDurability = pItem.durability;
        }
    }

    public void SetAmount(int pAmount)
    {
        Amount = pAmount;
    }

    public void SetDurability(int pDurability)
    {
        Durability = pDurability;
    }

    public override string ToString()
    {
        return $"{Item}, {Amount}, {Durability}, {MaxDurability}";
    }
}