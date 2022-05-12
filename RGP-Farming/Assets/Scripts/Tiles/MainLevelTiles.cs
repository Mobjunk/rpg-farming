using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MainLevelTiles : Singleton<MainLevelTiles>
{
    [SerializeField] public Grid MainGrid;

    [SerializeField] public Tilemap HoverTilemap;

    [SerializeField] public Tilemap[] TilemapsToCheck;

    [SerializeField] public Tilemap[] AllTilemaps;

    [SerializeField] public Tilemap[] UnwalkableTilemaps;
    private void Awake()
    {
        /*_tilemapManager.MainGrid = _mainGrid;
        _tilemapManager.HoverTilemap = _hoverTilemap;
        _tilemapManager.TilemapsToCheck = _tilemapsToCheck;
        _tilemapManager.AllTilemaps = _allTilemaps;
        _tilemapManager.UnwalkableTilemaps = _unwalkableTilemaps;*/
    }
}
