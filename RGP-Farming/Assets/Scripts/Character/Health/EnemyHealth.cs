using UnityEngine;

public class EnemyHealth : CharacterHealthManager
{
    private EnemyManager _enemyManager;
    private Animator _animator;
    
    public override void Awake()
    {
        base.Awake();
        _enemyManager = GetComponent<EnemyManager>();
        _animator = GetComponent<Animator>();
        if (_animator == null) _animator = GetComponentInChildren<Animator>();
    }

    public override void HandleDeath()
    {
        Debug.Log("HANDLE ENEMY DEATHS!");
        _enemyManager.SetAction(new EnemyDeathAction(_enemyManager, transform.parent.gameObject, _animator));
        //Destroy(gameObject);
    }
}
