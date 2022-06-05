using System.Collections.Generic;
using UnityEngine;

public class RoomFirstDungeonGenerator : SimpleRandomWalkDungeonGenerator
{
    private AbstractRandomRoomFirst _randomDungeon => (AbstractRandomRoomFirst) _abstractRandomDungeon;

    protected override void RunProceduralGeneration()
    {
        CreateRooms();
    }

    private void CreateRooms()
    {
        List<BoundsInt> rooms = ProceduralGenerationAlgorithms.BinarySpacePartitioning(new BoundsInt((Vector3Int) _startPosition, new Vector3Int(_randomDungeon.DungeonWidth, _randomDungeon.DungeonHeight, 0)), _randomDungeon.MinRoomWidth, _randomDungeon.MinRoomHeight);

        HashSet<Vector2Int> floor = new HashSet<Vector2Int>();

        if (_randomDungeon.RandomRooms) floor = CreateRoomsRandomly(rooms);
        else floor = CreateSimpleRooms(rooms);

        List<Vector2Int> roomCenterPoints = new List<Vector2Int>();

        foreach (BoundsInt room in rooms)
            roomCenterPoints.Add((Vector2Int)Vector3Int.RoundToInt(room.center));

        HashSet<Vector2Int> corridors = ConnectRooms(roomCenterPoints);
        floor.UnionWith(corridors);
        
        _tilemapVisualizer.PaintFloorTiles(floor);
        WallGenerator.CreateWalls(floor, _tilemapVisualizer);
    }

    private HashSet<Vector2Int> CreateRoomsRandomly(List<BoundsInt> pRooms)
    {
        HashSet<Vector2Int> floor = new HashSet<Vector2Int>();
        for (int index = 0; index < pRooms.Count; index++)
        {
            BoundsInt roomBounds = pRooms[index];
            Vector2Int centerOfRoom = new Vector2Int(Mathf.RoundToInt(roomBounds.center.x), Mathf.RoundToInt(roomBounds.center.y));
            HashSet<Vector2Int> randomFloor = RunRandomWalk(_randomDungeon, centerOfRoom, false);

            foreach (Vector2Int position in randomFloor)
            {
                if (position.x >= (roomBounds.xMin + _randomDungeon.RoomOffset) && position.x <= (roomBounds.xMax - _randomDungeon.RoomOffset) && position.y >= (roomBounds.yMin - _randomDungeon.RoomOffset) && position.y <= (roomBounds.yMax - _randomDungeon.RoomOffset))
                    floor.Add(position);
            }
        }
        
        _tilemapVisualizer.PaintPlaceableTiles(floor);

        return floor;
    }

    private HashSet<Vector2Int> ConnectRooms(List<Vector2Int> pRoomCenterPoints)
    {
        HashSet<Vector2Int> corridors = new HashSet<Vector2Int>();
        Vector2Int currentRoomCenter = pRoomCenterPoints[Random.Range(0, pRoomCenterPoints.Count)];

        pRoomCenterPoints.Remove(currentRoomCenter);

        while (pRoomCenterPoints.Count > 0)
        {
            Vector2Int closestCenter = FindClosestPointTo(currentRoomCenter, pRoomCenterPoints);
            pRoomCenterPoints.Remove(closestCenter);
            
            HashSet<Vector2Int> newCorridor = CreateCorridor(currentRoomCenter, closestCenter);
            currentRoomCenter = closestCenter;
            
            corridors.UnionWith(newCorridor);
        }
        
        return corridors;
    }

    private HashSet<Vector2Int> CreateCorridor(Vector2Int currentRoomCenter, Vector2Int pDestination)
    {
        HashSet<Vector2Int> corridor = new HashSet<Vector2Int>();
        Vector2Int position = currentRoomCenter;
        corridor.Add(position);

        while (position.y != pDestination.y)
        {
            if (pDestination.y > position.y) position += Vector2Int.up;
            else if(pDestination.y < position.y) position += Vector2Int.down;

            corridor.Add(position);
        }

        while (position.x != pDestination.x)
        {
            if (pDestination.x > position.x) position += Vector2Int.right;
            else if (pDestination.x < position.x) position += Vector2Int.left;

            corridor.Add(position);
        }
        
        return corridor;
    }

    private Vector2Int FindClosestPointTo(Vector2Int pCurrentRoomCenter, List<Vector2Int> pRoomCenterPoints)
    {
        Vector2Int closestPoint = Vector2Int.zero;
        float distance = float.MaxValue;

        foreach (Vector2Int centerPoint in pRoomCenterPoints)
        {
            float currentDistance = Vector2.Distance(centerPoint, pCurrentRoomCenter);
            if (currentDistance < distance)
            {
                distance = currentDistance;
                closestPoint = centerPoint;
            }
        }

        return closestPoint;
    }

    private HashSet<Vector2Int> CreateSimpleRooms(List<BoundsInt> pRooms)
    {
        HashSet<Vector2Int> floor = new HashSet<Vector2Int>();

        foreach (BoundsInt room in pRooms)
        {
            for (int x = _randomDungeon.RoomOffset; x < room.size.x - _randomDungeon.RoomOffset; x++)
            {
                for (int y = _randomDungeon.RoomOffset; y < room.size.y - _randomDungeon.RoomOffset; y++)
                {
                    Vector2Int position = (Vector2Int) room.min + new Vector2Int(x, y);
                    floor.Add(position);
                }
            }
        }
        
        _tilemapVisualizer.PaintPlaceableTiles(floor);
        
        return floor;
    }
}
