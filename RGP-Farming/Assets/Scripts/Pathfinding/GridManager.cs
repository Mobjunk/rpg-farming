using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GridManager : Singleton<GridManager>
{
    [SerializeField] private bool _drawGizmos;
    [SerializeField] private Grid _unityGrid;
    public Grid UnityGrid => _unityGrid;
    
    [SerializeField] private Transform _playerTransform;

    [SerializeField] private Tilemap[] _allTilemaps;
    
    [SerializeField] private Tilemap[] _unwalkableTilemaps;
    
    public Vector2 GridWorldSize;

    private Node[,] _gridArray;

    private int _gridSizeX;
    private int _gridSizeY;
    
    private void Awake()
    {
        //Grabs the biggest x,y combi for the grid size
        int biggestX = 0, biggestY = 0;
        foreach (Tilemap tilemap in _allTilemaps)
        {
            if (tilemap.cellBounds.size.x > biggestX) biggestX = tilemap.cellBounds.size.x;
            if (tilemap.cellBounds.size.y > biggestY) biggestY = tilemap.cellBounds.size.y;
        }

        GridWorldSize = new Vector2(biggestX, biggestY);
        
        CreateGrid(biggestX, biggestY);
    }

    private void CreateGrid(int pX, int pY)
    {
        _gridSizeX = pX * 2;
        _gridSizeY = pY * 2;
        _gridArray = new Node[_gridSizeX, _gridSizeY];
        int gridX = 0;
        int gridY = 0;
        for (int x = -pX; x < pX; x++)
        {
            for (int y = -pY; y < pY; y++)
            {
                Vector3Int tilePos = new Vector3Int(x, y, 0);
                bool walkable = true;
                foreach (Tilemap tilemap in _unwalkableTilemaps)
                {
                    if (tilemap.HasTile(tilePos))
                    {
                        walkable = false;
                        break;
                    }
                }

                _gridArray[gridX, gridY] = new Node(walkable, new Vector2(x, y), gridX, gridY);
                gridY++;
            }
            gridX++;
            gridY = 0;
        }
    }

    
    public Node GetNodeFromPosition(Vector2 pWorldPos)
    {
        foreach (Node node in _gridArray)
            if (node.WorldPosition.Equals(pWorldPos)) return node;
        return null;
    }

    public void UpdateGrid(Vector2 pWorldPos, bool pWalkable = true)
    {
        Node currentNode = GetNodeFromPosition(pWorldPos);
        if (currentNode == null) return;
        
        _gridArray[currentNode.GridX, currentNode.GridY].Walkable = pWalkable;
    }

    public List<Node> GetNeighbours(Node pCurrentNode, bool pStopDiagonal = false)
    {
        List<Node> neighbours = new List<Node>();

        for (int x = -1; x <= 1; x++) {
            for (int y = -1; y <= 1; y++) {
                if (x == 0 && y == 0) continue;

                int checkX = pCurrentNode.GridX + x;
                int checkY = pCurrentNode.GridY + y;

                if (checkX >= 0 && checkX < _gridSizeX && checkY >= 0 && checkY < _gridSizeY) {
                    neighbours.Add(_gridArray[checkX,checkY]);
                }
            }
        }

        return neighbours;
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, GridWorldSize);
        if (_gridArray != null && _drawGizmos)
        {
            Vector3Int vector3Int = _unityGrid.WorldToCell(_playerTransform.position);
            Node playerNode = GetNodeFromPosition(new Vector2(vector3Int.x, vector3Int.y));
            foreach (Node node in _gridArray)
            {
                if (node == null) continue;

                Gizmos.color = node == playerNode ? Color.cyan : node.Walkable ? Color.green : Color.red;
                Gizmos.color = new Color(Gizmos.color.r, Gizmos.color.g, Gizmos.color.b, 0.15f);

                Gizmos.DrawCube(new Vector3(node.WorldPosition.x + 0.5f, node.WorldPosition.y + 0.5f), Vector2.one);
            }
        }
    }
}