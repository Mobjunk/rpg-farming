using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupItem : Sounds
{
    public override void HandleSound(int pIntParameter = -1, GameObject pAttachedObject = null)
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/FX/Skills/General/Collect item");
    }

    public override string SoundName()
    {
        return "PickupItem";
    }
}

