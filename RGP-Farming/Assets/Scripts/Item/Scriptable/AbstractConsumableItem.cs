using UnityEngine;

[CreateAssetMenu(fileName = "New Consumable", menuName = "Items/New Consumable")]
public class AbstractConsumableItem : AbstractItemData
{
    public int healthRestoration;
    public int energyRestoration;
}
