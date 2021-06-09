using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilePlacer : MonoBehaviour
{
    public Tilemap tilesGrass;
    public Tilemap tilesDirt;
    public Tile dirtTile;
    public Tile wateredDirtTile;

    private Vector3Int location;
    private Vector3 mp;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            mp = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //GetTileWorldPosition();
            PlaceWaterTile();
            PlaceDirtTile();
        } 
    }
    public void PlaceDirtTile()
    {
        if (tilesDirt.GetTile(tilesDirt.WorldToCell(mp)) == null)
        {
            location = tilesGrass.WorldToCell(mp);

            tilesDirt.SetTile(location, dirtTile);
        }
    }
    public void PlaceWaterTile()
    {
        if (tilesDirt.GetTile(tilesDirt.WorldToCell(mp)) == dirtTile)
        {
            location = tilesGrass.WorldToCell(mp);
            tilesDirt.SetTile(location, wateredDirtTile);
        }
       
    }
    public void GetTileWorldPosition()
    {
        GridLayout gridLayout = transform.parent.GetComponent<GridLayout>();
        Vector3Int cellPosition = gridLayout.WorldToCell(mp);
        //transform.position = gridLayout.CellToWorld(cellPosition);
        Debug.Log(gridLayout.CellToWorld(cellPosition));
    }
    public void CheckDirtTile()
    {
       // tiles.GetTile(location);
    }
}
