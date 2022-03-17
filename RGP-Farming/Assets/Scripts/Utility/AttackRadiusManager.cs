using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackRadiusManager : MonoBehaviour
{
    private EnemyManager _enemyManager;
    private CircleCollider2D _circleCollider2D;

    private void Awake()
    {
        _enemyManager = GetComponentInParent<EnemyManager>();
        _circleCollider2D = GetComponent<CircleCollider2D>();
        _circleCollider2D.radius = _enemyManager.EnemyData.attackRange;
    }
}
