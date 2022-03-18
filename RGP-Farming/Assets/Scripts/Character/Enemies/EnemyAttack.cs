using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    private Player _player => Player.Instance();
    
    private EnemyManager _enemyManager;
    private EnemyFollowManager _enemyFollowManager;
    private Animator _animator;
    private CharacterHealthManager _characterHealthManager;

    [SerializeField] private float _attackDelay;
    
    private void Awake()
    {
        _enemyManager = GetComponent<EnemyManager>();
        _enemyFollowManager = GetComponent<EnemyFollowManager>();
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        _characterHealthManager = _player.GetComponent<CharacterHealthManager>();
    }

    private void Update()
    {
        if (_enemyFollowManager.InAttackRange && _attackDelay <= 0)
        {
            Utility.SetAnimator(_animator, "attacking", true, true);
            _characterHealthManager.TakeDamage(_enemyManager.EnemyData.attackDamage);
            _attackDelay = _enemyManager.EnemyData.attackSpeed;
        } 
        
        if (_attackDelay > 0) _attackDelay -= Time.deltaTime;
    }
}
