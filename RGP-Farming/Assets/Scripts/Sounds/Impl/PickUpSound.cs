using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpSound : Sounds
{
    public override void HandleSound(int pIntParameter = -1, GameObject pAttachedObject = null)
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/FX/General/PickUp");
    }

    public override string SoundName()
    {
        return "PickUpSound";
    }
}

