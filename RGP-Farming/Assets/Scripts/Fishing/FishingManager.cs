﻿using UnityEngine;

public class FishingManager : HarvestSkillManager
{
    private DialogueManager _dialogueManager => DialogueManager.Instance();

    private PlayerFishing _playerFishing => PlayerFishing.Instance();
    
    private AbstractFishingData _abstractFishingData;

    private Vector3 _tilePosition;
    
    private DrawFishingLine _drawFishingLine;
    
    private bool _fishOnTheHook;
    private bool _startedFishing = false;
    private float fishOnHookTimer;
    private float _animationTimePassed;
    
    
    public FishingManager(CharacterManager pCharacterManager, AbstractFishingData pFishingData, Vector3 pTilePosition) : base(pCharacterManager)
    {
        _abstractFishingData = pFishingData;
        _tilePosition = pTilePosition;
    }

    public override void Update()
    {
        if (!_startedFishing)
        {
            if(_animationTimePassed <= 0) Utility.SetAnimator(CharacterManager.CharacterStateManager.GetAnimator(), "fishing", true);
            _animationTimePassed += Time.deltaTime;
            //Debug.Log("_animationTimePassed: " + _animationTimePassed + ", " + Utility.GetAnimationClipTime(CharacterManager.CharacterStateManager.GetAnimator(), "fishing"));
            if (_animationTimePassed > Utility.GetAnimationClipTime(CharacterManager.CharacterStateManager.GetAnimator(), "fishing"))
            {
                Debug.Log("TESTER");
                GameObject bobber = GameObject.Instantiate(_playerFishing.BobberPrefab, _tilePosition, Quaternion.identity);

                _drawFishingLine = bobber.GetComponent<DrawFishingLine>();
        
                _drawFishingLine.Draw(_playerFishing.GetStartingPosition(), _playerFishing.GetSegmentLength());
                
                Utility.SetAnimator(CharacterManager.CharacterStateManager.GetAnimator(), "fishing_idle", true);
        
                CharacterManager.CharacterActionBubbles.SetBubbleAction(BubbleActions.WAITING);
                _startedFishing = true;
            }
        }
        else if(!_fishOnTheHook && _startedFishing) base.Update();
        else
        {
            fishOnHookTimer += Time.deltaTime;
            if (Input.GetMouseButtonDown(0))
            {
                if (CharacterManager is Player player)
                {
                    player.CharacterInventory.RemoveItem(_abstractFishingData.baitRequired);
                    player.CharacterInventory.AddItem(_abstractFishingData.fish);
                    _dialogueManager.StartDialogue($"You have caught a {_abstractFishingData.fish.itemName}.");
                }
                GameObject.Destroy(_drawFishingLine.gameObject);
                CharacterManager.SetAction(null);
            } else if (fishOnHookTimer >= 5f)
            {
                if (CharacterManager is Player player)
                {
                    player.CharacterInventory.RemoveItem(_abstractFishingData.baitRequired);
                    _dialogueManager.StartDialogue("The fish has gotten away.");
                }

                GameObject.Destroy(_drawFishingLine.gameObject);
                CharacterManager.SetAction(null);
            }
        }
    }

    public override CharacterStates GetCharacterState()
    {
        return CharacterStates.FISHING_IDLE;
    }

    public override float TimeRequired()
    {
        return 1f;
    }

    public override bool HasRequirements()
    {
        return true;
    }

    public override void ReceiveItem()
    {
        _fishOnTheHook = true;
        CharacterManager.CharacterActionBubbles.SetBubbleAction(BubbleActions.READY);
        _drawFishingLine.SetFishOn();
    }

    public override bool Successful()
    {
        return Random.Range(0, 100) >= 95;
    }

    public override AbstractItemData ItemToReceive()
    {
        return _abstractFishingData.fish;
    }

    public override int GetMaxFailures()
    {
        return 20;
    }

    public override void HandleFailure()
    {
        if (CharacterManager is Player player)
        {
            player.CharacterInventory.RemoveItem(_abstractFishingData.baitRequired);
            _dialogueManager.StartDialogue("The bait fell off your hook.");
        }
        GameObject.Destroy(_drawFishingLine.gameObject);
    }

    public override void OnStop()
    {
        base.OnStop();
        GameObject.Destroy(_drawFishingLine.gameObject);
    }
}