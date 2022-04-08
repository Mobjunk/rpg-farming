using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ProceduralGenerationAlgorithms
{
    /// <summary>
    /// Returns a simple random walk
    /// </summary>
    /// <param name="pStartPosition">The starting position</param>
    /// <param name="pWalkLength">The length of the walk</param>
    /// <returns></returns>
    public static HashSet<Vector2Int> SimpleRandomWalk(Vector2Int pStartPosition, int pWalkLength)
    {
        HashSet<Vector2Int> path = new HashSet<Vector2Int>();

        path.Add(pStartPosition);
        Vector2Int prevPosition = pStartPosition;

        for (int index = 0; index < pWalkLength; index++)
        {
            Vector2Int newPos = prevPosition + Directions.GetRandomCardinalDirection();
            path.Add(newPos);
            prevPosition = newPos;
        }

        return path;
    }

    public static List<Vector2Int> RandomWalkCorridor(Vector2Int pStartPosition, int pCorridorLength)
    {
        List<Vector2Int> corridor = new List<Vector2Int>();

        Vector2Int direction = Directions.GetRandomCardinalDirection();

        Vector2Int currentPosition = pStartPosition;
        corridor.Add(currentPosition);
        
        for (int index = 0; index < pCorridorLength; index++)
        {
            currentPosition += direction;
            corridor.Add(currentPosition);
        }

        return corridor;
    }

    public static List<BoundsInt> BinarySpacePartitioning(BoundsInt pSpaceToSplit, int pMinWidth, int pMinHeight)
    {
        Queue<BoundsInt> roomsQueue = new Queue<BoundsInt>();
        List<BoundsInt> roomList = new List<BoundsInt>();
        
        roomsQueue.Enqueue(pSpaceToSplit);

        while (roomsQueue.Count > 0)
        {
            BoundsInt room = roomsQueue.Dequeue();
            if (room.size.y >= pMinHeight && room.size.x >= pMinWidth)
            {
                if (Random.value < 0.5f)
                {
                    if (room.size.y >= pMinHeight * 2) SplitHorizontally(pMinHeight, roomsQueue, room);
                    else if(room.size.x >= pMinWidth * 2) SplitVertically(pMinWidth, roomsQueue, room);
                    else roomList.Add(room);
                }
                else
                {
                    if(room.size.x >= pMinWidth * 2) SplitVertically(pMinWidth, roomsQueue, room);
                    else if (room.size.y >= pMinHeight * 2) SplitHorizontally(pMinHeight, roomsQueue, room);
                    else roomList.Add(room);
                }
            }
        }

        return roomList;
    }

    private static void SplitVertically(int pMinWidth, Queue<BoundsInt> pRoomsQueue, BoundsInt pRoom)
    {
        int xSplit = Random.Range(1, pRoom.size.x);
        BoundsInt roomOne = new BoundsInt(pRoom.min, new Vector3Int(xSplit, pRoom.size.y, pRoom.size.z));
        BoundsInt roomTwo = new BoundsInt(new Vector3Int(pRoom.min.x + xSplit, pRoom.min.y, pRoom.min.z), new Vector3Int(pRoom.size.x - xSplit, pRoom.size.y, pRoom.size.z));
        pRoomsQueue.Enqueue(roomOne);
        pRoomsQueue.Enqueue(roomTwo);
    }

    private static void SplitHorizontally(int pMinHeight, Queue<BoundsInt> pRoomsQueue, BoundsInt pRoom)
    {
        int ySplit = Random.Range(1, pRoom.size.y);
        BoundsInt roomOne = new BoundsInt(pRoom.min, new Vector3Int(pRoom.size.x, ySplit, pRoom.size.z));
        BoundsInt roomTwo = new BoundsInt(new Vector3Int(pRoom.min.x, pRoom.min.y + ySplit, pRoom.min.z), new Vector3Int(pRoom.size.x, pRoom.size.y - ySplit, pRoom.size.z));
        pRoomsQueue.Enqueue(roomOne);
        pRoomsQueue.Enqueue(roomTwo);
    }
}