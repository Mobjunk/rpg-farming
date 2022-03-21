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
        TileBase grassTile = _allTilemaps[1].GetTile(_mainGrid.WorldToCell(pCurrentPosition));
        if (grassTile != null) return 1;
        
        TileBase dirtTile = _allTilemaps[0].GetTile(_mainGrid.WorldToCell(pCurrentPosition));
        if (dirtTile != null) return 0;
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
        }
        return "DEFAULT";
    }
}
