using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwingTool : Sounds
{
    public override void HandleSound(int pIntParameter = -1, GameObject pAttachedObject = null)
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/FX/Skills/General/Swing tool");
    }

    public override string SoundName()
    {
        return "SwingTool";
    }
}

