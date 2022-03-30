using UnityEngine;

public class SlimeHurt : Sounds
{
    public override void HandleSound(int pIntParameter = -1, GameObject pAttachedObject = null)
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/FX/Mobs/Slime/Hurt");
    }

    public override string SoundName()
    {
        return "slime_hurt";
    }
}
