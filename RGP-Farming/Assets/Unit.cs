using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    private PathfinderManager _pathfinderManager => PathfinderManager.Instance();

    [SerializeField] private Animator _animator;
    
    public float MovementSpeed;
    public Transform Target;
    private Vector2[] _path;
    private int _waypoint;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void Test()
    {
        Vector3Int posA = _pathfinderManager.GetWorldToCell(transform.position);
        Vector3Int posB = _pathfinderManager.GetWorldToCell(Target.position);
        
        //PathRequestManager.Instance().RequestPath(posA, posB, OnPathFound);
    }

    private void OnPathFound(Vector2[] pPath, bool pPathSuccesful)
    {
        if (pPathSuccesful)
        {
            _waypoint = 0;
            _path = pPath;
            Debug.Log("_path length: " + _path.Length);
            StopCoroutine("FollowPath");
            StartCoroutine("FollowPath");
        }
        else _path = null;
    }

    private IEnumerator FollowPath()
    {
        Vector2 currentWayPoint = _path[0];

        while (true)
        {
            if ((Vector2)transform.position == currentWayPoint)
            {
                _waypoint++;
                if (_waypoint >= _path.Length)
                {
                    _waypoint = 0;
                    _path = null;
                    yield break;
                }

                currentWayPoint = _path[_waypoint];
            }

            Vector2 currentPosition = new Vector2(transform.position.x, transform.position.y);
            Vector2 moveDir = (currentWayPoint - currentPosition);

            int x = moveDir.x < 0 ? -1 : moveDir.x > 0 ? 1 : 0;
            int y = moveDir.y < 0 ? -1 : moveDir.y > 0 ? 1 : 0;
            
            
            transform.position = Vector2.MoveTowards (transform.position, currentWayPoint, MovementSpeed * Time.deltaTime);

            if (!moveDir.Equals(Vector2.zero) && _animator != null && _animator.enabled)
            {
                _animator.SetFloat("moveX", x);
                _animator.SetFloat("moveY", y);
                _animator.SetBool("moving", !moveDir.Equals(Vector2.zero));
            }

            yield return null;
        }
    }

    private void OnDrawGizmos()
    {
        if (_path != null)
        {
            for (int index = _waypoint; index < _path.Length; index++)
            {
                Gizmos.color = Color.black;
                Gizmos.DrawWireCube(_path[index], Vector2.one);

                if (index == _waypoint) Gizmos.DrawLine(transform.position, _path[index]);
                else Gizmos.DrawLine(_path[index - 1], _path[index]);
            }
        }
    }
}
