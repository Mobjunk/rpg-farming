using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorCloseSound : Sounds
{
    public override void HandleSound(int pIntParameter = -1, GameObject pAttachedObject = null)
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/FX/General/Door close");
    }

    public override string SoundName()
    {
        return "DoorCloseSound";
    }
}

