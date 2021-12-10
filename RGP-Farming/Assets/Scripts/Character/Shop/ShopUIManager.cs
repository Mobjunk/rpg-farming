using System;
using System.Collections.Generic;
using UnityEngine;

public class ShopUIManager : AbstractInventoryUIManger
{
    private ShopUI _shopUI => ShopUI.Instance();
    private Player _player => Player.Instance();
    private ShopInventory _shopInventory;
    private PlayerInvenotryUIManager _inventoryUIManager;
    
    public override void Awake()
    {
        base.Awake();
        _shopInventory = GetComponent<ShopInventory>();
        if (InventoryUI == null && InventoryContainers[0].inventoryContainer == null && UiTabs[0] == null && UiTabs[1] == null)
        {
            InventoryUI = _shopUI.Contents;
            InventoryContainers[0].inventoryContainer = _shopUI.ItemContainer.transform;
            UiTabs = _shopUI.UiTabs;
        }
    }

    public void Start()
    {
        _inventoryUIManager = _player.GetComponent<PlayerInvenotryUIManager>();
        InventoryContainers[0].maxSlots = _shopInventory.MaxInventorySize;
    }

    public override void Open()
    {
        base.Open();
        
        if (_shopInventory != null)
        {
            Initialize(_shopInventory);

            UpdatePrices();
            
            ItemBarManager.Instance().Hide();
        }
    }

    public override void Close()
    {
        base.Close();
        
        ItemBarManager.Instance().Unhide();
        
        //Handles setting up the container
        for (int parentIndex = 0; parentIndex < InventoryContainers.Length; parentIndex++)
        {
            ParentData parent = InventoryContainers[parentIndex];
            foreach(Transform childParent in parent.inventoryContainer)
                Destroy(childParent.gameObject);
        }
    }

    /// <summary>
    /// Handles updating the UI with the correct pricing
    /// </summary>
    private void UpdatePrices()
    {
        //Handles updating the shops sell price
        for (int slot = 0; slot < containers[0].Count; slot++)
        {
            if (ContainmentContainer.Items[slot].item == null) continue;
            ((ShopContainerGrid)containers[0][slot]).UpdateItemPrice(_shopInventory.GetBuyPrice(ContainmentContainer.Items[slot].item));
        }
        
        //Handles updating the player his inventory sell price
        for (int slot = 0; slot < _player.CharacterInventory.Items.Length; slot++)
        {
            if (_player.CharacterInventory.Items[slot].item == null) continue;
            ((ShopContainerGrid)_inventoryUIManager.containers[2][slot]).UpdateItemPrice(_shopInventory.GetSellPrice(_player.CharacterInventory.Items[slot].item));
        }
    }

    public override void OnInventoryChanged(List<int> slots)
    {
        base.OnInventoryChanged(slots);
        UpdatePrices();
    }
}