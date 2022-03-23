using UnityEngine;

public class FootstepSounds : Sounds
{
    public override void HandleSound(int pIntParameter = -1, GameObject pAttachedObject = null)
    {
        if (pAttachedObject != null)
        {
            Debug.Log("pAttachedObject name: " + pAttachedObject.name);
        }
        FMOD.Studio.EventInstance instance = FMODUnity.RuntimeManager.CreateInstance("event:/FX/Character/Walking/Walk");
        instance.start();
        instance.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(pAttachedObject.transform));
        instance.setParameterByName("Surface", pIntParameter);
        instance.release();
    }

    public override string SoundName()
    {
        return "footsteps";
    }
}