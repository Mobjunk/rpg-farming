using UnityEngine;

public class SwordSounds : Sounds
{
    public override void HandleSound(int pIntParameter = 0)
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/FX/Combat/Sword");
        Debug.Log("test sword");
    }

    public override string SoundName()
    {
        return "sword_swing";
    }
}