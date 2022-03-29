using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireSound : Sounds
{
    public override void HandleSound(int pIntParameter = -1, GameObject pAttachedObject = null)
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/FX/Skills/FireMaking/Fire");
    }

    public override string SoundName()
    {
        return "FireSound";
    }
}
