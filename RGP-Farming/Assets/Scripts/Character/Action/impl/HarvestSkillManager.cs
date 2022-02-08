using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class HarvestSkillManager : CharacterAction
{
    public HarvestSkillManager(CharacterManager pCharacterManager) : base(pCharacterManager) { }

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
            if (Successful()) ReceiveItem();
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

    public virtual void Reset()
    {
        Debug.Log("Reset harvest skill manager...");
    }

}