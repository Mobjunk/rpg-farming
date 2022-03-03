using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialogue
{
    
    public string Npc;

    [TextArea(2,5)]
    public string[] sentences;

    
}
