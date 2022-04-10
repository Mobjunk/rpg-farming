using UnityEngine;

[CreateAssetMenu(fileName = "CorridorFirstParameters_", menuName = "Dungeons/CorridorFirst")]
public class AbstractRandomCorridorFirst : AbstractRandomDungeon
{
    [Header("Corridor Settings")]
    public int CorridorLength = 14;
    public int CorridorCount = 5;
    [Range(0.1f, 1)] public float RoomPercent = 0.8f;
}
