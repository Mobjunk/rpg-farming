using UnityEngine;

public class FootstepSounds : Sounds
{
    public override void HandleSound(int pIntParameter = -1)
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/FX/Character/Walking/Walk");
        Debug.Log("test");
    }

    public override string SoundName()
    {
        return "footsteps";
    }
}