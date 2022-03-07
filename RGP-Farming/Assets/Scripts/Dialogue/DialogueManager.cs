using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;

public class DialogueManager : Singleton<DialogueManager>
{
    //FIFO
    private Queue<string> _sentences;

    [Header("References")]
    [SerializeField] private TextMeshProUGUI _npcNameUI;
    [SerializeField] private TextMeshProUGUI _sentencesUI;
    [SerializeField] private GameObject _dialogueBox;
    [SerializeField] private GameObject _sentenceBox;
    [SerializeField] private GameObject _nameBox;

    [HideInInspector]
    public bool DialogueIsPlaying;
    [Header("In Characters per Second")]
    public float TextSpeed;

    void Start()
    {
        _sentences = new Queue<string>();
    }
    //TODO %p , aanpassen naar de player naam. Dan komt overal de playernaam automatisch te staan.
    public void StartDialogue (Dialogue pDialogue)
    {
        _sentenceBox.SetActive(true);
        _nameBox.SetActive(true);
        DialogueIsPlaying = true;
        _npcNameUI.text = pDialogue.Npc;
        //Clear last queue.
        _sentences.Clear();
        //Fill up the queue with new text.
        foreach (string sentence in pDialogue.sentences)
        {
            _sentences.Enqueue(sentence);
        }
        DisplayNextLine();
    }
    //Start dialogue without a name.
    public void StartDialogue (string pSentence,string pName = "")
    {
        DialogueIsPlaying = true;
        _sentenceBox.SetActive(true);
        DisplayNextLine();
        if (!pName.Equals("")) { _npcNameUI.text = pName; _nameBox.SetActive(true); } 
    }
    public void DisplayNextLine()
    {
        if (_sentences.Count == 0)
        {
            EndDialogue();
            return;
        }
        string sentence = _sentences.Dequeue();

        //Clear any running coroutines within the script.
        StopAllCoroutines();
        //Start typing dialogue
        StartCoroutine(WriteSentence(sentence));      
    }
    void EndDialogue()
    {
        _sentenceBox.SetActive(false);
        _nameBox.SetActive(false);
        Debug.Log("Einde Gesprek");
        DialogueIsPlaying = false;
    }

    //Slowly types text instead of instantly showing.
    IEnumerator WriteSentence (string pSentence)
    {
        _sentencesUI.text = ""; 
        foreach (char letter in pSentence.ToCharArray())
        {
            _sentencesUI.text += letter;
            //Calculates time between characters if Textspeed is amount of characters a second.
            float speed = TextSpeed / (TextSpeed * TextSpeed);
            yield return new WaitForSeconds(speed);
        }
    }

    // TEST TEST TEST
    void RestrictCharacters(string pSentence)
    {
        //Check if the pSentence is longer then certain amount of characters.
        if(pSentence.Length > 100)
        {
            //Split up the sentence in multiple character arrays depending on the amount of characters.
            char[] part1 = pSentence.ToCharArray().Take(100).ToArray();
            char[] part2 = pSentence.ToCharArray().Skip(100).Take(100).ToArray();
            char[] part3 = pSentence.ToCharArray().Skip(200).Take(100).ToArray();
                     

            Debug.Log(part1);
            Debug.Log(part2);

            //Display the first array of characters.
            if (part2 != null)
            {
              
            }          
            if(part2 != null & Input.GetKeyDown(KeyCode.Return))
            {
                
            }
            //Display those sentences (Should display next line)(Uses Input)
            //Check if it is the end of the final array.
            // If yes : Go to the next sentence (Text Area)
        }
        return;
        
        // If not : Dispay the sentence as normal
    }
}
