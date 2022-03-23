using UnityEngine;

public class FishingSound : Sounds
{
    public override void HandleSound(int pIntParameter = -1, GameObject pAttachedObject = null)
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/FX/Skills/Fishing/throw line"); 
    }

    public override string SoundName()
    {
        return "fishing";
    }
}
