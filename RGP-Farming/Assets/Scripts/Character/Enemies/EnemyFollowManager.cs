using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollowManager : MonoBehaviour
{
    private Player _player => Player.Instance();
    
    private PathFinderManager _pathFinderManager => PathFinderManager.Instance();
    private GridManager _gridManager => GridManager.Instance();
    
    private EnemyManager _enemyManager;

    private void Awake()
    {
        _enemyManager = GetComponent<EnemyManager>();
    }

    private void Update()
    {
        float playerDistance = Vector2.Distance(_player.transform.position, transform.position);
        if (playerDistance <= _enemyManager.EnemyData.attackRange)
        {
            
        } else if (playerDistance <= _enemyManager.EnemyData.followRange)
        {
            
        }
        /*float playerDistance = Vector2.Distance(_player.transform.position, transform.position);
        if (playerDistance <= _enemyManager.EnemyData.attackRange)
        {
            _path.Clear();
            _currentNode = 0;
            _currentDirection = Vector2.zero;
            _currentAssignment = Vector2.zero;
        } else if (playerDistance <= _enemyManager.EnemyData.followRange)
        {
            //Fills the path when there is no path...

            if (_path != null && _path.Count > 0)
            {
                Vector2 lastNode = _path[_path.Count - 1].WorldPosition;
                Vector3Int worldPos = _pathFinderManager.GetWorldToCell(_player.transform.position);
                if (lastNode.x != worldPos.x || lastNode.y != worldPos.y)
                {
                    _path = _pathFinderManager.FindPath(transform.position, _player.transform.position);
                    _currentNode = 0;
                    _currentDirection = Vector2.zero;
                    if (_path == null) return;
                }

                Node currentNode = _path[_currentNode];
                _currentAssignment = new Vector2(currentNode.WorldPosition.x + 0.5f, currentNode.WorldPosition.y);
                
                Vector3Int vector = _pathFinderManager.GetWorldToCell(transform.position);
                Node standingNode = _gridManager.GetNodeFromPosition(new Vector2(vector.x, vector.y));
                
                if (!currentNode.Equals(standingNode)) //!transform.position.Equals(_currentAssignment)
                {
                    int dirX = currentNode.GridX - standingNode.GridX, dirY = currentNode.GridY - standingNode.GridY;

                    if (_currentDirection.Equals(Vector2.zero)) _currentDirection = new Vector2(dirX, dirY);
                    
                    _enemyManager.CharacterMovementMananger.Move(_currentDirection);
                }
                else
                {
                    Debug.Log("????");
                    _currentDirection = Vector2.zero;
                    _currentNode++;
                }
            } else _path = _pathFinderManager.FindPath(transform.position, _player.transform.position);
        }
        else ClearPath();*/
    }

    private void OnDrawGizmos()
    {
        if (_enemyManager != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.GetChild(0).position, _enemyManager.EnemyData.followRange);
            
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.GetChild(0).position, _enemyManager.EnemyData.attackRange);
        }
    }
}
