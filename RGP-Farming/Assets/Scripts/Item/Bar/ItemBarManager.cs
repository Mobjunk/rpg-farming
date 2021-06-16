using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
            //TODO: Clean this mess
            if(Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Keypad1)) UpdateSlot(0);
            else if(Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Keypad2)) UpdateSlot(1);
            else if(Input.GetKeyDown(KeyCode.Alpha3) || Input.GetKeyDown(KeyCode.Keypad3)) UpdateSlot(2);
            else if(Input.GetKeyDown(KeyCode.Alpha4) || Input.GetKeyDown(KeyCode.Keypad4)) UpdateSlot(3);
            else if(Input.GetKeyDown(KeyCode.Alpha5) || Input.GetKeyDown(KeyCode.Keypad5)) UpdateSlot(4);
            else if(Input.GetKeyDown(KeyCode.Alpha6) || Input.GetKeyDown(KeyCode.Keypad6)) UpdateSlot(5);
            else if(Input.GetKeyDown(KeyCode.Alpha7) || Input.GetKeyDown(KeyCode.Keypad7)) UpdateSlot(6);
            else if(Input.GetKeyDown(KeyCode.Alpha8) || Input.GetKeyDown(KeyCode.Keypad8)) UpdateSlot(7);
            else if(Input.GetKeyDown(KeyCode.Alpha9) || Input.GetKeyDown(KeyCode.Keypad9)) UpdateSlot(8);
            else if(Input.GetKeyDown(KeyCode.Alpha0) || Input.GetKeyDown(KeyCode.Keypad0)) UpdateSlot(9);
            else if(Input.GetKeyDown(KeyCode.Minus) || Input.GetKeyDown(KeyCode.KeypadMinus)) UpdateSlot(10);
            else if(Input.GetKeyDown(KeyCode.Equals) || Input.GetKeyDown(KeyCode.KeypadEquals)) UpdateSlot(11);
            else UpdateSlot(Input.mouseScrollDelta.y);
        }
    }

    void UpdateSlot(float value)
    {
        //TODO: Add a check to see if the player has a UI element open (Like shops/inventory)
        if (Input.mouseScrollDelta.y == 0 || player.CharacterUIManager.CurrentUIOpened != null) return;

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
            //Sets the animator
            player.CharacterStateManager.SetAnimator("wieldingItem", true);
            //Sets item above head
            player.ItemAboveHead = characterInventory.items[nextSlot];
            //Sets the sprite above the head
            player.ItemAboveHeadRenderer.sprite = characterInventory.items[nextSlot].item.uiSprite;
            //Update the item containment
            if (itemDisplayer.Containment != characterInventory.items[nextSlot]) itemDisplayer.SetContainment(characterInventory.items[nextSlot]);
            //Checks if there is currently a item snapped
            if (!itemSnapper.isSnapped)
            {
                itemSnapper.SetSnappedItem(itemDisplayer);
                //itemDisplayer.Icon.enabled = true;
                itemDisplayer.gameObject.SetActive(true);
            }
        }
        else
        { //Reset everything
            player.CharacterStateManager.SetAnimator("wieldingItem", false);
            player.ItemAboveHead = null;
            player.ItemAboveHeadRenderer.sprite = null;
            //itemDisplayer.Icon.enabled = false;
            itemDisplayer.gameObject.SetActive(false);
            itemSnapper.ResetSnappedItem();
        }
    }

    public AbstractItemData GetItemSelected()
    {
        return player.CharacterInventory.items[selectedSlot].item;
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

    public bool IsWearingCorrectTools(ToolType[] tooltypes)
    {
        return tooltypes.Any(type => IsWearingCorrectTool(type));
    }
}
