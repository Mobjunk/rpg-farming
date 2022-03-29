using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiningInteraction : ObjectInteractionManager
{
    private DialogueManager _dialogueManager => DialogueManager.Instance();

    private HealthManager _healthManager;
    private ItemBarManager _itemBarManager => ItemBarManager.Instance();

    public void Awake()
    {
        _healthManager = GetComponent<HealthManager>();
    }

    public override void OnInteraction(CharacterManager pCharacterManager)
    {
        if (_itemBarManager.IsWearingCorrectTool(ToolType.PICKAXE))
        {
            if (pCharacterManager.CharacterAction is RockInteractionAction) return;
            
            pCharacterManager.SetAction(new RockInteractionAction(pCharacterManager, _healthManager, gameObject.name));
        }
        else _dialogueManager.StartDialogue("Maybe I should be using a different tool.");
    }
}
