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

    void InventoryChanged(List<int> slotsUpdated)
    {
        onInventoryChanged.Invoke(slotsUpdated);
        slotsUpdated.Clear();
    }

    private List<int> slotsUpdated = new List<int>();
    
    /// <summary>
    /// Debugging the inventory
    /// </summary>
    public bool debugInventory = true;
    /// <summary>
    /// Debugging the inventory
    /// </summary>
    private bool allowShifting = false;

    public bool AllowShifting
    {
        set => allowShifting = value;
    }

    /// <summary>
    /// Max amount of slots within this inventory
    /// </summary>
    public int maxInventorySize;
    /// <summary>
    /// Stack type of the inventory
    /// </summary>
    public StackType stackType = StackType.STANDARD;
    /// <summary>
    /// Array of all the items
    /// </summary>
    public Item[] items;

    public virtual void Awake()
    {
        Setup();
    }

    public virtual void Start() { }

    public void Setup()
    {
        items = new Item[maxInventorySize];
        
        for (int index = 0; index < maxInventorySize; index++)
            items[index] = new Item();
    }
    
    public void Set(int slot, Item item, bool update = true)
    {
        items[slot] = item.amount == 0 ? null : item;
        
        if (update)
        {
            slotsUpdated.Add(slot);
            InventoryChanged(slotsUpdated);
        }
    }

    public void Swap(int from, int to, bool update = true)
    {
        //Creates an copy of the item
        var temp = items[from];
        
        //Spawns around the 2 items
        items[from] = items[to];
        items[to] = temp;

        if (update)
        {
            slotsUpdated.Add(from);
            slotsUpdated.Add(to);

            InventoryChanged(slotsUpdated);
        }
    }

    protected void AddItem(AbstractItemData item, int itemAmount = 1)
    {
        if (!ItemFitsInventory())
        {
            if(debugInventory) Debug.LogError($"There is no room for the item[{item.name}].");
            return;
        }
        var newSlot = GetFreeSlot();
        if ((item.stackable || stackType.Equals(StackType.ALWAYS_STACK)) && HasItem(item, 0)) newSlot = GetSlot(item);

        if (newSlot == -1)
        {
            if(debugInventory) Debug.LogError($"No slot to add the item[{item.name}].");
            return;
        }
        
        if (item.stackable || stackType.Equals(StackType.ALWAYS_STACK))
        {
            Item currentItem = items[newSlot];
            if (currentItem.item == null) currentItem.item = item;
            
            var totalAmount = currentItem.amount + itemAmount;
            if (totalAmount >= int.MaxValue || totalAmount < 1)
            {
                if(debugInventory) Debug.LogError($"Total amount is higher then max int or amount is 0.");
                return;
            }
            
            currentItem.amount = totalAmount;
            slotsUpdated.Add(newSlot);
        }
        else
        {
            for (int index = 0; index < itemAmount; index++)
            {
                int freeSlot = GetFreeSlot();
                if (freeSlot == -1)
                {
                    if(debugInventory) Debug.LogError("No free slots were found.");
                    return;
                }
                items[freeSlot] = new Item(item);
                slotsUpdated.Add(freeSlot);
            }
        }
        InventoryChanged(slotsUpdated);
    }

    public void RemoveItem(AbstractItemData item, int itemAmount = 1, bool allowZero = false)
    {
        int slot = GetSlot(item);
        if (slot == -1)
        {
            if(debugInventory) Debug.LogError($"There is no slot found with this item[{item.name}].");
            return;
        }

        Item currentItem = items[slot];
        bool shiftContainer = false;
        if (currentItem == null)
        {
            if(debugInventory) Debug.Log($"There is currently no item in slot[{slot}].");
            return;
        }

        if (item.stackable || stackType.Equals(StackType.ALWAYS_STACK))
        {
            if (currentItem.amount > itemAmount) currentItem.amount -= itemAmount;
            else
            {
                if(!allowZero) currentItem.item = null;
                currentItem.amount = 0;
                shiftContainer = true;
            }
            slotsUpdated.Add(slot);
        }
        else
        {
            for (int index = 0; index < itemAmount; index++)
            {
                slot = GetSlot(item);
                if (slot != -1)
                {
                    currentItem = items[slot];
                    currentItem.item = null;
                    currentItem.amount = 0;
                    
                    slotsUpdated.Add(slot);
                } else if(debugInventory) Debug.LogError($"There is no item[{item.name}] to remove.");
            }
        }

        if(allowShifting && shiftContainer) Shift();
        
        InventoryChanged(slotsUpdated);
    }

    public int GetNextOccupiedSlot(int currentSlot, bool increase = true)
    {
        currentSlot += increase ? 1 : -1;
        for (int index = currentSlot, tries = 0; index < 12; tries++)
        {
            //Make sure it only has 12 tries, else it creates a inf loop
            if (tries > 12) break;
            
            if (index < 0) index = 11;
            else if (index > 10) index = 0;
            
            if (items[index].item != null) return index;
            
            if (increase) index++;
            else index--;
        }
        return -1;
    }
    
    public bool HasItem(AbstractItemData item, int itemAmount = 1)
    {
        return items.Any(data => data.item == item && data.amount >= itemAmount);
    }

    public bool HasItems(List<Item> items)
    {
        return items.All(item => HasItem(item.item, item.amount));
    }

    public bool ItemFitsInventory()
    {
        
        //return SlotsOccupied() < maxInventorySize;
        return items.Any(data => data.item == null);
    }

    int GetSlot(AbstractItemData item)
    {
        for (int index = 0; index < maxInventorySize; index++)
            if (items[index].item == item) return index;
        return -1;
    }

    int GetFreeSlot()
    {
        for (int index = 0; index < maxInventorySize; index++)
            if (items[index].item == null) return index;
        return -1;
    }

    public int SlotsOccupied()
    {
        int amount = 0;
        for (int index = 0; index < maxInventorySize; index++)
        {
            if (items[index].item != null)
                amount++;
        }

        return amount;
    }
    
    public void Shift() {
        Item[] old = items;
        items = new Item[maxInventorySize];
        int newIndex = 0;
        for (int i = 0; i < items.Length; i++) {
            if (old[i].item != null) {
                items[newIndex] = old[i];
                newIndex++;
            }
        }
        //TODO: Update slot list
        InventoryChanged(slotsUpdated);
    }
}

public enum StackType
{
    STANDARD,
    ALWAYS_STACK
}