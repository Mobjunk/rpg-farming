using System.Collections.Generic;
using UnityEngine;

public class CraftingInventoryUIManager : AbstractInventoryUIManger
{
    private InventoryMenuManager inventoryMenuManager => InventoryMenuManager.Instance();
    private ItemBarManager itemBarManager => ItemBarManager.Instance();
    
    [SerializeField] private CraftingInventory craftingInventory;
    public override void Awake()
    {
        craftingInventory = GetComponent<CraftingInventory>();
        base.Awake();
    }
    public override void Open()
    {
        if(InventoryContainers[0].maxSlots == 0) InventoryContainers[0].maxSlots = craftingInventory.maxInventorySize;
        base.Open();
        
        Initialize(craftingInventory);
        
        inventoryMenuManager.SetAnchorPoint(AnchorsPresets.BOTTOM, new Vector2(0, 158f));
        inventoryMenuManager.Unhide(true);
        
        itemBarManager.Hide();
    }

    public override void Close()
    {
        base.Close();

        inventoryMenuManager.Hide(true);
        inventoryMenuManager.SetAnchorPoint(AnchorsPresets.CENTER, new Vector2(0, 0));
        
        itemBarManager.Unhide();
        
        for (int parentIndex = 0; parentIndex < InventoryContainers.Length; parentIndex++)
        {
            ParentData parent = InventoryContainers[parentIndex];
            foreach(Transform childParent in parent.inventoryContainer)
                Destroy(childParent.gameObject);
        }
    }

    public void SwitchToInventory()
    {
        base.Close();
        
        Player.Instance().PlayerInventoryUIManager.Open();
        
        inventoryMenuManager.SetButtons(true);
        inventoryMenuManager.SetAnchorPoint(AnchorsPresets.CENTER, new Vector2(0, 0));
        
        for (int parentIndex = 0; parentIndex < InventoryContainers.Length; parentIndex++)
        {
            ParentData parent = InventoryContainers[parentIndex];
            foreach(Transform childParent in parent.inventoryContainer)
                Destroy(childParent.gameObject);
        }
    }

    public override void Interact()
    {
        if (isOpened) Close();
        else Open();
    }
}
