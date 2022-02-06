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
    public override void Open()
    {
        if(InventoryContainers[0].MaxSlots == 0) InventoryContainers[0].MaxSlots = _craftingInventory._maxInventorySize;
        base.Open();
        
        Initialize(_craftingInventory);
        
        _inventoryMenuManager.SetAnchorPoint(AnchorsPresets.BOTTOM, new Vector2(0, 189.5f));
        _inventoryMenuManager.Unhide(true);
        
        //itemBarManager.Hide();
    }

    public override void Close()
    {
        base.Close();

        _inventoryMenuManager.Hide(true);
        _inventoryMenuManager.SetAnchorPoint(AnchorsPresets.CENTER, new Vector2(0, 0));
        
        _itemBarManager.Unhide();
        
        for (int parentIndex = 0; parentIndex < InventoryContainers.Length; parentIndex++)
        {
            ParentData parent = InventoryContainers[parentIndex];
            foreach(Transform childParent in parent.InventoryContainer)
                Destroy(childParent.gameObject);
        }
    }

    public void SwitchToInventory()
    {
        base.Close();
        
        Player.Instance().PlayerInventoryUIManager.Open();
        
        _inventoryMenuManager.SetButtons(true);
        _inventoryMenuManager.SetAnchorPoint(AnchorsPresets.CENTER, new Vector2(0, 0));
        
        //itemBarManager.Unhide();
        
        for (int parentIndex = 0; parentIndex < InventoryContainers.Length; parentIndex++)
        {
            ParentData parent = InventoryContainers[parentIndex];
            foreach(Transform childParent in parent.InventoryContainer)
                Destroy(childParent.gameObject);
        }
    }

    public override void Interact()
    {
        if (IsOpened) Close();
        else Open();
    }
}
