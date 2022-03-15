using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "New Crop", menuName = "New Crop")]
public class Crops : ScriptableObject
{
    [Header("Text")]
    public new string name;
    public string discription;

    [Header("Variables")]
    [FormerlySerializedAs("timeBetweenGrowthStage")] public float growthTime; //timeBetweenGrowthStage
    public int harvestAmount;

    [Header("Min and max amount randomize value")]
    public int harvestModifier;

    [Header("Sprites")]
    public AbstractConsumableItem harvestedItem;
    public GrowStages[] growStages;
}

[System.Serializable]
public class GrowStages
{
    public Sprite Sprite;
    public bool NeedsWaterThisStage;
}