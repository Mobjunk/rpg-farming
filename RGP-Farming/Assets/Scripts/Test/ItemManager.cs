using System.Collections.Generic;
using System.Linq;

public class ItemManager : Singleton<ItemManager>
{

    public List<AbstractItemData> items = new List<AbstractItemData>();
    
    public AbstractItemData ForName(string itemName)
    {
        return items.FirstOrDefault(itemData => itemData.name.ToLower().Equals(itemName.ToLower()));
    }
}
