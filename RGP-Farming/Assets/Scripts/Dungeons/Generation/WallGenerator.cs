using System.Collections.Generic;
using UnityEngine;

public static class WallGenerator
{
    public static void CreateWalls(HashSet<Vector2Int> pFloorPositions, TilemapVisualizer pTilemapVisualizer)
    {
        HashSet<Vector2Int> basicWallPositions = FindWallsInDirs(pFloorPositions, Directions.CardinalDirectionsList);
        HashSet<Vector2Int> cornerWallPositions = FindWallsInDirs(pFloorPositions, Directions.DiagonalDirectionsList);

        CreateBasicWalls(pTilemapVisualizer, basicWallPositions, pFloorPositions);
        CreateCornerWalls(pTilemapVisualizer, cornerWallPositions, pFloorPositions);
    }

    private static void CreateCornerWalls(TilemapVisualizer pTilemapVisualizer, HashSet<Vector2Int> pCornerWallPositions, HashSet<Vector2Int> pFloorPositions)
    {
        foreach (Vector2Int position in pCornerWallPositions)
        {
            string binaryType = string.Empty;
            
            foreach (Vector2Int dir in Directions.EightDirectionsList)
                binaryType += pFloorPositions.Contains(position + dir) ? "1" : "0";

            pTilemapVisualizer.PaintSingleCornerWall(position, binaryType);
        }
    }

    private static void CreateBasicWalls(TilemapVisualizer pTilemapVisualizer, HashSet<Vector2Int> pPositions, HashSet<Vector2Int> pFloorPositions)
    {
        foreach (Vector2Int position in pPositions)
        {
            string binaryType = string.Empty;
            
            foreach (Vector2Int dir in Directions.CardinalDirectionsList)
                binaryType += pFloorPositions.Contains(position + dir) ? "1" : "0";
            
            pTilemapVisualizer.PaintSingleBasicWall(position, binaryType);
        }
    }
    
    private static HashSet<Vector2Int> FindWallsInDirs(HashSet<Vector2Int> pFloorPositions, List<Vector2Int> pDirections)
    {
        HashSet<Vector2Int> wallPositions = new HashSet<Vector2Int>();

        foreach (Vector2Int position in pFloorPositions)
        {
            foreach (Vector2Int dir in pDirections)
            {
                Vector2Int neighbourPosition = position + dir;
                if (!pFloorPositions.Contains(neighbourPosition))
                    wallPositions.Add(neighbourPosition);
            }
        }
        
        return wallPositions;
    }
}
