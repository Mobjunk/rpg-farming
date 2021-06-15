using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilePlacer : Singleton<TilePlacer>
{
    private ItemBarManager itemBarManager => ItemBarManager.Instance();
    private Player player => Player.Instance();

    public Tilemap tilesGrass;
    public Tilemap tilesDirt;
    public Tile dirtTile;
    public Tile wateredDirtTile;
    public Grid grid;

    private Vector3Int location;
    private Vector3 mp;

    private void Update()
    {
        mp = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //If Holding ....
        location = tilesGrass.WorldToCell(mp);
        if (Input.GetMouseButtonDown(0) && !CursorManager.Instance().IsPointerOverUIElement() && Utility.CanInteractWithTile(grid, location, player.TileChecker))
        {
            PlaceWaterTile();
            PlaceDirtTile();
            RemoveDirtTile();
        } 
    }
    public void PlaceDirtTile()
    {
        if (tilesDirt.GetTile(tilesDirt.WorldToCell(mp)) == null && itemBarManager.IsWearingCorrectTool(ToolType.HOE))
            tilesDirt.SetTile(location, dirtTile);
    }

    public void RemoveDirtTile()
    {
        if (tilesDirt.GetTile(tilesDirt.WorldToCell(mp)) == dirtTile && itemBarManager.IsWearingCorrectTool(ToolType.PICKAXE) && player.CharacterPlaceObject.CurrentGameObjectHoverd == null)
            tilesDirt.SetTile(location, null);
    }
    
    public void PlaceWaterTile()
    {
        if (tilesDirt.GetTile(tilesDirt.WorldToCell(mp)) == dirtTile && itemBarManager.IsWearingCorrectTool(ToolType.WATERING_CAN))
        {
            if (player.CharacterPlaceObject.CurrentGameObjectHoverd == null) return;

            //Checks if the crops the player is clicking is finished growing
            CropsCycle cropsCycle = player.CharacterPlaceObject.CurrentGameObjectHoverd.GetComponent<CropsCycle>();
            if (cropsCycle != null && cropsCycle.HasFinishedGrowing()) return;

            //Checks if the crops you are hovering is in the interactable list
            InteractionManager interactionManager = player.CharacterPlaceObject.CurrentGameObjectHoverd.GetComponent<InteractionManager>();
            if(player.CharacterInteractionManager.GetInteractables().Contains(interactionManager))
                tilesDirt.SetTile(location, wateredDirtTile);
        }
    }
    public bool CheckTileUnderObject(Vector3 position, TileType tileType)
    {
        Tile checkTile = wateredDirtTile;
        if (tileType == TileType.DIRT)
            checkTile = dirtTile;
        
        return tilesDirt.GetTile(grid.WorldToCell(position)) == checkTile;
    }
    
    public void UpdateTile(GameObject crop, TileType tileType)
    {
        Tile setTile = dirtTile;
        Tile checkTile = wateredDirtTile;
        if (tileType == TileType.DIRT)
        {
            setTile = wateredDirtTile;
            checkTile = dirtTile;
        }

        //Checks position on the grid
        Vector3Int pos = grid.WorldToCell(crop.transform.position);

        if (tilesDirt.GetTile(pos) == checkTile)
            tilesDirt.SetTile(pos, setTile);
    }

    public Vector3Int GetTilePosition(Vector3 mousePosition)
    {
        return tilesDirt.WorldToCell(mp);
    }
}
public enum TileType
{
    DIRT,
    WATER
}