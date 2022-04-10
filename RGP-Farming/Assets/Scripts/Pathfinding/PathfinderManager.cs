using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PathfinderManager : Singleton<PathfinderManager>
{
    private GridManager _grid;

    private void Awake()
    {
        _grid = GetComponent<GridManager>();
    }

    public Vector3Int GetWorldToCell(Vector3 pPos)
    {
        return _grid.UnityGrid.WorldToCell(pPos);
    }
    
    public Vector2[] FindPath(Vector3 pStartPos, Vector3 pTargetPos)
    {
        Vector3Int startPos = _grid.UnityGrid.WorldToCell(pStartPos);
        Vector3Int targetPos = _grid.UnityGrid.WorldToCell(pTargetPos);
        
        Node startNode = _grid.GetNodeFromPosition(new Vector2(startPos.x, startPos.y));
        Node targetNode = _grid.GetNodeFromPosition(new Vector2(targetPos.x, targetPos.y));

        Vector2[] waypoints = new Vector2[0];
        bool wasSuccesful = false;
        
        if (startNode.Walkable && targetNode.Walkable)
        {
            List<Node> openSet = new List<Node>();
            HashSet<Node> closedSet = new HashSet<Node>();
        
            openSet.Add(startNode);

            while (openSet.Count > 0) {
                if (openSet.Count > 50) break;
                
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
                    wasSuccesful = true;
                    break;
                }

                List<Node> neighbours = _grid.GetNeighbours(currentNode);
                foreach (Node neighbour in neighbours) {
                    if (!neighbour.Walkable || closedSet.Contains(neighbour)) continue;
                    if (!CheckDiagonal(currentNode, neighbour, neighbours)) continue;

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

        //yield return null;
        
        if (wasSuccesful) waypoints = RetracePath(startNode, targetNode);
        else _grid.Waypoints = null;

        return waypoints;
        //_pathRequestManager.FinishedProcessingPath(waypoints, wasSuccesful);
    }
    
    private bool CheckDiagonal(Node currentNode, Node neighbour, List<Node> neighbours) {
        int deltaX = neighbour.GridX - currentNode.GridX;
        int deltaY = neighbour.GridY - currentNode.GridY;

        if(Math.Abs(deltaX) != 1 || Math.Abs(deltaY) != 1)
            return true;

        foreach (Node node in neighbours) {
            if(node == neighbour)
                continue;

            if(node.Walkable == false && (node.GridX == currentNode.GridX + deltaX || node.GridY == currentNode.GridY + deltaY))
                return false;
        }

        return true;
    }

    private Vector2[] RetracePath(Node startNode, Node endNode)
    {
        /*List<Node> path = new List<Node>();
        Node currentNode = endNode;
        while (currentNode != startNode)
        {
            path.Add(currentNode);
            currentNode = currentNode.ParentNode;
        }

        Vector2[] waypoints = SimplifyPath(path);
        Array.Reverse(waypoints);

        return waypoints;*/

        List<Vector2> waypoints = new List<Vector2>();
        Node currentNode = endNode;

        while (currentNode != startNode) {
            waypoints.Add(new Vector2(currentNode.WorldPosition.x + 0.5f, currentNode.WorldPosition.y + 0.5f));
            currentNode = currentNode.ParentNode;
        }

        waypoints.Reverse();

        _grid.Waypoints = waypoints.ToArray();
        
        return waypoints.ToArray();
    }

    private Vector2[] SimplifyPath(List<Node> pPath)
    {
        List<Vector2> waypoints = new List<Vector2>();
        Vector2 directionOld = Vector2.zero;
        for (int index = 0; index < pPath.Count; index++)
        {
            Vector2 directionNew = new Vector2(pPath[index - 1].GridX - pPath[index].GridX, pPath[index - 1].GridY - pPath[index].GridY);
            if (directionNew != directionOld)
            {
                waypoints.Add(new Vector2(pPath[index].WorldPosition.x + 0.5f, pPath[index].WorldPosition.y + 0.5f));
            }

            directionOld = directionNew;
        }
        
        return waypoints.ToArray();
    }

    private int GetDistance(Node pNodeA, Node pNodeB)
    {
        int dstX = Mathf.Abs(pNodeA.GridX - pNodeB.GridX);
        int dstY = Mathf.Abs(pNodeA.GridY - pNodeB.GridY);

        if (dstX > dstY) return 14 * dstY + 10 * (dstX - dstY);
        
        return 14 * dstX + 10 * (dstY - dstX);
    }
}