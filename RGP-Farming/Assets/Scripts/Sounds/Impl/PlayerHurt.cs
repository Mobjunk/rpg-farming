using UnityEngine;

public class PlayerHurt : Sounds
{
    public override void HandleSound(int pIntParameter = -1, GameObject pAttachedObject = null)
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/FX/Character/hurt/hurt");
    }

    public override string SoundName()
    {
        return "player_hurt";
    }
}
