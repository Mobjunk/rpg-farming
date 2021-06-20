using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SmeltingManager : Singleton<SmeltingManager>
{
    [SerializeField] private List<AbstractSmeltingData> smeltingRecipes = new List<AbstractSmeltingData>();

    public List<AbstractSmeltingData> GetSmeltingRecipes() => smeltingRecipes;

    public AbstractSmeltingData GetSmeltingData(AbstractItemData ore)
    {
        return smeltingRecipes.FirstOrDefault(data => data.baseItem == ore);
    }
}
