using UnityEngine;

public class SwordSounds : Sounds
{
    public override void HandleSound(int pIntParameter = -1, GameObject pAttachedObject = null)
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/FX/Combat/Sword");
        Debug.Log("test sword");
    }

    public override string SoundName()
    {
        return "sword_swing";
    }
}