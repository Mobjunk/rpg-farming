using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterPlantSound : Sounds
{
    public override void HandleSound(int pIntParameter = -1, GameObject pAttachedObject = null)
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/FX/Skills/Farming/water");
    }

    public override string SoundName()
    {
        return "WaterPlantSound";
    }
}