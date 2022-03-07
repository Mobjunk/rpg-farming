using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFishing : Singleton<PlayerFishing>
{
    private DialogueManager _dialogueManager => DialogueManager.Instance();
    
    [SerializeField] private Player _characterManager;

    private void Awake()
    {
        _characterManager = GetComponent<Player>();
    }

    public void StartFishing(AbstractFishingData pFishData)
    {
        if (!_characterManager.CharacterInventory.HasItem(pFishData.baitRequired))
        {
            //Debug.Log("Player does not have the required bait.");
            _dialogueManager.StartDialogue("You do not have any bait to start fishing.");
            return;
        }

        if (!_characterManager.CharacterInventory.ItemFitsInventory())
        {
            _dialogueManager.StartDialogue("You do not have enough inventory space.");
            return;
        }
        
        Utility.SetAnimator(_characterManager.CharacterStateManager.GetAnimator(), "fishing", true);
        StartCoroutine(RealStart(pFishData));
    }
    
    IEnumerator RealStart(AbstractFishingData pFishData)
    {
        yield return new WaitForSeconds(Utility.GetAnimationClipTime(_characterManager.CharacterStateManager.GetAnimator(), "fishing"));
        Utility.SetAnimator(_characterManager.CharacterStateManager.GetAnimator(), "fishing_idle", true);
        _characterManager.CharacterActionBubbles.SetBubbleAction(BubbleActions.WAITING);
        _characterManager.SetAction(new FishingManager(_characterManager, pFishData));
    }
}
