using UnityEngine;

public class EnemyHealth : CharacterHealthManager
{
    public override void HandleDeath()
    {
        Debug.Log("HANDLE ENEMY DEATHS!");
        Destroy(gameObject);
    }
}
