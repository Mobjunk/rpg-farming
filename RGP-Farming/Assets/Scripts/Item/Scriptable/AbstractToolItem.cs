using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Tool", menuName = "Items/New Tool")]
public class AbstractToolItem : AbstractItemData
{
    [Header("Tool damage")] public int toolDamage;
}
