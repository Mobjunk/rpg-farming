using UnityEngine;

public class CharacterHealthManager : HealthManager
{
    public override void HandleDeath()
    {
        Debug.Log("Handle character death...");
    }
}