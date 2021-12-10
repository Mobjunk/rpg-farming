using System.Collections.Generic;
using UnityEngine;

public class CraftingInventory : AbstractItemInventory
{
    private ItemManager _itemManager => ItemManager.Instance();

    public override void Start()
    {
        MaxInventorySize = _itemManager.craftingRecipes.Count;
        Setup();

        for (int slot = 0; slot < MaxInventorySize; slot++)
        {
            CraftingRecipeData recipe = _itemManager.craftingRecipes[slot];
            Items[slot] = new Item(recipe.craftedItem);
        }
    }
}
