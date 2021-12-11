using System.Collections.Generic;
using UnityEngine;

public class CraftingInventory : AbstractItemInventory
{
    private ItemManager itemManager => ItemManager.Instance();

    public override void Start()
    {
        MaxInventorySize = itemManager.craftingRecipes.Count;
        Setup();

        for (int slot = 0; slot < MaxInventorySize; slot++)
        {
            CraftingRecipeData recipe = itemManager.craftingRecipes[slot];
            Items[slot] = new Item(recipe.craftedItem);
        }
    }
}
