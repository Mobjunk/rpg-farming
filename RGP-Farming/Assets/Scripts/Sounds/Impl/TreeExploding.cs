using UnityEngine;

public class TreeExploding : Sounds
{
    public override void HandleSound(int pIntParameter = -1, GameObject pAttachedObject = null)
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/");
    }

    public override string SoundName()
    {
        return "tree_explosion";
    }
}
