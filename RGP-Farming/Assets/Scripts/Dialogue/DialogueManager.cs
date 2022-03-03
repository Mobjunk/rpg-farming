using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueManager : Singleton<DialogueManager>
{
    //FIFO
    private Queue<string> sentences;

    public TextMeshProUGUI NpcName;
    public TextMeshProUGUI Sentences;

    [HideInInspector]
    public bool DialogueIsPlaying;
    [Header("In Characters per Second")]
    public float TextSpeed;

    void Start()
    {
        sentences = new Queue<string>();
    }
    public void StartDialogue (Dialogue pDialogue)
    {
        DialogueIsPlaying = true;
        NpcName.text = pDialogue.Npc;
        //Clear last queue.
        sentences.Clear();
        //Fill up the queue with new text.
        foreach (string sentence in pDialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }
        DisplayNextLine();
    }
    public void DisplayNextLine()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }
        string sentence = sentences.Dequeue();

        //Clear any running coroutines within the script.
        StopAllCoroutines();
        //Start typing dialogue
        StartCoroutine(WriteSentence(sentence));
    }
    void EndDialogue()
    {
        Debug.Log("Einde Gesprek");
        DialogueIsPlaying = false;
    }

    //Slowly types text instead of instantly showing.
    IEnumerator WriteSentence (string pSentence)
    {
        Sentences.text = ""; 
        foreach (char letter in pSentence.ToCharArray())
        {
            Sentences.text += letter;
            //Calculates time between characters if Textspeed is amount of characters a second.
            float speed = TextSpeed / (TextSpeed * TextSpeed);
            yield return new WaitForSeconds(speed);
        }
    }
}
