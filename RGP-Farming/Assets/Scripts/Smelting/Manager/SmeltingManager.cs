using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SmeltingManager : Singleton<SmeltingManager>
{
    [SerializeField] private List<AbstractSmeltingData> _smeltingRecipes = new List<AbstractSmeltingData>();

    public List<AbstractSmeltingData> GetSmeltingRecipes() => _smeltingRecipes;

    public AbstractSmeltingData GetSmeltingData(AbstractItemData pOre)
    {
        return _smeltingRecipes.FirstOrDefault(data => data.baseItem == pOre);
    }
}
