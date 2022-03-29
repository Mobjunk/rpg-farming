using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PloughSound : Sounds
{
    public override void HandleSound(int pIntParameter = -1, GameObject pAttachedObject = null)
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/FX/Skills/Farming/plough");
    }

    public override string SoundName()
    {
        return "PloughSound";
    }
}

