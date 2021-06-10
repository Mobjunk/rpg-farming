using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBarManager : MenuManager<ItemBarManager>
{
    private ItemSnapperManager itemSnapper => ItemSnapperManager.Instance();
    private Player player => Player.Instance();
    
    [HideInInspector] public int selectedSlot = 0;
    [SerializeField] private ItemContainerGrid itemDisplayer;
    
    private PlayerInvenotryUIManager inventoryUIManager;

    public void Start()
    {
        inventoryUIManager = player.GetComponent<PlayerInvenotryUIManager>();
    }

    public void Update()
    {
        if (player.ControllerConnected)
        {
            
        }
        else
        {
            //TODO: Add a delay
            UpdateSlot(Input.mouseScrollDelta.y);
        }
    }

    void UpdateSlot(float value)
    {
        if (Input.mouseScrollDelta.y == 0) return;

        CharacterInventory characterInventory = player.CharacterInventory;
        int nextSlot = characterInventory.GetNextOccupiedSlot(selectedSlot, value > 0);
        if (nextSlot == -1) return;

        UpdateSlot(nextSlot);
    }

    public void UpdateSlot(int nextSlot)
    {
        CharacterInventory characterInventory = player.CharacterInventory;

        if (characterInventory.items[nextSlot].item == null) return;
        
        inventoryUIManager.containers[0][selectedSlot].SetHighlighted(false);
        selectedSlot = nextSlot;
        inventoryUIManager.containers[0][selectedSlot].SetHighlighted(true);

        //Checks if the item is a placeable item
        if (characterInventory.items[nextSlot].item.GetType() == typeof(AbstractPlaceableItem) || characterInventory.items[nextSlot].item.GetType() == typeof(AbstractPlantData))
        {
            player.ItemAboveHead = characterInventory.items[nextSlot];
            //Sets the sprite above the head
            player.ItemAboveHeadRenderer.sprite = characterInventory.items[nextSlot].item.uiSprite;
            //Update the item containment
            if (itemDisplayer.Containment != characterInventory.items[nextSlot]) itemDisplayer.SetContainment(characterInventory.items[nextSlot]);
            //Checks if there is currently a item snapped
            if (!itemSnapper.isSnapped)
            {
                itemSnapper.SetSnappedItem(itemDisplayer);
                itemDisplayer.Icon.enabled = true;
            }
        }
        else
        { //Reset everything
            player.ItemAboveHead = null;
            player.ItemAboveHeadRenderer.sprite = null;
            itemDisplayer.Icon.enabled = false;
            itemSnapper.ResetSnappedItem();
        }
    }

    public bool IsWearingCorrectTool(ToolType tooltype)
    {
        if (player.CharacterInventory.items[selectedSlot].item == null) return false;
        
        if (player.CharacterInventory.items[selectedSlot].item.GetType() == typeof(AbstractToolItem))
        {
            AbstractToolItem tool = (AbstractToolItem) player.CharacterInventory.items[selectedSlot].item;
            if (tool.tooltype.Equals(tooltype)) return true;
        }
        return false;
    }
}
