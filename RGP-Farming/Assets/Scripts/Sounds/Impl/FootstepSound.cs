using UnityEngine;

public class FootstepSounds : Sounds
{
    public override void HandleSound(int pIntParameter = -1)
    {
        Debug.Log("foot step sound with paramenter: " + pIntParameter);
    }

    public override string SoundName()
    {
        return "footsteps";
    }
}