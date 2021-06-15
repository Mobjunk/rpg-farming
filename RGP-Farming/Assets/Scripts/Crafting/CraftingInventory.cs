using System.Collections.Generic;
using UnityEngine;

public class CraftingInventory : AbstractItemInventory
{
    private ItemManager itemManager => ItemManager.Instance();

    public override void Start()
    {
        maxInventorySize = itemManager.craftingRecipes.Count;
        Setup();

        for (int slot = 0; slot < maxInventorySize; slot++)
        {
            CraftingRecipeData recipe = itemManager.craftingRecipes[slot];
            items[slot] = new Item(recipe.craftedItem);
        }
    }
}
