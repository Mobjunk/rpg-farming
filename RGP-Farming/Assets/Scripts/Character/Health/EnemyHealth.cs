using UnityEngine;

public class EnemyHealth : HealthManager
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

    public override void TakeDamage(int pDamage)
    {
        base.TakeDamage(pDamage);
        SoundManager.Instance().ExecuteSound("slime_hurt");
    }

    public override void HandleDeath()
    {
        Debug.Log($"HANDLE ENEMY DEATHS {gameObject.name} {transform.parent.name}!");
        _enemyManager.SetAction(new EnemyDeathAction(_enemyManager, transform.gameObject, _animator));
        //Destroy(gameObject);
    }
}
