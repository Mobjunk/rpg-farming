using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilePlacer : Singleton<TilePlacer>
{
    private ItemBarManager itemBarManager => ItemBarManager.Instance();

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
        if (Input.GetMouseButtonDown(0) && !CursorManager.Instance().IsPointerOverUIElement() && Utility.CanInteractWithTile(grid, location, Player.Instance().TileChecker))
        {
            PlaceWaterTile();
            PlaceDirtTile();
        } 
    }
    public void PlaceDirtTile()
    {
        if (tilesDirt.GetTile(tilesDirt.WorldToCell(mp)) == null && itemBarManager.IsWearingCorrectTool(ToolType.HOE))
            tilesDirt.SetTile(location, dirtTile);
    }
    public void PlaceWaterTile()
    {
        //TODO: Add a check to see if the crop is finished growing
        if (tilesDirt.GetTile(tilesDirt.WorldToCell(mp)) == dirtTile && itemBarManager.IsWearingCorrectTool(ToolType.WATERING_CAN))
            tilesDirt.SetTile(location, wateredDirtTile);
    }
    public bool CheckTileUnderObject(GameObject crop, TileType tileType)
    {
        Tile checkTile = wateredDirtTile;
        if (tileType == TileType.DIRT)
            checkTile = dirtTile;

        return tilesDirt.GetTile(grid.WorldToCell(crop.transform.position)) == checkTile;
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
}
public enum TileType
{
    DIRT,
    WATER,
}