using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;

public abstract class PathManager : MonoBehaviour
{
    protected PathfinderManager _pathfinderManager => PathfinderManager.Instance();

    public PathfinderManager PathfinderManager => _pathfinderManager;

    protected CharacterManager _characterManager;
    
    private Animator _animator;
    
    private HeightBasedSorting _heightBasedSorting;
    
    /// <summary>
    /// The movement speed of the characther
    /// </summary>
    [SerializeField] private float _movementSpeed;

    /// <summary>
    /// Should the path be visualized in the scene view
    /// </summary>
    [SerializeField, Tooltip("Visualize the path in the scene view")] private bool _drawPath;
    
    [SerializeField] protected Vector2[] _path;

    public Vector2[] Path => _path;
    
    protected int _waypoint;

    public virtual void Awake()
    {
        _animator = GetComponent<Animator>();
        if (_animator == null) _animator = GetComponentInChildren<Animator>();
        _characterManager = GetComponent<CharacterManager>();
        _heightBasedSorting = GetComponent<HeightBasedSorting>();
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
    public void OnPathFound(Vector2[] pPath, bool pPathSuccesful)
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
            
            if ((Vector2)transform.position == currentWayPoint)
            {
                _waypoint++;
                if (_waypoint >= _path.Length)
                {
                    _animator.SetBool("moving", false);
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
            
            
            transform.position = Vector2.MoveTowards (transform.position, currentWayPoint, _movementSpeed * Time.deltaTime);

            if (!moveDir.Equals(Vector2.zero) && _animator != null && _animator.enabled)
            {
                if (_characterManager.CharacterStateManager.GetCharacterState().Equals(CharacterStates.WALKING_3) || _characterManager.CharacterStateManager.GetCharacterState().Equals(CharacterStates.WALKING_7) || _characterManager.CharacterStateManager.GetCharacterState().Equals(CharacterStates.CARRY_3) || _characterManager.CharacterStateManager.GetCharacterState().Equals(CharacterStates.CARRY_7))
                    SoundManager.Instance().ExecuteSound("footsteps", TilemapManager.Instance().GetTileType(transform.position), gameObject);

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

                if (index == _waypoint) Gizmos.DrawLine(transform.position, _path[index]);
                else Gizmos.DrawLine(_path[index - 1], _path[index]);
            }
        }
    }
}