using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollowManager : MonoBehaviour
{
    private Player _player => Player.Instance();
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