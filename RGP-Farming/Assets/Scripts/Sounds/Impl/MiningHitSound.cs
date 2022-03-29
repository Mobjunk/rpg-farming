using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiningHitSound : Sounds
{
    public override void HandleSound(int pIntParameter = -1, GameObject pAttachedObject = null)
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/FX/Skills/Mining/Stone hit");
    }

    public override string SoundName()
    {
        return "MiningHitSound";
    }
}
