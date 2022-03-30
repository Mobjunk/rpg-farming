using UnityEngine;

public class MenuSelect : Sounds
{
    public override void HandleSound(int pIntParameter = -1, GameObject pAttachedObject = null)
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/FX/UI/Menu select");
    }

    public override string SoundName()
    {
        return "menu_select";
    }
}
