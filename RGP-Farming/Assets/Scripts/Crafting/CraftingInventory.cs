using System.Collections.Generic;
using UnityEngine;

public class CraftingInventory : AbstractItemInventory
{
    private ItemManager _itemManager => ItemManager.Instance();

    public override void Start()
    {
        _maxInventorySize = _itemManager.CraftingRecipes.Count;
        Setup();

        for (int slot = 0; slot < _maxInventorySize; slot++)
        {
            CraftingRecipeData recipe = _itemManager.CraftingRecipes[slot];
            Items[slot] = new GameItem(recipe.CraftedItem);
        }
    }
}
