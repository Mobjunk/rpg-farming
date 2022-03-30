using UnityEngine;

public class HarvestCropsManager : HarvestSkillManager
{
    private Player _player;
    private Vector3Int _tileLocation;
    private GameObject _interactedObject;
    private AbstractItemData _receivedItem;
    private int _requiredAmount;
    
    public HarvestCropsManager(CharacterManager pCharacterManager, Vector3Int pTileLocation, GameObject pInteractedObject, AbstractItemData pReceivedItem, int pRequiredAmount) : base(pCharacterManager)
    {
        _player = (Player) pCharacterManager;
        _tileLocation = pTileLocation;
        _interactedObject = pInteractedObject;
        _receivedItem = pReceivedItem;
        _requiredAmount = pRequiredAmount;
        pCharacterManager.CharacterActionBubbles.SetBubbleAction(BubbleActions.CUSTOM, pReceivedItem);
    }

    public override CharacterStates GetCharacterState()
    {
        return CharacterStates.NONE;
    }

    public override float TimeRequired()
    {
        return 0.5f;
    }

    public override bool HasRequirements()
    {
        return true;
    }

    public override void ReceiveItem()
    {
        if (_requiredAmount <= 0)
        {
            Object.Destroy(_interactedObject);
            CharacterManager.SetAction(null);
            return;
        }
            
        SoundManager.Instance().ExecuteSound("PickUpSound");
        _requiredAmount--;
        _player.CharacterInventory.AddItem(ItemToReceive(), pShow:true);
        
        if (_requiredAmount <= 0)
        {
            CursorManager.Instance().SetDefaultCursor();
            CharacterPlaceObject.Instance().GetPlayerTileMap.SetTile(_tileLocation, null);
            Object.Destroy(_interactedObject);
            CharacterManager.SetAction(null);
        }
    }

    public override bool Successful()
    {
        return true;
    }

    public override AbstractItemData ItemToReceive()
    {
        return _receivedItem;
    }

    public override int GetMaxFailures()
    {
        return int.MaxValue;
    }

    public override void HandleFailure() { }
}
