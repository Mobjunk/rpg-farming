using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class ItemBarManager : MenuManager<ItemBarManager>
{
    private CharacterInputManager _characterInputManager => CharacterInputManager.Instance();
    private ItemSnapperManager _itemSnapper => ItemSnapperManager.Instance();
    private Player _player => Player.Instance();
    private DialogueManager _dialogueManager => DialogueManager.Instance();
    
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
        if (_player.CharacterAction != null || _dialogueManager.DialogueIsPlaying) return;
        
        //TODO: Clean this mess
        if(_characterInputManager.SlotOne.WasPressedThisFrame()) UpdateSlot(0);
        else if(_characterInputManager.SlotTwo.WasPressedThisFrame()) UpdateSlot(1);
        else if(_characterInputManager.SlotThree.WasPressedThisFrame()) UpdateSlot(2);
        else if(_characterInputManager.SlotFour.WasPressedThisFrame()) UpdateSlot(3);
        else if(_characterInputManager.SlotFive.WasPressedThisFrame()) UpdateSlot(4);
        else if(_characterInputManager.SlotSix.WasPressedThisFrame()) UpdateSlot(5);
        else if(_characterInputManager.SlotSeven.WasPressedThisFrame()) UpdateSlot(6);
        else if(_characterInputManager.SlotEight.WasPressedThisFrame()) UpdateSlot(7);
        else if(_characterInputManager.SlotNine.WasPressedThisFrame()) UpdateSlot(8);
        else if(_characterInputManager.SlotTen.WasPressedThisFrame()) UpdateSlot(9);
        else if(_characterInputManager.SlotEleven.WasPressedThisFrame()) UpdateSlot(10);
        else if(_characterInputManager.SlotTwelve.WasPressedThisFrame()) UpdateSlot(11);
        else if(_characterInputManager.NextSlot.WasPressedThisFrame()) UpdateSlot(1f);
        else if(_characterInputManager.PrevSlot.WasPressedThisFrame()) UpdateSlot(-1f);
        else if(!_characterInputManager.GamepadActive) UpdateSlot(Mouse.current.scroll.ReadValue().y); //Input.mouseScrollDelta.y
    }

    void UpdateSlot(float pValue)
    {
        if ((Mouse.current.scroll.ReadValue().y == 0 && !_characterInputManager.GamepadActive) || _player.CharacterUIManager.CurrentUIOpened != null) return;

        CharacterInventory characterInventory = _player.CharacterInventory;
        int nextSlot = characterInventory.GetNextOccupiedSlot(SelectedSlot, pValue > 0);
        if (nextSlot == -1) return;

        UpdateSlot(nextSlot);
    }

    public void UpdateSlot(int pNextSlot)
    {
        if (_player.CharacterAction != null || _dialogueManager.DialogueIsPlaying) return;
        
        CharacterInventory characterInventory = _player.CharacterInventory;

        if (characterInventory.Items[pNextSlot].Item == null) return;
     
        
        _inventoryUIManager._containers[0][SelectedSlot].SetHighlighted(false);
        SelectedSlot = pNextSlot;
        _inventoryUIManager._containers[0][SelectedSlot].SetHighlighted(true);

        //Checks if the item is a placeable item
        if (characterInventory.Items[pNextSlot].Item.GetType() == typeof(AbstractPlaceableItem) || characterInventory.Items[pNextSlot].Item.GetType() == typeof(AbstractPlantData))
        {
            //Sets the animator
            //_player.CharacterStateManager.SetAnimator("wielding", true);
            Utility.SetAnimator(_player.CharacterStateManager.GetAnimator(), "wielding", true);
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
            //_player.CharacterStateManager.SetAnimator("wielding", false);
            Utility.SetAnimator(_player.CharacterStateManager.GetAnimator(), "wielding", false);
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
            //_player.CharacterStateManager.SetAnimator("wielding", true);
            Utility.SetAnimator(_player.CharacterStateManager.GetAnimator(), "wielding", true);
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
            Utility.SetAnimator(_player.CharacterStateManager.GetAnimator(), "wielding", false);
            _player.ItemAboveHead = null;
            _player.ItemAboveHeadRenderer.sprite = null;
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
