using UnityEngine;

[CreateAssetMenu(fileName = "New Consumable", menuName = "Items/New Consumable")]
public class AbstractConsumableItem : AbstractItemData
{
    [Header("Restoration data")]
    public int healthRestoration;
    public int energyRestoration;
}
