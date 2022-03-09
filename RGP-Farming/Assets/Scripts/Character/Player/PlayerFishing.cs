using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFishing : Singleton<PlayerFishing>
{
    private DialogueManager _dialogueManager => DialogueManager.Instance();
    
    [SerializeField] private Player _characterManager;

    [SerializeField] private GameObject _bobberPrefab;

    [SerializeField] private Transform[] _rodLines;

    public GameObject BobberPrefab => _bobberPrefab;
    
    private void Awake()
    {
        _characterManager = GetComponent<Player>();
    }

    public void StartFishing(AbstractFishingData pFishData, Vector3 pTilePosition)
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
        
        _characterManager.SetAction(new FishingManager(_characterManager, pFishData, pTilePosition));
    }
    
    public Vector3 GetStartingPosition()
    {
        switch (_characterManager.CharacterStateManager.GetDirection())
        {
            case 1:
                return _rodLines[0].position;
            case 2:
                return _rodLines[1].position;
            case 3:
                return _rodLines[3].position;
            default: return _rodLines[2].position;
        }
    }
    
    public int GetSegmentLength()
    {
        switch (_characterManager.CharacterStateManager.GetDirection())
        {
            case 1:
                return 6;
            case 2:
                return 5;
            case 3:
                return 3;
            default: return 5;
        }
    }
}
