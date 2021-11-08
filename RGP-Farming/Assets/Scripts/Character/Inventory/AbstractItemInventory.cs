using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public abstract class AbstractItemInventory : MonoBehaviour
{
    /// <summary>
    /// Handles the inventory changing event
    /// </summary>
    public delegate void OnInventoryChanged(List<int> slotsUpdated);
    public OnInventoryChanged onInventoryChanged = delegate {  };

    void InventoryChanged(List<int> pSlotsUpdated)
    {
        onInventoryChanged.Invoke(pSlotsUpdated);
        pSlotsUpdated.Clear();
    }

    private List<int> SlotsUpdated = new List<int>();
    
    /// <summary>
    /// Debugging the inventory
    /// </summary>
    public bool DebugInventory = true;
    /// <summary>
    /// Debugging the inventory
    /// </summary>
    public bool AllowShifting = false;

    /// <summary>
    /// Max amount of slots within this inventory
    /// </summary>
    public int MaxInventorySize;
    /// <summary>
    /// Stack type of the inventory
    /// </summary>
    public StackType StackType = StackType.STANDARD;
    /// <summary>
    /// Array of all the items
    /// </summary>
    public Item[] Items;

    public virtual void Awake()
    {
        Setup();
    }

    public virtual void Start() { }

    /// <summary>
    /// Handles setting up the inventory size and initializing the array
    /// </summary>
    public void Setup()
    {
        Items = new Item[MaxInventorySize];
        
        for (int index = 0; index < MaxInventorySize; index++)
            Items[index] = new Item();
    }
    
    /// <summary>
    /// Handles setting a slot
    /// </summary>
    /// <param name="slot">The slot being set</param>
    /// <param name="item">The item being set on the slot</param>
    /// <param name="update">Should it update the inventory ui</param>
    public void Set(int pSlot, Item pItem, bool pUpdate = true)
    {
        Items[pSlot] = pItem.amount == 0 ? new Item() : pItem;
        
        if (pUpdate)
        {
            ItemBarManager.Instance().UpdateSlot();
            SlotsUpdated.Add(pSlot);
            InventoryChanged(SlotsUpdated);
        }
    }

    /// <summary>
    /// Handles swapping items between slots
    /// </summary>
    /// <param name="from">The from slot</param>
    /// <param name="to">The to slot</param>
    /// <param name="update">Should it update the inventory ui</param>
    public void Swap(int pFrom, int pTo, bool pUpdate = true)
    {
        //Creates an copy of the item
        var temp = Items[pFrom];
        
        //Spawns around the 2 items
        Items[pFrom] = Items[pTo];
        Items[pTo] = temp;

        if (pUpdate)
        {
            SlotsUpdated.Add(pFrom);
            SlotsUpdated.Add(pTo);

            InventoryChanged(SlotsUpdated);
        }
    }

    /// <summary>
    /// Handles adding a item to a inventory
    /// </summary>
    /// <param name="pItem">The item being added</param>
    /// <param name="pItemAmount">The amount of this item being added</param>
    protected void AddItem(AbstractItemData pItem, int pItemAmount = 1)
    {
        if (!ItemFitsInventory())
        {
            if(DebugInventory) Debug.LogError($"There is no room for the item[{pItem.name}].");
            return;
        }
        var newSlot = GetFreeSlot();
        if ((pItem.stackable || StackType.Equals(StackType.ALWAYS_STACK)) && HasItem(pItem, 0)) newSlot = GetSlot(pItem);

        if (newSlot == -1)
        {
            if(DebugInventory) Debug.LogError($"No slot to add the item[{pItem.name}].");
            return;
        }
        
        if (pItem.stackable || StackType.Equals(StackType.ALWAYS_STACK))
        {
            Item currentItem = Items[newSlot];
            if (currentItem.item == null) currentItem.item = pItem;
            
            var totalAmount = currentItem.amount + pItemAmount;
            if (totalAmount >= int.MaxValue || totalAmount < 1)
            {
                if(DebugInventory) Debug.LogError($"Total amount is higher then max int or amount is 0.");
                return;
            }
            
            currentItem.amount = totalAmount;
            SlotsUpdated.Add(newSlot);
        }
        else
        {
            for (int index = 0; index < pItemAmount; index++)
            {
                int freeSlot = GetFreeSlot();
                if (freeSlot == -1)
                {
                    if(DebugInventory) Debug.LogError("No free slots were found.");
                    return;
                }
                Items[freeSlot] = new Item(pItem);
                SlotsUpdated.Add(freeSlot);
            }
        }
        InventoryChanged(SlotsUpdated);
    }

    /// <summary>
    /// Handles removing a item from a inventory
    /// </summary>
    /// <param name="pItem">The item being removed</param>
    /// <param name="pItemAmount">The amount of that item being removed</param>
    /// <param name="pAllowZero">Checks if the containment is allowed to be zero</param>
    public void RemoveItem(AbstractItemData pItem, int pItemAmount = 1, bool pAllowZero = false)
    {
        int slot = GetSlot(pItem);
        if (slot == -1)
        {
            if(DebugInventory) Debug.LogError($"There is no slot found with this item[{pItem.name}].");
            return;
        }

        Item currentItem = Items[slot];
        bool shiftContainer = false;
        if (currentItem == null)
        {
            if(DebugInventory) Debug.Log($"There is currently no item in slot[{slot}].");
            return;
        }

        if (pItem.stackable || StackType.Equals(StackType.ALWAYS_STACK))
        {
            if (currentItem.amount > pItemAmount) currentItem.amount -= pItemAmount;
            else
            {
                if(!pAllowZero) currentItem.item = null;
                currentItem.amount = 0;
                shiftContainer = true;
            }
            SlotsUpdated.Add(slot);
        }
        else
        {
            for (int index = 0; index < pItemAmount; index++)
            {
                slot = GetSlot(pItem);
                if (slot != -1)
                {
                    currentItem = Items[slot];
                    currentItem.item = null;
                    currentItem.amount = 0;
                    
                    SlotsUpdated.Add(slot);
                } else if(DebugInventory) Debug.LogError($"There is no item[{pItem.name}] to remove.");
            }
        }

        if(AllowShifting && shiftContainer) Shift();
        
        InventoryChanged(SlotsUpdated);
    }

    /// <summary>
    /// Handles updating the durability of a item
    /// </summary>
    /// <param name="pItem">The item that is being updated</param>
    /// <param name="pDurability">The amount of durability that gets added or removed</param>
    public void UpdateDurability(AbstractItemData pItem, int pDurability)
    {
        int slot = GetSlot(pItem);
        if (slot == -1)
        {
            if(DebugInventory) Debug.LogError($"There is no slot found with this item[{pItem.name}].");
            return;
        }

        Item currentItem = Items[slot];

        if (currentItem.durability == -1 || currentItem.maxDurability == -1)
        {
            if(DebugInventory) Debug.LogError($"There is no durability found with this item[{pItem.name}].");
            return;
        }

        currentItem.durability += pDurability;
        if (currentItem.durability <= 0)
        {
            currentItem.durability = 0;
            if (currentItem.item.crumblesToDust)
            {
                currentItem.item = null;
                currentItem.amount = 0;
            }
        } else if (currentItem.durability > currentItem.maxDurability)
            currentItem.durability = currentItem.maxDurability;
        
        SlotsUpdated.Add(slot);
        
        InventoryChanged(SlotsUpdated);
    }

    /// <summary>
    /// Gets the next occupied slot in the inventory
    /// </summary>
    /// <param name="pCurrentSlot">The current slot the player is on</param>
    /// <param name="pIncrease">Should it start counting up</param>
    /// <returns></returns>
    public int GetNextOccupiedSlot(int pCurrentSlot, bool pIncrease = true)
    {
        pCurrentSlot += pIncrease ? 1 : -1;
        for (int index = pCurrentSlot, tries = 0; index < 12; tries++)
        {
            //Make sure it only has 12 tries, else it creates a inf loop
            if (tries > 12) break;
            
            if (index < 0) index = 11;
            else if (index > 10) index = 0;
            
            if (Items[index].item != null) return index;
            
            if (pIncrease) index++;
            else index--;
        }
        return -1;
    }
    
    /// <summary>
    /// Checks if the player has a certain item
    /// </summary>
    /// <param name="pItem"></param>
    /// <param name="pItemAmount"></param>
    /// <returns></returns>
    public bool HasItem(AbstractItemData pItem, int pItemAmount = 1)
    {
        return Items.Any(data => data.item == pItem && data.amount >= pItemAmount);
    }

    /// <summary>
    /// Checks if the player has all the items
    /// </summary>
    /// <param name="pItems">A list of items</param>
    /// <returns></returns>
    public bool HasItems(List<Item> pItems)
    {
        return pItems.All(item => HasItem(item.item, item.amount));
    }

    /// <summary>
    /// Checks if there is room in the inventory
    /// </summary>
    /// <returns></returns>
    public bool ItemFitsInventory()
    {
        return Items.Any(data => data.item == null);
    }

    /// <summary>
    /// Get the slot for a certain item
    /// </summary>
    /// <param name="pItem"></param>
    /// <returns></returns>
    int GetSlot(AbstractItemData pItem)
    {
        for (int index = 0; index < MaxInventorySize; index++)
            if (Items[index].item == pItem) return index;
        return -1;
    }

    /// <summary>
    /// Get the next free available slot
    /// </summary>
    /// <returns></returns>
    int GetFreeSlot()
    {
        for (int index = 0; index < MaxInventorySize; index++)
            if (Items[index].item == null) return index;
        return -1;
    }

    /// <summary>
    /// Check how many slots there are occupied
    /// </summary>
    /// <returns></returns>
    public int SlotsOccupied()
    {
        int amount = 0;
        for (int index = 0; index < MaxInventorySize; index++)
        {
            if (Items[index].item != null)
                amount++;
        }

        return amount;
    }
    
    /// <summary>
    /// Handles the shifting of a inventory
    /// Currently not being used tho
    /// </summary>
    public void Shift() {
        Item[] old = Items;
        Items = new Item[MaxInventorySize];
        int newIndex = 0;
        for (int i = 0; i < Items.Length; i++) {
            if (old[i].item != null) {
                Items[newIndex] = old[i];
                newIndex++;
            }
        }
        //TODO: Update slot list
        InventoryChanged(SlotsUpdated);
    }
}

public enum StackType
{
    STANDARD,
    ALWAYS_STACK
}