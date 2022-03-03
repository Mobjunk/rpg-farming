using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    // Trigger on 
    // Ranged inside the npc
    // Achieving certain goal
    // Completening a contract
    private DialogueManager _dialogueManager => DialogueManager.Instance();

    public Dialogue dialogue;
}
