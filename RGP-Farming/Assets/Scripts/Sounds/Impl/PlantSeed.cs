using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantSeed : Sounds
{
    public override void HandleSound(int pIntParameter = -1, GameObject pAttachedObject = null)
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/FX/Skills/Farming/plant seed");
    }

    public override string SoundName()
    {
        return "PlantSeed";
    }
}