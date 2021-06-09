using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Tool", menuName = "New Tool")]
public class AbstractToolItem : AbstractItemData
{
    [Header("Tooltype")] public ToolType tooltype;
}

public enum ToolType
{
    AXE,
    PICKAXE,
    SCYTHE,
    HOE,
    WATERING_CAN,
    SWORD
}
