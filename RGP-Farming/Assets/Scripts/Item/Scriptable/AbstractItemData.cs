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
    [Header("Item Type")] public ItemType tooltype;
    [Header("Optional data")] public AbstractCraftingRecipe craftingRecipe;
}

public enum ItemType
{
    NONE,
    AXE,
    PICKAXE,
    SCYTHE,
    HOE,
    WATERING_CAN,
    SWORD
}