using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestInvenotryUIManager : AbstractInventoryUIManger
{
    private InventoryMenuManager _inventoryMenuManager => InventoryMenuManager.Instance();
    private ItemBarManager _itemBarManager => ItemBarManager.Instance();

    [SerializeField] private ChestInventory _chestInventory;
    
    public void Start()
    {
        _chestInventory = GetComponent<ChestInventory>();
        Initialize(_chestInventory);
    }

    public override void Open()
    {
        base.Open();

        _inventoryMenuManager.SetAnchorPoint(AnchorsPresets.BOTTOM, new Vector2(0, 169.7f));
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
