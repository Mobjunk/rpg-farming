using UnityEngine;

public class FishingManager : HarvestSkillManager
{
    private DialogueManager _dialogueManager => DialogueManager.Instance();
    
    private AbstractFishingData _abstractFishingData;

    private DrawFishingLine _drawFishingLine;
    
    private bool _fishOnTheHook;
    private float fishOnHookTimer;
    
    
    public FishingManager(CharacterManager pCharacterManager, AbstractFishingData pFishingData, DrawFishingLine pDrawFishingLine) : base(pCharacterManager)
    {
        _abstractFishingData = pFishingData;
        _drawFishingLine = pDrawFishingLine;
    }

    public override void Update()
    {
        if(!_fishOnTheHook) base.Update();
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
        return Random.Range(0, 100) >= 75;
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
}