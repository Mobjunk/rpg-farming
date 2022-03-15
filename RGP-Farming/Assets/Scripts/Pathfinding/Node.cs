using UnityEngine;

[System.Serializable]
public class Node
{
    public int GridX;
    public int GridY;
    
    public bool Walkable;
    public Vector2 WorldPosition;

    public int GCost;
    public int HCost;
    public int FCost => GCost + HCost;

    public Node ParentNode;

    public Node(bool pWalkable, Vector2 pWorldPosition, int pGridX, int pGridY)
    {
        Walkable = pWalkable;
        WorldPosition = pWorldPosition;
        GridX = pGridX;
        GridY = pGridY;
    }
}