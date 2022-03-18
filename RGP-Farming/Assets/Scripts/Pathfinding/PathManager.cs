using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;

public abstract class PathManager : MonoBehaviour
{
    protected PathfinderManager _pathfinderManager => PathfinderManager.Instance();

    protected CharacterManager _characterManager;
    
    private Animator _animator;
    
    [SerializeField] private HeightBasedSorting _heightBasedSorting;

    [SerializeField] private Transform _transformToMove;
    
    /// <summary>
    /// The movement speed of the characther
    /// </summary>
    [SerializeField] private float _movementSpeed;

    /// <summary>
    /// Should the path be visualized in the scene view
    /// </summary>
    [SerializeField, Tooltip("Visualize the path in the scene view")] private bool _drawPath;
    
    [SerializeField] protected Vector2[] _path;
    protected int _waypoint;

    public virtual void Awake()
    {
        _animator = GetComponent<Animator>();
        _characterManager = GetComponent<CharacterManager>();
    }

    /// <summary>
    /// Handles resetting the current path
    /// </summary>
    protected void ResetPath()
    {
        _animator.SetBool("moving", false);
        StopCoroutine("FollowPath");
        _waypoint = 0;
        _path = null;
    }
    
    /// <summary>
    /// Handles setting the path when a path was succesfully found
    /// </summary>
    /// <param name="pPath"></param>
    /// <param name="pPathSuccesful"></param>
    protected void OnPathFound(Vector2[] pPath, bool pPathSuccesful)
    {
        if (pPathSuccesful)
        {
            _waypoint = 0;
            _path = pPath;
            StopCoroutine("FollowPath");
            StartCoroutine("FollowPath");
        }
        else _path = null;
    }

    /// <summary>
    /// Makes the character follow the path that was found with the a* pathfinding algorithm
    /// </summary>
    /// <returns></returns>
    protected IEnumerator FollowPath()
    {
        if(_path.Length <= 0) yield break;
        
        Vector2 currentWayPoint = _path[0];

        while (true)
        {
            if(_characterManager.CharacterAction is EnemyDeathAction) yield break;
            
            if ((Vector2)_transformToMove.position == currentWayPoint)
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

            Vector2 currentPosition = new Vector2(_transformToMove.position.x, _transformToMove.position.y);
            Vector2 moveDir = (currentWayPoint - currentPosition);

            int x = moveDir.x < 0 ? -1 : moveDir.x > 0 ? 1 : 0;
            int y = moveDir.y < 0 ? -1 : moveDir.y > 0 ? 1 : 0;
            
            
            _transformToMove.position = Vector2.MoveTowards (_transformToMove.position, currentWayPoint, _movementSpeed * Time.deltaTime);
            
            if (!moveDir.Equals(Vector2.zero) && _animator != null && _animator.enabled)
            {
                _animator.SetFloat("moveX", x);
                _animator.SetFloat("moveY", y);
                _animator.SetBool("moving", !moveDir.Equals(Vector2.zero));
            }
            
            _heightBasedSorting?.UpdateOrder();

            yield return null;
        }
    }
    
    /// <summary>
    /// Handles visualizing the path the character is taking towards its target
    /// </summary>
    public virtual void OnDrawGizmos()
    {
        if (_path != null && _drawPath)
        {
            for (int index = _waypoint; index < _path.Length; index++)
            {
                Gizmos.color = Color.black;
                Gizmos.DrawWireCube(_path[index], Vector2.one);

                if (index == _waypoint) Gizmos.DrawLine(_transformToMove.position, _path[index]);
                else Gizmos.DrawLine(_path[index - 1], _path[index]);
            }
        }
    }
}