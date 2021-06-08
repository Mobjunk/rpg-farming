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
        
        InventoryMenuManager.Instance().SetAnchorPoint(AnchorsPresets.BOTTOM, new Vector2(0, 33.5f));
        InventoryMenuManager.Instance().Unhide();
        
        ItemBarManager.Instance().Hide();
        
        //TODO: SET INVENTORY MENU TO BOTTOM AND OFFSET
        //TODO: HIDE INVENTORY BAR
        //TODO: UNHIDE INVENTORY MENU
    }

    public override void Close()
    {
        base.Close();
        
        InventoryMenuManager.Instance().Hide();
        InventoryMenuManager.Instance().SetAnchorPoint(AnchorsPresets.CENTER, new Vector2(0, 0));
        
        ItemBarManager.Instance().Unhide();
        
        //TODO: RESET THE INVENTORY MENU TO CENTER
        //TODO: UNHIDE INVENTORY BAR
        //TODO: HIDE INVENTORY MENU
    }

    public void Interact()
    {
        if (isOpened)
        {
            Player.Instance().InteractedObject = null;
            Close();
        }
        else
        {
            Player.Instance().InteractedObject = gameObject;
            Open();
        }
    }
}
