using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

public class DialogueManager : Singleton<DialogueManager>
{
    //FIFO
    private Player _player => Player.Instance();
    private Queue<string> _sentences;
    public bool _textShown;
    public bool _lettersShown;

    [Header("References")]
    [SerializeField] private TextMeshProUGUI _npcNameUI;
    [SerializeField] private TextMeshProUGUI _sentencesUI;
    [SerializeField] private GameObject _dialogueBox;
    [SerializeField] private GameObject _sentenceBox;
    [SerializeField] private GameObject _nameBox;
    [SerializeField] private Npc _activeNpc;
    
    [HideInInspector]
    public bool DialogueIsPlaying;
    [Header("In Characters per Second")]
    public float TextSpeed;
    public int maxCharacters;

    void Start()
    {
        _sentences = new Queue<string>();
    }   
    public void StartDialogue(Dialogue pDialogue, Npc pNpc = null)
    {
        _sentenceBox.SetActive(true);
        _nameBox.SetActive(true);
        DialogueIsPlaying = true;
        
        if (_player.InputEnabled)
        {
            _player.ToggleInput();
            _player.CharacterMovementMananger.ResetMovement();
        }
        _activeNpc = pNpc;
        _activeNpc?.CharacterMovementMananger.ResetMovement();
        
        _npcNameUI.text = pDialogue.Npc;
        //Clear last queue.
        _sentences.Clear();
        //Fill up the queue with new text.
        foreach (string sentence in pDialogue.sentences)
        {
            //Checks if not too long.
            if (sentence.Length > maxCharacters)
            {
                //Split the string up in substrings.
                string[] subSentences = Split(sentence, maxCharacters, true).ToArray();
                foreach (string subSentence in subSentences)
                {
                    _sentences.Enqueue(subSentence);
                }
            }
            else 
                _sentences.Enqueue(sentence);
        }
        DisplayNextLine();
    }  
    public void StartDialogue(string pSentence, string pName = "")
    {
        DialogueIsPlaying = true;
        if (_player.InputEnabled)
        {
            _player.ToggleInput();
            _player.CharacterMovementMananger.ResetMovement();
        }
        if (pSentence.Length > maxCharacters)
        {
            //Split the string up in substrings.
            string[] subSentences = Split(pSentence, maxCharacters, true).ToArray();
            foreach (string subSentence in subSentences)
            {
                _sentences.Enqueue(subSentence);
            }
        }
        else
        {
            _sentences.Enqueue(pSentence);
        }

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
        //Clear any running coroutines within the script.
        StopAllCoroutines();

        if (!_textShown)
        {
            string sentence = _sentences.Peek();
            StartCoroutine(WriteSentence(sentence));           
        }
        else 
        {
            string sentence = _sentences.Dequeue();
            _sentencesUI.text = sentence;
            _textShown = false;
        }
    }
    void EndDialogue()
    {
        _sentenceBox.SetActive(false);
        _nameBox.SetActive(false);
        Debug.Log("Einde Gesprek");
        DialogueIsPlaying = false;
        if(!_player.InputEnabled) _player.ToggleInput();
        if (_activeNpc != null)
        {
            _activeNpc.IsBusy = false;
            _activeNpc = null;
        }
    }

    //Slowly types text instead of instantly showing.
    IEnumerator WriteSentence (string pSentence)
    {
        _textShown = true;
        _sentencesUI.text = ""; 
        foreach (char letter in pSentence.ToCharArray())
        {
            _sentencesUI.text += letter;
            //Calculates time between characters if Textspeed is amount of characters a second.
            float speed = TextSpeed / (TextSpeed * TextSpeed);
            yield return new WaitForSeconds(speed);
        }
        _textShown = false;
        _sentences.Dequeue();
    }
    static IEnumerable<string> Split(string pOrgString, int pChunkSize, bool pWholeWords = true)
    {
        if (pWholeWords)
        {
            List<string> result = new List<string>();
            StringBuilder sb = new StringBuilder();

            if (pOrgString.Length > pChunkSize)
            {
                string[] newSplit = pOrgString.Split(' ');
                foreach (string str in newSplit)
                {
                    if (sb.Length != 0)
                        sb.Append(" ");

                    if (sb.Length + str.Length > pChunkSize)
                    {
                        result.Add(sb.ToString());
                        sb.Clear();
                    }

                    sb.Append(str);
                }

                result.Add(sb.ToString());
            }
            else
                result.Add(pOrgString);

            return result;
        }
        else
            return new List<string>(Regex.Split(pOrgString, @"(?<=\G.{" + pChunkSize + "})", RegexOptions.Singleline));
    }
}
