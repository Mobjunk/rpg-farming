using UnityEngine;

public class JeroenInteraction : NpcInteraction
{
    private DialogueManager _dialogueManager => DialogueManager.Instance();
    
    [SerializeField] private Dialogue _jeroenDialogue;
    
    public override void HandleOthers()
    {
        Npc.IsBusy = true;
        _dialogueManager.StartDialogue(_jeroenDialogue, Npc);
    }
}
