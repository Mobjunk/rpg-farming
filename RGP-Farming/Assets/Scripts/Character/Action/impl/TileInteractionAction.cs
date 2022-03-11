using UnityEngine;
using UnityEngine.Tilemaps;
using static Utility;

public class TileInteractionAction : CharacterAction
{
    private ItemBarManager _itemBarManager => ItemBarManager.Instance();
    
    private string _animationName;
    private Tilemap _tilemap;
    private Vector3Int _tileLocation;
    private TileBase _tile;
    private bool _updateDurability;
    private float _animationTime;
    private float _requiredTime;
    
    public TileInteractionAction(CharacterManager pCharacterManager, string pAnimationName, Tilemap pTileMap, Vector3Int pTileLocation, TileBase pTile, bool pUpdateDurability = false) : base(pCharacterManager)
    {
        _animationName = pAnimationName;
        _tilemap = pTileMap;
        _tileLocation = pTileLocation;
        _tile = pTile;
        _updateDurability = pUpdateDurability;
        _requiredTime = GetAnimationClipTime(CharacterManager.CharacterStateManager.GetAnimator(), _animationName);
        SetAnimator(pCharacterManager.CharacterStateManager.GetAnimator(), pAnimationName, true, true);
    }
    
    public TileInteractionAction(CharacterManager pCharacterManager, string pAnimationName, Tilemap pTileMap, Vector3Int pTileLocation, TileBase pTile, float pAnimationTime, bool pUpdateDurability = false) : base(pCharacterManager)
    {
        _animationName = pAnimationName;
        _tilemap = pTileMap;
        _tileLocation = pTileLocation;
        _tile = pTile;
        _updateDurability = pUpdateDurability;
        _requiredTime = pAnimationTime;
        SetAnimator(pCharacterManager.CharacterStateManager.GetAnimator(), pAnimationName, true, true);
    }

    public override void Update()
    {
        base.Update();

        _animationTime += Time.deltaTime;
        if (_animationName.Equals("watering") && _animationTime > (_requiredTime / 2))
            GetWateringPartical()?.SetActive(true);

        if (_animationTime > _requiredTime)
        {
            _tilemap.SetTile(_tileLocation, _tile);
            if (CharacterManager is Player player)
            {
                if (_updateDurability)
                    player.CharacterInventory.UpdateDurability(_itemBarManager.GetItemSelected(), -1);
                
                if(_animationName.Equals("watering"))
                    foreach(GameObject waterPartical in player.WaterPartical)
                        waterPartical.SetActive(false);
            }

            CharacterManager.SetAction(null);
        }
    }

    public override CharacterStates GetCharacterState()
    {
        return CharacterStates.NONE;
    }

    private GameObject GetWateringPartical()
    {
        switch (CharacterManager.CharacterStateManager.GetDirection())
        {
            case 0: //Down
                return ((Player)CharacterManager).WaterPartical[0];
            case 1: //Left
                return ((Player)CharacterManager).WaterPartical[2];
            case 2: //Right
                return ((Player)CharacterManager).WaterPartical[1];
            default: return null;
        }
    }
}