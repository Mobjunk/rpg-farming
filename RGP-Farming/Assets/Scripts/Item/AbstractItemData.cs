using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New item", menuName = "Items/New Item")]
[System.Serializable]
public class AbstractItemData : ScriptableObject
{
    public string itemName;
    public Sprite uiSprite;
    public bool stackable;
}