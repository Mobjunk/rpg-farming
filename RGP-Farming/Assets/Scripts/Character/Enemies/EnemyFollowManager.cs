using System;
using System.Collections;
using UnityEngine;

public class EnemyFollowManager : PathManager
{
    private Player _player => Player.Instance();

    private EnemyManager _enemyManager;

    [HideInInspector] public bool InAttackRange;

    [SerializeField] private Transform _centerPosition;
    
    public override void Awake()
    {
        base.Awake();
        _enemyManager = GetComponent<EnemyManager>();
    }

    private void Update()
    {
        if (_characterManager.CharacterAction is EnemyDeathAction) return;
        
        Vector3Int posA = _pathfinderManager.GetWorldToCell(_centerPosition.position),
        posB = _pathfinderManager.GetWorldToCell(_player.transform.position);
        
        float playerDistance = Vector2.Distance(new Vector2(posA.x, posA.y), new Vector2(posB.x, posB.y));
        if (playerDistance <= _enemyManager.EnemyData.followRange && !InAttackRange)
        {
            if (_path != null && _path.Length > 0)
            {
                Vector2 lastPosition = _path[_path.Length - 1];
                Vector2 currentPlayerPos = new Vector2(posB.x + 0.5f, posB.y + 0.5f);

                if (lastPosition.x != currentPlayerPos.x || lastPosition.y != currentPlayerPos.y)
                {
                    Vector2[] path = _pathfinderManager.FindPath(posA, posB);
                    OnPathFound(path, path != null && path.Length > 0);
                }
            }
            else
            {
                Vector2[] path = _pathfinderManager.FindPath(posA, posB);
                OnPathFound(path, path != null && path.Length > 0);
            }
        } else if (_path != null || InAttackRange) ResetPath();
    }

    public override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        if (_enemyManager != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(_centerPosition.position, _enemyManager.EnemyData.followRange);
            
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(_centerPosition.position, _enemyManager.EnemyData.attackRange);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Player player = other.GetComponent<Player>();
        if (player != null) InAttackRange = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        Player player = other.GetComponent<Player>();
        if (player != null) InAttackRange = false;
    }
}
