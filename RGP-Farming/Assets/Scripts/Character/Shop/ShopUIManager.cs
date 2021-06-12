﻿using System;
using System.Collections.Generic;
using UnityEngine;

public class ShopUIManager : AbstractInventoryUIManger
{
    private Player player => Player.Instance();
    private ShopInventory shopInventory;
    private PlayerInvenotryUIManager inventoryUIManager;
    
    public override void Awake()
    {
        base.Awake();
        shopInventory = GetComponent<ShopInventory>();
    }

    public void Start()
    {
        inventoryUIManager = player.GetComponent<PlayerInvenotryUIManager>();
        InventoryContainers[0].maxSlots = shopInventory.maxInventorySize;
    }

    public override void Open()
    {
        base.Open();
        
        if (shopInventory != null)
        {
            Initialize(shopInventory);

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
            if (ContainmentContainer.items[slot].item == null) continue;
            ((ShopContainerGrid)containers[0][slot]).UpdateItemPrice(shopInventory.GetBuyPrice(ContainmentContainer.items[slot].item));
        }
        
        //Handles updating the player his inventory sell price
        for (int slot = 0; slot < player.CharacterInventory.items.Length; slot++)
        {
            if (player.CharacterInventory.items[slot].item == null) continue;
            ((ShopContainerGrid)inventoryUIManager.containers[2][slot]).UpdateItemPrice(shopInventory.GetSellPrice(player.CharacterInventory.items[slot].item));
        }
    }

    public override void OnInventoryChanged(List<int> slots)
    {
        base.OnInventoryChanged(slots);
        UpdatePrices();
    }
}