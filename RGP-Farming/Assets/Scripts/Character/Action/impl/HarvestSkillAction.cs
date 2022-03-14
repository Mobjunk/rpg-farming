using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class HarvestSkillManager : CharacterAction
{
    private CharacterContractManager _characterContractManager;
    private int _failedAttempts;
    
    public HarvestSkillManager(CharacterManager pCharacterManager) : base(pCharacterManager)
    {
        pCharacterManager.CharacterMovementMananger.ResetMovement();
        _characterContractManager = pCharacterManager.GetComponent<CharacterContractManager>();
    }

    private float timePassedBy;
    
    public override void Update()
    {
        base.Update();
        
        if(!HasRequirements()) {
            Reset();
            return;
        }

        timePassedBy += Time.deltaTime;
        if (timePassedBy > TimeRequired())
        {
            if (Successful())
            {
                ReceiveItem();
                _characterContractManager.HandleContractDevelopment(ItemToReceive());
            }
            else
            {
                _failedAttempts++;
                //Debug.Log("_failedAttempts: " + _failedAttempts);
                if (_failedAttempts >= GetMaxFailures())
                {
                    HandleFailure();
                    CharacterManager.SetAction(null);
                }
            }
            timePassedBy = 0;
        }
    }

    public override void OnStart()
    {
        Debug.Log("Start action...");
        base.OnStart();
        
        if (!HasRequirements())
        {
            CharacterManager.SetAction(null);
            return;
        }
    }

    public override void OnStop()
    {
        base.OnStop();
        Reset();
    }

    public abstract float TimeRequired();
    public abstract bool HasRequirements();
    public abstract void ReceiveItem();
    public abstract bool Successful();
    public abstract AbstractItemData ItemToReceive();
    public abstract int GetMaxFailures();
    public abstract void HandleFailure();

    public virtual void Reset()
    {
        Debug.Log("Reset harvest skill manager...");
        CharacterManager.CharacterMovementMananger.ResetSkillingAnimations();
        CharacterManager.CharacterActionBubbles.SetBubbleAction(BubbleActions.NONE);
    }

}