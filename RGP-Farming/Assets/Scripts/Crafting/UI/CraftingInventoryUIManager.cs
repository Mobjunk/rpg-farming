using System.Collections.Generic;
using UnityEngine;

public class CraftingInventoryUIManager : AbstractInventoryUIManger
{
    private InventoryMenuManager _inventoryMenuManager => InventoryMenuManager.Instance();
    private ItemBarManager _itemBarManager => ItemBarManager.Instance();
    
    [SerializeField] private CraftingInventory _craftingInventory;

    public override void Awake()
    {
        _craftingInventory = GetComponent<CraftingInventory>();
        base.Awake();
    }

    public void SwitchToTabInit(int pIndex)
    {
        if (base.SwitchToTab(pIndex))
        {
            if (InventoryContainers[0].MaxSlots == 0)
                InventoryContainers[0].MaxSlots = _craftingInventory._maxInventorySize;

            Initialize(_craftingInventory);
        }
    }

    public void SwitchToInventory()
    {
        if (base.SwitchToTab(0)) ClearCraftingChildren();
    }

    public void ClearCraftingChildren()
    {
        for (int parentIndex = 0; parentIndex < InventoryContainers.Length; parentIndex++)
        {
            ParentData parent = InventoryContainers[parentIndex];
            foreach (Transform childParent in parent.InventoryContainer)
                Destroy(childParent.gameObject);
        }
    }
}
