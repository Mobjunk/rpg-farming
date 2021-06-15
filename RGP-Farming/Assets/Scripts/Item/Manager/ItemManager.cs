using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ItemManager : Singleton<ItemManager>
{

    public List<AbstractItemData> items = new List<AbstractItemData>();
    
    public List<CraftingRecipeData> craftingRecipes = new List<CraftingRecipeData>();
    
    public AbstractItemData ForName(string itemName)
    {
        return items.FirstOrDefault(itemData => itemData.name.ToLower().Equals(itemName.ToLower()));
    }

    private void Awake()
    {
        foreach (AbstractItemData item in items)
        {
            if (item.craftingRecipe == null) continue;
            craftingRecipes.Add(new CraftingRecipeData(item, item.craftingRecipe));
        }
    }
}

public class CraftingRecipeData
{
    public AbstractItemData craftedItem;
    public AbstractCraftingRecipe craftingRecipe;

    public CraftingRecipeData(AbstractItemData craftedItem, AbstractCraftingRecipe craftingRecipe)
    {
        this.craftedItem = craftedItem;
        this.craftingRecipe = craftingRecipe;
    }
}
