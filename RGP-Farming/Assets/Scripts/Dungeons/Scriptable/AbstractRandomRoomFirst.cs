using UnityEngine;

[CreateAssetMenu(fileName = "RoomFirstParameters_", menuName = "Dungeons/RoomFirst")]
public class AbstractRandomRoomFirst : AbstractRandomDungeon
{
    public int MinRoomWidth = 4;
    public int MinRoomHeight = 4;

    public int DungeonWidth = 20;
    public int DungeonHeight = 20;

    [Range(0, 10)] public int RoomOffset;

    public bool RandomRooms;
}