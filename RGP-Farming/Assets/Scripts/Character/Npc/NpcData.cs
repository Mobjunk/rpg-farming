using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "", menuName = "New Npc")]
public class NpcData : ScriptableObject
{
    [Header("Randomizer")] public bool randomizeValues;


    [Header("Random walking")]
    public bool randomWalking;
    public int walkingRandius;
}
