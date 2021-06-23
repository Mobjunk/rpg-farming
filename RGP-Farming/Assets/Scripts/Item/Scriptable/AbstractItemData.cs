using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Item", menuName = "Items/New Item")]
[System.Serializable]
public class AbstractItemData : ScriptableObject
{
    public string itemName;
    public string itemDescription;
    public Sprite uiSprite;
    public bool stackable;
    public int itemPrice;
    [Header("Item Type")] public ItemType itemType;
    [Header("Optional data")] public AbstractCraftingRecipe craftingRecipe;
    [Header("Durability")] public int durability = -1;
}

public enum ItemType
{
    NONE,
    TOOL,
    CONSUMABLE,
    RESOURCE,
    OBJECT,
    SEED
}