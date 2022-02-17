using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ItemBarManager : MenuManager<ItemBarManager>
{
    private ItemSnapperManager _itemSnapper => ItemSnapperManager.Instance();
    private Player _player => Player.Instance();
    
    [HideInInspector] public int SelectedSlot = 0;
    [SerializeField] private ItemContainerGrid _itemDisplayer;

    public ItemContainerGrid ItemDisplayer => _itemDisplayer;
    
    private PlayerInvenotryUIManager _inventoryUIManager;

    public void Start()
    {
        _inventoryUIManager = _player.GetComponent<PlayerInvenotryUIManager>();
    }

    public void Update()
    {
        if (_player.ControllerConnected)
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

    void UpdateSlot(float pValue)
    {
        //TODO: Add a check to see if the player has a UI element open (Like shops/inventory)
        if (Input.mouseScrollDelta.y == 0 || _player.CharacterUIManager.CurrentUIOpened != null) return;

        CharacterInventory characterInventory = _player.CharacterInventory;
        int nextSlot = characterInventory.GetNextOccupiedSlot(SelectedSlot, pValue > 0);
        if (nextSlot == -1) return;

        UpdateSlot(nextSlot);
    }

    public void UpdateSlot(int pNextSlot)
    {
        CharacterInventory characterInventory = _player.CharacterInventory;

        if (characterInventory.Items[pNextSlot].Item == null) return;
        
        _inventoryUIManager._containers[0][SelectedSlot].SetHighlighted(false);
        SelectedSlot = pNextSlot;
        _inventoryUIManager._containers[0][SelectedSlot].SetHighlighted(true);

        //Checks if the item is a placeable item
        if (characterInventory.Items[pNextSlot].Item.GetType() == typeof(AbstractPlaceableItem) || characterInventory.Items[pNextSlot].Item.GetType() == typeof(AbstractPlantData))
        {
            //Sets the animator
            _player.CharacterStateManager.SetAnimator("wielding", true);
            //Sets item above head
            _player.ItemAboveHead = characterInventory.Items[pNextSlot];
            //Sets the sprite above the head
            _player.ItemAboveHeadRenderer.sprite = characterInventory.Items[pNextSlot].Item.uiSprite;
            //Update the item containment
            if (_itemDisplayer.Containment != characterInventory.Items[pNextSlot]) _itemDisplayer.SetContainment(characterInventory.Items[pNextSlot]);
            //Checks if there is currently a item snapped
            if (!_itemSnapper.IsSnapped && !_inventoryUIManager.IsOpened)
            {
                _itemSnapper.SetSnappedItem(_itemDisplayer);
                _itemDisplayer.gameObject.SetActive(true);
            }
        }
        else
        { //Reset everything
            _player.CharacterStateManager.SetAnimator("wielding", false);
            _player.ItemAboveHead = null;
            _player.ItemAboveHeadRenderer.sprite = null;
            //itemDisplayer.Icon.enabled = false;
            _itemDisplayer.gameObject.SetActive(false);
            _itemSnapper.ResetSnappedItem();
        }
    }

    public void UpdateSlot()
    {
        CharacterInventory characterInventory = _player.CharacterInventory;
        
        if(characterInventory.Items[SelectedSlot].Item == null) return;
        
        //Checks if the item is a placeable item
        if (characterInventory.Items[SelectedSlot].Item.GetType() == typeof(AbstractPlaceableItem) || characterInventory.Items[SelectedSlot].Item.GetType() == typeof(AbstractPlantData))
        {
            //Sets the animator
            _player.CharacterStateManager.SetAnimator("wielding", true);
            //Sets item above head
            _player.ItemAboveHead = characterInventory.Items[SelectedSlot];
            //Sets the sprite above the head
            _player.ItemAboveHeadRenderer.sprite = characterInventory.Items[SelectedSlot].Item.uiSprite;
            //Update the item containment
            if (_itemDisplayer.Containment != characterInventory.Items[SelectedSlot]) _itemDisplayer.SetContainment(characterInventory.Items[SelectedSlot]);
            //Checks if there is currently a item snapped
            if (!_itemSnapper.IsSnapped && !_inventoryUIManager.IsOpened)
            {
                _itemSnapper.SetSnappedItem(_itemDisplayer);
                //itemDisplayer.Icon.enabled = true;
                _itemDisplayer.gameObject.SetActive(true);
            }
        }
        else
        { //Reset everything
            _player.CharacterStateManager.SetAnimator("wielding", false);
            _player.ItemAboveHead = null;
            _player.ItemAboveHeadRenderer.sprite = null;
            //itemDisplayer.Icon.enabled = false;
            _itemDisplayer.gameObject.SetActive(false);
            _itemSnapper.ResetSnappedItem();
        }
    }

    public AbstractItemData GetItemSelected()
    {
        return _player.CharacterInventory.Items[SelectedSlot].Item;
    }
    
    public bool IsWearingCorrectTool(ToolType pTooltype)
    {
        if (_player.CharacterInventory.Items[SelectedSlot].Item == null) return false;
        
        if (_player.CharacterInventory.Items[SelectedSlot].Item.GetType() == typeof(AbstractToolItem))
        {
            AbstractToolItem tool = (AbstractToolItem) _player.CharacterInventory.Items[SelectedSlot].Item;
            if (tool.tooltype.Equals(pTooltype)) return true;
        }
        return false;
    }

    public bool IsWearingCorrectTools(ToolType[] pTooltypes)
    {
        return pTooltypes.Any(type => IsWearingCorrectTool(type));
    }
}
