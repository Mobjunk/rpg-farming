using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    private PathFinderManager _pathFinderManager => PathFinderManager.Instance();
    
    public float MovementSpeed;
    public Transform Target;
    private Vector2[] _path;
    private int _targetIndex;

    public void Test()
    {
        Vector3Int posA = _pathFinderManager.GetWorldToCell(transform.position);
        Vector3Int posB = _pathFinderManager.GetWorldToCell(Target.position);
        PathRequestManager.Instance().RequestPath(posA, posB, OnPathFound);
    }

    private void OnPathFound(Vector2[] pPath, bool pPathSuccesful)
    {
        if (pPathSuccesful)
        {
            _path = pPath;
            Debug.Log("_path: " + _path.Length);
            StopCoroutine("FollowPath");
            StartCoroutine("FollowPath");
        }
    }

    private IEnumerator FollowPath()
    {
        Vector2 currentWayPoint = _path[0];

        while (true)
        {
            if ((Vector2)transform.position == currentWayPoint)
            {
                _targetIndex++;
                if (_targetIndex >= _path.Length) yield break;

                currentWayPoint = _path[_targetIndex];
            }

            transform.position = Vector2.MoveTowards (transform.position, currentWayPoint, MovementSpeed * Time.deltaTime);
            yield return null;
        }
    }

    private void OnDrawGizmos()
    {
        if (_path != null)
        {
            for (int index = _targetIndex; index < _path.Length; index++)
            {
                Gizmos.color = Color.black;
                Gizmos.DrawWireCube(_path[index], Vector2.one);

                if (index == _targetIndex) Gizmos.DrawLine(transform.position, _path[index]);
                else Gizmos.DrawLine(_path[index - 1], _path[index]);
            }
        }
    }
}
