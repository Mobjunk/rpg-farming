using UnityEngine;

public class SlimeDeath : Sounds
{
    public override void HandleSound(int pIntParameter = -1, GameObject pAttachedObject = null)
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/FX/Mobs/Slime/Dead");
    }

    public override string SoundName()
    {
        return "slime_death";
    }
}
