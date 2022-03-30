using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishFloat : Sounds
{
    public override void HandleSound(int pIntParameter = -1, GameObject pAttachedObject = null)
    {
     FMODUnity.RuntimeManager.PlayOneShot("event:/FX/Skills/Fishing/hit water"); 
    }

    public override string SoundName()
    {
        return "FishFloat";
    }
}
