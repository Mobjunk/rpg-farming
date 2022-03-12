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
    
    private void Awake()
    {
        _mainGrid = GetComponent<Grid>();
    }
}
