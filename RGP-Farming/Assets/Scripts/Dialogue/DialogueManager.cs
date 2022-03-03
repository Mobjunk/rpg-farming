using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : Singleton<DialogueManager>
{
    //FIFO
    private Queue<string> sentences;

    private void Start()
    {
        sentences = new Queue<string>();
    }

    public void StartDialogue (Dialogue pDialogue)
    {
        Debug.Log("Test gesprek met " + pDialogue.Npc);
        sentences.Clear();

        foreach (string sentence in pDialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }
    }
    public void DisplayNextLine()
    {
        if(sentences.Count == 0)
        {
            EndDialogue();
            return;
        }
        string sentence = sentences.Dequeue();

        StopAllCoroutines();
        StopCoroutine(WriteSentence(sentence));
        Debug.Log(sentence);
    }
    void EndDialogue()
    {
        Debug.Log("Einde Gesprek");
    }

    IEnumerator WriteSentence (string pSentence)
    {
        //Test = ""; 
        foreach (char letter in pSentence.ToCharArray())
        {
            //text += letter;
            yield return null;
        }
    }
}
