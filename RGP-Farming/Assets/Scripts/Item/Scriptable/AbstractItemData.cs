using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Item", menuName = "Items/New Item")]
[System.Serializable]
public class AbstractItemData : ScriptableObject
{
    public string itemName;
    public Sprite uiSprite;
    public bool stackable;
    public int itemPrice;
    [Header("Optional data")] public AbstractCraftingRecipe craftingRecipe;
}