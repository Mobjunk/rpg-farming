using System.Collections.Generic;
using UnityEngine;

public static class Directions
{
    /// <summary>
    /// List of all the cardinal directions
    /// </summary>
    public static List<Vector2Int> CardinalDirectionsList = new List<Vector2Int>()
    {
        new Vector2Int(0, 1), //Up
        new Vector2Int(1, 0), //Right
        new Vector2Int(0, -1), //Down
        new Vector2Int(-1, 0) //Left
    };
    
    public static List<Vector2Int> DiagonalDirectionsList = new List<Vector2Int>()
    {
        new Vector2Int(1, 1), //Up-right
        new Vector2Int(1, -1), //Right-down
        new Vector2Int(-1, -1), //Down-left
        new Vector2Int(-1, 1) //Left-up
    };

    public static List<Vector2Int> EightDirectionsList = new List<Vector2Int>()
    {
        new Vector2Int(0, 1), //Up
        new Vector2Int(1, 1), //Up-right
        new Vector2Int(1, 0), //Right
        new Vector2Int(1, -1), //Right-down
        new Vector2Int(0, -1), //Down
        new Vector2Int(-1, -1), //Down-left
        new Vector2Int(-1, 0), //Left
        new Vector2Int(-1, 1) //Left-up
    };

    /// <summary>
    /// Gets a random direction
    /// </summary>
    /// <returns></returns>
    public static Vector2Int GetRandomCardinalDirection()
    {
        return CardinalDirectionsList[Random.Range(0, CardinalDirectionsList.Count)];
    }
}