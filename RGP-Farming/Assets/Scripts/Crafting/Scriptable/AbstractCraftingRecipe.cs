using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Crafting Recipe", menuName = "Crafting/New Recipe")]
public class AbstractCraftingRecipe : ScriptableObject
{
    public List<GameItem> requiredItems = new List<GameItem>();
}
