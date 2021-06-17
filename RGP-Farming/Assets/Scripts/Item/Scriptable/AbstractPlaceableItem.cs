using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "New Placeable Item", menuName = "Items/New Placeable Item")]
public class AbstractPlaceableItem : AbstractItemData
{
    [Header("Object prefab")] public GameObject objectPrefab;
    [Header("Settings")]
    public int width = 1;
    public int height = 1;
}
