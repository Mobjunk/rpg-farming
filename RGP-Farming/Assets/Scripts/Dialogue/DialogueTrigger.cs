using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueTrigger : MonoBehaviour
{
    // Trigger on 
    // Ranged inside the npc
    // Achieving certain goal
    // Completening a contract
    private DialogueManager _dialogueManager => DialogueManager.Instance();

    public Dialogue dialogue;

    public bool TestTrigger;
    private void Update()
    {
        if (TestTrigger) { TriggerDialogue(); TestTrigger = false; }

        if (Input.GetKeyDown(KeyCode.Space)) _dialogueManager.DisplayNextLine();
    }
    public void TriggerDialogue()
    {
        //Druk een knop in range van een character.
        _dialogueManager.StartDialogue(dialogue);
    }
}
