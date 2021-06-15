using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Tool", menuName = "Items/New Tool")]
public class AbstractToolItem : AbstractItemData
{
    [Header("Tool Type")] public ToolType tooltype;
    [Header("Tool damage")] public int toolDamage;
}

public enum ToolType
{
    NONE,
    AXE,
    PICKAXE,
    SCYTHE,
    HOE,
    WATERING_CAN,
    SWORD
}