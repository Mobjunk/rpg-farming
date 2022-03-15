using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PathFinderManager : Singleton<PathFinderManager>
{
    private GridManager _grid;
    private PathRequestManager _pathRequestManager;

    private void Awake()
    {
        _grid = GetComponent<GridManager>();
        _pathRequestManager = GetComponent<PathRequestManager>();
    }

    public Vector3Int GetWorldToCell(Vector3 pPos)
    {
        return _grid.UnityGrid.WorldToCell(pPos);
    }
    
    public void FindPath(Vector3 pStartPos, Vector3 pTargetPos)
    {
        Vector3Int startPos = _grid.UnityGrid.WorldToCell(pStartPos);
        Vector3Int targetPos = _grid.UnityGrid.WorldToCell(pTargetPos);
        
        Node startNode = _grid.GetNodeFromPosition(new Vector2(startPos.x, startPos.y));
        Node targetNode = _grid.GetNodeFromPosition(new Vector2(targetPos.x, targetPos.y));

        if (startNode.Walkable && targetNode.Walkable)
        {
            List<Node> openSet = new List<Node>();
            HashSet<Node> closedSet = new HashSet<Node>();
        
            openSet.Add(startNode);

            while (openSet.Count > 0) {
                Node currentNode = openSet[0];
                for (int i = 1; i < openSet.Count; i ++) {
                    if (openSet[i].FCost < currentNode.FCost || openSet[i].FCost == currentNode.FCost) {
                        if (openSet[i].HCost < currentNode.HCost)
                            currentNode = openSet[i];
                    }
                }

                openSet.Remove(currentNode);
                closedSet.Add(currentNode);

                if (currentNode == targetNode)
                {
                    RetracePath(startNode,targetNode);
                    return;
                }

                foreach (Node neighbour in _grid.GetNeighbours(currentNode)) {
                    if (!neighbour.Walkable || closedSet.Contains(neighbour)) continue;

                    int newCostToNeighbour = currentNode.GCost + GetDistance(currentNode, neighbour);
                    if (newCostToNeighbour < neighbour.GCost || !openSet.Contains(neighbour)) {
                        neighbour.GCost = newCostToNeighbour;
                        neighbour.HCost = GetDistance(neighbour, targetNode);
                        neighbour.ParentNode = currentNode;

                        if (!openSet.Contains(neighbour))
                            openSet.Add(neighbour);
                    }
                }
            }
        }
    }

    private List<Node> RetracePath(Node startNode, Node endNode) {
        List<Node> path = new List<Node>();
        Node currentNode = endNode;

        while (currentNode != startNode) {
            path.Add(currentNode);
            currentNode = currentNode.ParentNode;
        }

        path.Reverse();
        
        return path;
    }

    private int GetDistance(Node pNodeA, Node pNodeB)
    {
        int dstX = Mathf.Abs(pNodeA.GridX - pNodeB.GridX);
        int dstY = Mathf.Abs(pNodeA.GridY - pNodeB.GridY);

        if (dstX > dstY) return 14 * dstY + 10 * (dstX - dstY);
        
        return 14 * dstX + 10 * (dstY - dstX);
    }
}