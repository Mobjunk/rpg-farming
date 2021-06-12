using UnityEngine;

public class ShopUIManager : AbstractInventoryUIManger
{
    private ShopInventory shopInventory;
    
    public override void Awake()
    {
        base.Awake();
        shopInventory = GetComponent<ShopInventory>();
    }

    public override void Open()
    {
        base.Open();
        
        if (shopInventory != null)
        {
            InventoryContainers[0].maxSlots = shopInventory.maxInventorySize;
            Initialize(shopInventory);
        }
    }

    public override void Close()
    {
        base.Close();
        
        //Handles setting up the container
        for (int parentIndex = 0; parentIndex < InventoryContainers.Length; parentIndex++)
        {
            ParentData parent = InventoryContainers[parentIndex];
            foreach(Transform childParent in parent.inventoryContainer)
                Destroy(childParent.gameObject);
        }
    }
}