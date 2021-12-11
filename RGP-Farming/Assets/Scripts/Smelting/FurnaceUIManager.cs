using System.Collections.Generic;
using UnityEngine;

public class FurnaceUIManager : AbstractInventoryUIManger
{
    private InventoryMenuManager inventoryMenuManager => InventoryMenuManager.Instance();
    private ItemBarManager itemBarManager => ItemBarManager.Instance();
    
    [SerializeField] private List<ItemContainerGrid> slots = new List<ItemContainerGrid>();

    public override void Awake()
    {
        base.Awake();
        
        ContainmentContainer = GetComponent<FurnaceInventory>();
        ContainmentContainer.onInventoryChanged += OnInventoryChanged;
        
        for (int slot = 0; slot < slots.Count; slot++)
        {
            ItemContainerGrid grid = slots[slot];
            grid.AllowMoving = true;
            grid.Container = ContainmentContainer;
            grid.SetContainment(ContainmentContainer.items[slot]);
            
            containers[0].Add(grid);
        }
    }

    public override void Open()
    {
        base.Open();
        
        inventoryMenuManager.SetAnchorPoint(AnchorsPresets.BOTTOM, new Vector2(0, 200f));
        inventoryMenuManager.Unhide(true);
        
        itemBarManager.Hide();
    }

    public override void Close()
    {
        base.Close();

        inventoryMenuManager.Hide(true);
        inventoryMenuManager.SetAnchorPoint(AnchorsPresets.CENTER, new Vector2(0, 0));
        
        itemBarManager.Unhide();
    }

    public override void Interact()
    {
        if (isOpened) Close();
        else Open();
    }
}