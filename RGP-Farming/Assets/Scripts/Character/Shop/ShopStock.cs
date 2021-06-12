using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Shop", menuName = "New Shop")]
public class ShopStock : ScriptableObject
{
    public float buyRatio;
    public float sellRatio;
    public NpcData linkedNpc;
    public List<Item> items = new List<Item>();
}