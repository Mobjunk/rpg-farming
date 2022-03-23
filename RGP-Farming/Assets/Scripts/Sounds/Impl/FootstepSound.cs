using UnityEngine;

public class FootstepSounds : Sounds
{
    public override void HandleSound(int pIntParameter = -1)
    {
       // FMODUnity.RuntimeManager.PlayOneShot("event:/FX/Character/Walking/Walk");
        FMOD.Studio.EventInstance instance = FMODUnity.RuntimeManager.CreateInstance("event:/FX/Character/Walking/Walk");
        instance.start();
        instance.setParameterByName("Surface", pIntParameter);
        instance.release();

        Debug.Log("test");
    }

    public override string SoundName()
    {
        return "footsteps";
    }
}