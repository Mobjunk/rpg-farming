using FMOD;
using Debug = UnityEngine.Debug;

public class CharacterHealthManager : HealthManager
{
    public override void TakeDamage(int pDamage)
    {
        base.TakeDamage(pDamage);
        SoundManager.Instance().ExecuteSound("player_hurt");
    }

    public override void HandleDeath()
    {
        Debug.Log("Handle character death...");
    }
}