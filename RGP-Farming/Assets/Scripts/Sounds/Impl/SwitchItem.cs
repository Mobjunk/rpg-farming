using UnityEngine;

public class SwitchItem : Sounds
{
    public override void HandleSound(int pIntParameter = -1, GameObject pAttachedObject = null)
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/FX/UI/Switch invetory");
    }

    public override string SoundName()
    {
        return "switch_item";
    }
}
