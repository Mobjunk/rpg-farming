using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Crop", menuName = "New Crop")]
public class Crops : ScriptableObject
{

    [Header("Text")]
    public new string name;
    public string discription;

    [Header("Variables")]
    public float timeBetweenGrowthStage;
    public bool useOfWater; 
    public int harvestAmount;

    [Header("Min and max amount randomize value")]
    public int harvestModifier;

    [Header("Sprites")]
    public AbstractItemData harvestedItem;
    public Sprite[] spriteStages;
}
