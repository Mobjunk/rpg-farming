using UnityEngine;

[CreateAssetMenu(fileName = "New Placeable Item", menuName = "Items/New Placeable Item")]
public class AbstractPlaceableItem : AbstractItemData
{
    [Header("Object prefab")] public GameObject objectPrefab;
    [Header("Settings")] public bool boundToTiles;
}
