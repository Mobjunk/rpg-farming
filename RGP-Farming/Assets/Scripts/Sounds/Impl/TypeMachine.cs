using UnityEngine;

public class TypeMachine : Sounds
{
    public override void HandleSound(int pIntParameter = -1, GameObject pAttachedObject = null)
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/FX/UI/typeing");
    }

    public override string SoundName()
    {
        return "type_machine";
    }
}
