using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;
using static Utility;

public class TilePlacer : Singleton<TilePlacer>
{
    private ItemBarManager _itemBarManager => ItemBarManager.Instance();
    private Player _player => Player.Instance();

    public Tilemap TilesGrass;
    public Tilemap TilesDirt;
    public Tile DirtTile;
    public Tile WateredDirtTile;
    public Grid Grid;

    private Vector3Int _location;
    private Vector3 _mp;

    private void Update()
    {
        _mp = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //If Holding ....
        _location = TilesGrass.WorldToCell(_mp);
        if (Input.GetMouseButtonDown(0) && !CursorManager.Instance().IsPointerOverUIElement() && Utility.CanInteractWithTile(Grid, _location, _player.TileChecker))
        {
            PlaceWaterTile();
            PlaceDirtTile();
            RemoveDirtTile();
        } 
    }
    public void PlaceDirtTile()
    {
        if (TilesDirt.GetTile(TilesDirt.WorldToCell(_mp)) == null && _itemBarManager.IsWearingCorrectTool(ToolType.HOE))
            TilesDirt.SetTile(_location, DirtTile);
    }

    public void RemoveDirtTile()
    {
        if (TilesDirt.GetTile(TilesDirt.WorldToCell(_mp)) == DirtTile && _itemBarManager.IsWearingCorrectTool(ToolType.PICKAXE) && _player.CharacterPlaceObject.CurrentGameObjectHoverd == null)
            TilesDirt.SetTile(_location, null);
    }
    
    public void PlaceWaterTile()
    {
        if (TilesDirt.GetTile(TilesDirt.WorldToCell(_mp)) == DirtTile && _itemBarManager.IsWearingCorrectTool(ToolType.WATERING_CAN))
        {
            if (_player.CharacterPlaceObject.CurrentGameObjectHoverd == null) return;

            //Checks if the crops the player is clicking is finished growing
            CropsGrowManager cropsGrowManager = _player.CharacterPlaceObject.CurrentGameObjectHoverd.GetComponent<CropsGrowManager>();
            if (cropsGrowManager != null && cropsGrowManager.ReadyToHarvest) return;

            //Checks if the crops you are hovering is in the interactable list
            InteractionManager interactionManager = _player.CharacterPlaceObject.CurrentGameObjectHoverd.GetComponent<InteractionManager>();
            if (_player.CharacterInteractionManager.GetInteractables().Contains(interactionManager) && _player.CharacterInventory.Items[_itemBarManager.SelectedSlot].Durability > 0)
            {
                TilesDirt.SetTile(_location, WateredDirtTile);
                _player.CharacterInventory.UpdateDurability(_itemBarManager.GetItemSelected(), -1);
            }
        }
    }
    public bool CheckTileUnderObject(Vector3 pPosition, TileType pTileType)
    {
        Tile checkTile = WateredDirtTile;
        if (pTileType == TileType.DIRT)
            checkTile = DirtTile;
        
        return TilesDirt.GetTile(Grid.WorldToCell(pPosition)) == checkTile;
    }
    
    public void UpdateTile(GameObject pCrop, TileType pTileType)
    {
        Tile setTile = DirtTile;
        Tile checkTile = WateredDirtTile;
        if (pTileType == TileType.DIRT)
        {
            setTile = WateredDirtTile;
            checkTile = DirtTile;
        }

        //Checks position on the grid
        Vector3Int pos = Grid.WorldToCell(pCrop.transform.position);

        if (TilesDirt.GetTile(pos) == checkTile)
            TilesDirt.SetTile(pos, setTile);
    }
}
public enum TileType
{
    DIRT,
    WATER
}