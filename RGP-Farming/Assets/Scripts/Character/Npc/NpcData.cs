using UnityEngine;

[CreateAssetMenu(fileName = "New Npc", menuName = "Npcs/New Npc")]
public class NpcData : ScriptableObject
{
    [Header("Randomizer")] public bool randomizeValues;


    [Header("Random walking")]
    public bool randomWalking;
    public int walkingRandius;
}
