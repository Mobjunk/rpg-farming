using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestOpenSound : Sounds
{
    public override void HandleSound(int pIntParameter = -1, GameObject pAttachedObject = null)
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/FX/General/Chest open");
    }

    public override string SoundName()
    {
        return "ChestOpenSound";
    }
}

