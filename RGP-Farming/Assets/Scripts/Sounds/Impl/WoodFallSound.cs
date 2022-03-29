using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodFallSound : Sounds
{
    public override void HandleSound(int pIntParameter = -1, GameObject pAttachedObject = null)
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/FX/Skills/Woodcutting/fall");
    }

    public override string SoundName()
    {
        return "WoodFallSound";
    }
}
