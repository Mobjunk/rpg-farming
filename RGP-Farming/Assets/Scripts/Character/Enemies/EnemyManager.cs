using UnityEngine;

[RequireComponent(typeof(EnemyHealth), typeof(EnemyFollowManager))]
public class EnemyManager : Npc
{
    [SerializeField] private EnemyData _enemyData;
    public EnemyData EnemyData => _enemyData;

    public override void Awake()
    {
        base.Awake();
        
        //SetAction(new RandomMovementAction(this));
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        /*Debug.Log("name[enter]: " + other.name);
        CharacterHealthManager characterHealthManager = other.GetComponent<CharacterHealthManager>();
        if (characterHealthManager != null)
        {
            characterHealthManager.TakeDamage(1);
        }*/
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        /*Debug.Log("name[stay]: " + other.name);
        CharacterHealthManager characterHealthManager = other.GetComponent<CharacterHealthManager>();
        if (characterHealthManager != null)
        {
            characterHealthManager.TakeDamage(1);
        }*/
    }
}
