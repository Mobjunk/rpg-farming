using UnityEngine;

public class PlayerAttack : Sounds
{
    public override void HandleSound(int pIntParameter = -1, GameObject pAttachedObject = null)
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/FX/Character/Attack/Attack");
    }

    public override string SoundName()
    {
        return "player_attack";
    }
}
