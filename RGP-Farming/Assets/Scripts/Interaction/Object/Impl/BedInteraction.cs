using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using static Utility;

public class BedInteraction : InteractionManager
{
    TimeManager _timeManager => TimeManager.Instance();
    DialogueManager _dialogueManager => DialogueManager.Instance();

    public override void OnInteraction(CharacterManager pCharacterManager)
    {
        base.OnInteraction(pCharacterManager);
        
        if(_timeManager.CurrentGameTime.Hour > 18)
        {
            //Call bedtime
        }
        else
        {
            _dialogueManager.StartDialogue("You can only sleep during night time!");
        }
    }
    IEnumerator BedTime()
    {
        //Fade the screen out

        //Wait until its night time

        //Set the current time to 08:00 AM


        yield return new WaitForSeconds(1f);

        //Fade the screen in

        //Enable player actions

    }
}
