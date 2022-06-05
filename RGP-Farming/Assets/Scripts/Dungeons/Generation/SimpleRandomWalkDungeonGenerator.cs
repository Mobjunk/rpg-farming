using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SimpleRandomWalkDungeonGenerator : AbstractDungeonGenerator
{
    [SerializeField] protected AbstractRandomDungeon _abstractRandomDungeon;

    protected override void RunProceduralGeneration()
    {
        HashSet<Vector2Int> floorPositions = RunRandomWalk(_abstractRandomDungeon, _startPosition);
        _tilemapVisualizer.PaintFloorTiles(floorPositions);
        WallGenerator.CreateWalls(floorPositions, _tilemapVisualizer);
    }

    protected HashSet<Vector2Int> RunRandomWalk(AbstractRandomDungeon pParameters, Vector2Int pPosition, bool pPaintPlaceable = true)
    {
        Vector2Int currentPosition = pPosition;

        HashSet<Vector2Int> floorPositions = new HashSet<Vector2Int>();

        for (int index = 0; index < pParameters.Iterations; index++)
        {
            HashSet<Vector2Int> path = ProceduralGenerationAlgorithms.SimpleRandomWalk(currentPosition, pParameters.WalkLength);
            
            floorPositions.UnionWith(path);

            if (pParameters.StartRandomlyEachIteration)
                currentPosition = floorPositions.ElementAt(Random.Range(0, floorPositions.Count));
        }
        
        if(pPaintPlaceable) _tilemapVisualizer.PaintPlaceableTiles(floorPositions);
        
        return floorPositions;
    }
}
