using System.Collections.Generic;
using UnityEngine;

public class FurnaceUIManager : AbstractInventoryUIManger
{
    private InventoryMenuManager _inventoryMenuManager => InventoryMenuManager.Instance();
    private ItemBarManager _itemBarManager => ItemBarManager.Instance();
    
    //[SerializeField] private List<ItemContainerGrid> _slots = new List<ItemContainerGrid>();
    private ItemContainerGrid[] _slots;

    public override void Awake()
    {
        base.Awake();
        
        ContainmentContainer = GetComponent<FurnaceInventory>();
        ContainmentContainer.onInventoryChanged += OnInventoryChanged;
        
        _slots = GetComponentsInChildren<ItemContainerGrid>(true);
        
        for (int slot = 0; slot < _slots.Length; slot++)
        {
            ItemContainerGrid grid = _slots[slot];
            grid.AllowMoving = true;
            grid.Container = ContainmentContainer;
            grid.SetContainment(ContainmentContainer.Items[slot]);
            _containers[0].Add(grid);
        }
    }

    public override void Open()
    {
        base.Open();
        
        _inventoryMenuManager.SetAnchorPoint(AnchorsPresets.BOTTOM, new Vector2(0, 200f));
        _inventoryMenuManager.Unhide(true);
        
        _itemBarManager.Hide();
    }

    public override void Close()
    {
        base.Close();

        _inventoryMenuManager.Hide(true);
        _inventoryMenuManager.SetAnchorPoint(AnchorsPresets.CENTER, new Vector2(0, 0));
        
        _itemBarManager.Unhide();
    }

    public override void Interact()
    {
        if (IsOpened) Close();
        else Open();
    }
}