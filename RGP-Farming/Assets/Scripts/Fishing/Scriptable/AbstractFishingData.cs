using UnityEngine;

[CreateAssetMenu(fileName = "New Fish", menuName = "Fish/New Fish")]
[System.Serializable]
public class AbstractFishingData : ScriptableObject
{
    public AbstractItemData baitRequired;
    public AbstractItemData fish;
}