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

    [Header("Sprites")]
    public AbstractItemData harvestedItem;
    public Sprite[] spriteStages;
    public Sprite diseased;



}
