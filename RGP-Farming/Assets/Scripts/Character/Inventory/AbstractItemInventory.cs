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

    /// <summary>
    /// Handles setting up the inventory size and initializing the array
    /// </summary>
    public void Setup()
    {
        items = new Item[maxInventorySize];
        
        for (int index = 0; index < maxInventorySize; index++)
            items[index] = new Item();
    }
    
    /// <summary>
    /// Handles setting a slot
    /// </summary>
    /// <param name="slot">The slot being set</param>
    /// <param name="item">The item being set on the slot</param>
    /// <param name="update">Should it update the inventory ui</param>
    public void Set(int slot, Item item, bool update = true)
    {
        items[slot] = item.amount == 0 ? new Item() : item;
        
        if (update)
        {
            slotsUpdated.Add(slot);
            InventoryChanged(slotsUpdated);
        }
    }

    /// <summary>
    /// Handles swapping items between slots
    /// </summary>
    /// <param name="from">The from slot</param>
    /// <param name="to">The to slot</param>
    /// <param name="update">Should it update the inventory ui</param>
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

    /// <summary>
    /// Handles adding a item to a inventory
    /// </summary>
    /// <param name="item">The item being added</param>
    /// <param name="itemAmount">The amount of this item being added</param>
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

    /// <summary>
    /// Handles removing a item from a inventory
    /// </summary>
    /// <param name="item">The item being removed</param>
    /// <param name="itemAmount">The amount of that item being removed</param>
    /// <param name="allowZero">Checks if the containment is allowed to be zero</param>
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

    /// <summary>
    /// Handles updating the durability of a item
    /// </summary>
    /// <param name="item">The item that is being updated</param>
    /// <param name="durability">The amount of durability that gets added or removed</param>
    public void UpdateDurability(AbstractItemData item, int durability)
    {
        int slot = GetSlot(item);
        if (slot == -1)
        {
            if(debugInventory) Debug.LogError($"There is no slot found with this item[{item.name}].");
            return;
        }

        Item currentItem = items[slot];

        if (currentItem.durability == -1 || currentItem.maxDurability == -1)
        {
            if(debugInventory) Debug.LogError($"There is no durability found with this item[{item.name}].");
            return;
        }

        currentItem.durability += durability;
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
        
        slotsUpdated.Add(slot);
        
        InventoryChanged(slotsUpdated);
    }

    /// <summary>
    /// Gets the next occupied slot in the inventory
    /// </summary>
    /// <param name="currentSlot">The current slot the player is on</param>
    /// <param name="increase">Should it start counting up</param>
    /// <returns></returns>
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
    
    /// <summary>
    /// Checks if the player has a certain item
    /// </summary>
    /// <param name="item"></param>
    /// <param name="itemAmount"></param>
    /// <returns></returns>
    public bool HasItem(AbstractItemData item, int itemAmount = 1)
    {
        return items.Any(data => data.item == item && data.amount >= itemAmount);
    }

    /// <summary>
    /// Checks if the player has all the items
    /// </summary>
    /// <param name="items">A list of items</param>
    /// <returns></returns>
    public bool HasItems(List<Item> items)
    {
        return items.All(item => HasItem(item.item, item.amount));
    }

    /// <summary>
    /// Checks if there is room in the inventory
    /// </summary>
    /// <returns></returns>
    public bool ItemFitsInventory()
    {
        return items.Any(data => data.item == null);
    }

    /// <summary>
    /// Get the slot for a certain item
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    int GetSlot(AbstractItemData item)
    {
        for (int index = 0; index < maxInventorySize; index++)
            if (items[index].item == item) return index;
        return -1;
    }

    /// <summary>
    /// Get the next free available slot
    /// </summary>
    /// <returns></returns>
    int GetFreeSlot()
    {
        for (int index = 0; index < maxInventorySize; index++)
            if (items[index].item == null) return index;
        return -1;
    }

    /// <summary>
    /// Check how many slots there are occupied
    /// </summary>
    /// <returns></returns>
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
    
    /// <summary>
    /// Handles the shifting of a inventory
    /// Currently not being used tho
    /// </summary>
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