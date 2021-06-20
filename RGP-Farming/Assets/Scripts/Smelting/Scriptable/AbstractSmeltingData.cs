using UnityEngine;

[CreateAssetMenu(fileName = "New Smelting", menuName = "Smelting/New Smelting")]
public class AbstractSmeltingData : ScriptableObject
{
    public AbstractItemData baseItem;
    public AbstractItemData completedItem;
    public float smeltTime;
}
