using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CorridorFirstDungeonGenerator : SimpleRandomWalkDungeonGenerator
{
    private AbstractRandomCorridorFirst _randomDungeon => (AbstractRandomCorridorFirst) _abstractRandomDungeon;
    
    protected override void RunProceduralGeneration()
    {
        CorridorFirstGeneration();
    }

    private void CorridorFirstGeneration()
    {
        HashSet<Vector2Int> floorPositions = new HashSet<Vector2Int>();
        HashSet<Vector2Int> potentialRoomPositions = new HashSet<Vector2Int>();
        
        CreateCorridors(floorPositions, potentialRoomPositions);

        HashSet<Vector2Int> roomPositions = CreateRooms(potentialRoomPositions);

        List<Vector2Int> deadEnds = FindAllDeadEnds(floorPositions);

        CreateRoomsAtDeadEnd(deadEnds, roomPositions);
        
        floorPositions.UnionWith(roomPositions);
        
        _tilemapVisualizer.PaintFloorTiles(floorPositions);
        WallGenerator.CreateWalls(floorPositions, _tilemapVisualizer);
    }

    private void CreateRoomsAtDeadEnd(List<Vector2Int> pDeadEnds, HashSet<Vector2Int> pRoomFloors)
    {
        foreach (Vector2Int position in pDeadEnds)
        {
            if (!pRoomFloors.Contains(position))
            {
                HashSet<Vector2Int> room = RunRandomWalk(_randomDungeon, position);
                pRoomFloors.UnionWith(room);
            }
        }
    }

    private List<Vector2Int> FindAllDeadEnds(HashSet<Vector2Int> pFloorPositions)
    {
        List<Vector2Int> deadEnds = new List<Vector2Int>();

        foreach (Vector2Int position in pFloorPositions)
        {
            int neighbourCount = 0;
            foreach (Vector2Int dir in Directions.CardinalDirectionsList)
            {
                if (pFloorPositions.Contains(position + dir))
                    neighbourCount++;
            }

            if (neighbourCount == 1)
                deadEnds.Add(position);
        }
        
        return deadEnds;
    }

    private HashSet<Vector2Int> CreateRooms(HashSet<Vector2Int> pPotentialRoomPositions)
    {
        HashSet<Vector2Int> roomPositions = new HashSet<Vector2Int>();

        int totalRoomsToCreate = Mathf.RoundToInt(pPotentialRoomPositions.Count * _randomDungeon.RoomPercent);

        List<Vector2Int> roomsToCreate = pPotentialRoomPositions.OrderBy(x => Guid.NewGuid()).Take(totalRoomsToCreate).ToList();

        foreach (Vector2Int roomPosition in roomsToCreate)
        {
            HashSet<Vector2Int> roomFloor = RunRandomWalk(_randomDungeon, roomPosition);
            roomPositions.UnionWith(roomFloor);
        }

        return roomPositions;
    }

    private void CreateCorridors(HashSet<Vector2Int> pFloorPositions, HashSet<Vector2Int> pPotentialRoomPositions)
    {
        Vector2Int currentPosition = _startPosition;
        pPotentialRoomPositions.Add(currentPosition);

        for (int index = 0; index < _randomDungeon.CorridorCount; index++)
        {
            List<Vector2Int> path = ProceduralGenerationAlgorithms.RandomWalkCorridor(currentPosition, _randomDungeon.CorridorLength);
            currentPosition = path[path.Count - 1];

            pPotentialRoomPositions.Add(currentPosition);
            
            pFloorPositions.UnionWith(path);
        }
    }
}
