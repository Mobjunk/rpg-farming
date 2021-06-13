using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestInvenotryUIManager : AbstractInventoryUIManger
{
    
    [SerializeField] private ChestInventory chestInventory;
    
    public void Start()
    {
        chestInventory = GetComponent<ChestInventory>();
        Initialize(chestInventory);
    }

    public override void Open()
    {
        base.Open();
        
        InventoryMenuManager.Instance().SetAnchorPoint(AnchorsPresets.BOTTOM, new Vector2(0, 56.6f));
        InventoryMenuManager.Instance().Unhide(true);
        
        ItemBarManager.Instance().Hide();
    }

    public override void Close()
    {
        base.Close();

        InventoryMenuManager.Instance().Hide(true);
        InventoryMenuManager.Instance().SetAnchorPoint(AnchorsPresets.CENTER, new Vector2(0, 0));
        
        ItemBarManager.Instance().Unhide();
    }

    public override void Interact()
    {
        if (isOpened) Close();
        else Open();
    }
}
