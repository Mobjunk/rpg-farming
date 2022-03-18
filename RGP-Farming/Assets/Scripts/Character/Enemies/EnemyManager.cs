using UnityEngine;

[RequireComponent(typeof(EnemyHealth), typeof(EnemyFollowManager))]
public class EnemyManager : Npc
{
    //[SerializeField] private EnemyData _enemyData;
    public EnemyData EnemyData => (EnemyData) NpcData;


    public override void Update()
    {
        CharacterAction?.Update();
    }

    public override void Awake()
    {
        base.Awake();

        //NpcData = _enemyData;

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
