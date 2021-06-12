using UnityEngine;

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
}
