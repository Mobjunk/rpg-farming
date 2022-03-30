using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ItemManager : Singleton<ItemManager>
{

    public List<AbstractItemData> Items = new List<AbstractItemData>();
    
    public List<CraftingRecipeData> CraftingRecipes = new List<CraftingRecipeData>();
    
    public AbstractItemData ForName(string pItemName)
    {
        return Items.FirstOrDefault(itemData => itemData != null && itemData.name.ToLower().Equals(pItemName.ToLower()));
    }

    private void Awake()
    {
        foreach (AbstractItemData item in Items)
        {
            if (item == null || item.craftingRecipe == null) continue;
            CraftingRecipes.Add(new CraftingRecipeData(item, item.craftingRecipe));
        }
    }
}

public class CraftingRecipeData
{
    public AbstractItemData CraftedItem;
    public AbstractCraftingRecipe CraftingRecipe;

    public CraftingRecipeData(AbstractItemData pCraftedItem, AbstractCraftingRecipe pCraftingRecipe)
    {
        this.CraftedItem = pCraftedItem;
        this.CraftingRecipe = pCraftingRecipe;
    }
}
