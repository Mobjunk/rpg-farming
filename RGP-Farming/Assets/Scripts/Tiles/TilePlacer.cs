using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;
using static Utility;

public class TilePlacer : Singleton<TilePlacer>
{
    private TileManager _tileManager => TileManager.Instance();
    private TilemapManager _tilemapManager => TilemapManager.Instance();
    private ItemBarManager _itemBarManager => ItemBarManager.Instance();
    private Player _player => Player.Instance();
    private TileHover _tileHover => TileHover.Instance();

    private Grid Grid => _tilemapManager.MainGrid;

    private Tilemap PlayerDirtTiles => _tilemapManager.TilemapsToCheck[3];

    private Vector3Int _location;
    
    private Vector3 _mp;

    private void Update()
    {
        _mp = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        _location = PlayerDirtTiles.WorldToCell(_mp);
        if (Input.GetMouseButtonDown(0) && !CursorManager.Instance().IsPointerOverUIElement() && CanInteractWithTile(Grid, _location, _player.TileChecker))
        {
            PlaceWaterTile();
            PlaceDirtTile();
            RemoveDirtTile();
        } 
    }
    public void PlaceDirtTile()
    {
        if (PlayerDirtTiles.GetTile(PlayerDirtTiles.WorldToCell(_mp)) == null && _itemBarManager.IsWearingCorrectTool(ToolType.HOE) && _tileHover.CanInteractNow)
            _player.SetAction(new TileInteractionAction(_player, "hoe", PlayerDirtTiles, _location, _tileManager.DryFarmTile));
    }

    public void RemoveDirtTile()
    {
        if ((PlayerDirtTiles.GetTile(PlayerDirtTiles.WorldToCell(_mp)) == _tileManager.DryFarmTile || PlayerDirtTiles.GetTile(PlayerDirtTiles.WorldToCell(_mp)) == _tileManager.WateredFarmTile) && _itemBarManager.IsWearingCorrectTool(ToolType.PICKAXE) && _player.CharacterPlaceObject.CurrentGameObjectHoverd == null && _tileHover.CanInteractNow)
            _player.SetAction(new TileInteractionAction(_player, "pickaxe_swing", PlayerDirtTiles, _location, null));
    }
    
    public void PlaceWaterTile()
    {
        if (PlayerDirtTiles.GetTile(PlayerDirtTiles.WorldToCell(_mp)) == _tileManager.DryFarmTile && _itemBarManager.IsWearingCorrectTool(ToolType.WATERING_CAN))
        {
            if (_player.CharacterPlaceObject.CurrentGameObjectHoverd == null) return;

            //Checks if the crops the player is clicking is finished growing
            CropsGrowManager cropsGrowManager = _player.CharacterPlaceObject.CurrentGameObjectHoverd.GetComponent<CropsGrowManager>();
            if (cropsGrowManager != null && cropsGrowManager.ReadyToHarvest) return;

            //Checks if the crops you are hovering is in the interactable list
            InteractionManager interactionManager = _player.CharacterPlaceObject.CurrentGameObjectHoverd.GetComponent<InteractionManager>();
            if (interactionManager != null && _player.CharacterInventory.Items[_itemBarManager.SelectedSlot].Durability > 0)
                _player.SetAction(new TileInteractionAction(_player, "watering", PlayerDirtTiles, _location, _tileManager.WateredFarmTile, true));
        }
    }
    public bool CheckTileUnderObject(Vector3 pPosition, TileType pTileType)
    {
        Tile checkTile = _tileManager.WateredFarmTile;
        if (pTileType == TileType.DIRT)
            checkTile = _tileManager.DryFarmTile;
        
        return PlayerDirtTiles.GetTile(Grid.WorldToCell(pPosition)) == checkTile;
    }
    
    public void UpdateTile(GameObject pCrop, TileType pTileType)
    {
        Tile setTile = _tileManager.DryFarmTile;
        Tile checkTile = _tileManager.WateredFarmTile;
        if (pTileType == TileType.DIRT)
        {
            setTile = _tileManager.WateredFarmTile;
            checkTile = _tileManager.DryFarmTile;
        }

        //Checks position on the grid
        Vector3Int pos = Grid.WorldToCell(pCrop.transform.position);

        if (PlayerDirtTiles.GetTile(pos) == checkTile)
            PlayerDirtTiles.SetTile(pos, setTile);
    }

    IEnumerator SetTile(string pAnimationName, Vector3Int pLocation, TileBase pTile, bool pUpdateDurability = false)
    {
        SetAnimator(_player.CharacterStateManager.GetAnimator(), pAnimationName, true, true);
        yield return new WaitForSeconds(GetAnimationClipTime(_player.CharacterStateManager.GetAnimator(), pAnimationName));
        PlayerDirtTiles.SetTile(pLocation, pTile);
        if(pUpdateDurability) _player.CharacterInventory.UpdateDurability(_itemBarManager.GetItemSelected(), -1);
    }
}
public enum TileType
{
    DIRT,
    WATER
}