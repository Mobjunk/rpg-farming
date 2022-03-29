using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodCutSound : Sounds
{
    public override void HandleSound(int pIntParameter = -1, GameObject pAttachedObject = null)
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/FX/Skills/Woodcutting/cut");
    }

    public override string SoundName()
    {
        return "WoodCutSound";
    }
}
