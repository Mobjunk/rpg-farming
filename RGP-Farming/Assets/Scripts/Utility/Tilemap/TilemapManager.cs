using System;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapManager : Singleton<TilemapManager>
{
    [SerializeField] private Grid _mainGrid;

    public Grid MainGrid => _mainGrid;

    [SerializeField] private Tilemap _hoverTilemap;

    public Tilemap HoverTilemap => _hoverTilemap;

    [SerializeField] private Tilemap[] _tilemapsToCheck;

    public Tilemap[] TilemapsToCheck => _tilemapsToCheck;

    [SerializeField] private Tilemap[] _allTilemaps;

    public Tilemap[] AllTilemaps => _allTilemaps;

    [SerializeField] private Tilemap[] _unwalkableTilemaps;

    public Tilemap[] UnwalkableTilemaps => _unwalkableTilemaps;
    
    private void Awake()
    {
        _mainGrid = GetComponent<Grid>();
    }

    public int GetTileType(Vector3 pCurrentPosition)
    {
        TileBase walkableTiles = _allTilemaps[5].GetTile(_mainGrid.WorldToCell(pCurrentPosition));
        if (walkableTiles != null)
        {
            //Debug.Log("walkableTiles.name: " + walkableTiles.name);
            switch (walkableTiles.name)
            {
                case "tiles_225":
                case "tiles_236":
                case "tiles_237":
                case "tiles_247":
                case "tiles_248":
                case "tiles_226":
                    return 2;
                default: return 0;
            }
        }
        TileBase grassTile = _allTilemaps[1].GetTile(_mainGrid.WorldToCell(pCurrentPosition));
        if (grassTile != null)
        {
            //Debug.Log("grassTile.name: " + grassTile.name);
            switch (grassTile.name)
            {
                case "GroundSummer_239":
                    return 2;
                default: return 1;
            }
        }
        
        TileBase dirtTile = _allTilemaps[0].GetTile(_mainGrid.WorldToCell(pCurrentPosition));
        if (dirtTile != null)
        {
            //Debug.Log("dirtTile.name: " + dirtTile.name);
            switch (dirtTile.name)
            {
                default: return 0;
            }
        }
        return -1;
    }

    public string GetFootstepName(int pTileType)
    {
        switch (pTileType)
        {
            case 0:
                return "Gravel";
            case 1:
                return "Grass";
            case 2:
                return "Wood";
            case 3:
                return "Tiles";
            case 4:
                return "Metal";
            case 5:
                return "Water";
        }
        return "DEFAULT";
    }
}
