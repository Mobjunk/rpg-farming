using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;

public class TilemapManager : Singleton<TilemapManager>
{
    private MainLevelTiles _mainLevelTiles => MainLevelTiles.Instance();
    //[Header("Main Level Tilemap Variables")]

    [SerializeField] private Grid _mainGrid=> _mainLevelTiles.MainGrid;
    public Grid MainGrid => _mainGrid;

    [SerializeField] private Tilemap _hoverTilemap => _mainLevelTiles.HoverTilemap;

    public Tilemap HoverTilemap => _hoverTilemap;
    
    [SerializeField] private Tilemap[] _tilemapsToCheck;

    public Tilemap[] TilemapsToCheck => _mainLevelTiles.TilemapsToCheck;

    [SerializeField] private Tilemap[] _allTilemaps => _mainLevelTiles.AllTilemaps;
    public Tilemap[] AllTilemaps => _allTilemaps;
    
    [SerializeField] private Tilemap[] _unwalkableTilemaps => _mainLevelTiles.UnwalkableTilemaps;

    public Tilemap[] UnwalkableTilemaps => _unwalkableTilemaps;
    
    [Header("House Tilemap Variables")]
    [SerializeField] private Tilemap _playerHouseTiles;

    public Tilemap PlayerHouseTiles
    {
        set => _playerHouseTiles = value;
    }
    
    [SerializeField] private Grid _houseGrid;

    public Grid HouseGrid
    {
        set => _houseGrid = value;
    }
    public int GetTileType(Vector3 pCurrentPosition)
    {
        if (_playerHouseTiles != null)
        {
            TileBase indoorTiles = _playerHouseTiles.GetTile(_mainGrid.WorldToCell(pCurrentPosition));

            if (SceneManager.GetSceneByName("Playerhouse").isLoaded)
                indoorTiles = _playerHouseTiles.GetTile(_houseGrid.WorldToCell(pCurrentPosition));

            if (indoorTiles != null)
            {
                Debug.Log("indoorTiles.name: " + indoorTiles.name);
                switch (indoorTiles.name)
                {
                    case "wallpapers_1615":
                        return (int) FootstepsValues.TILES;
                    default: return (int) FootstepsValues.WOOD;
                }
            }
        }

        if (SeasonManager.Instance().SeasonalCount == (int) SeasonValues.WINTER)
            return (int) FootstepsValues.SNOW;
        
        TileBase walkableTiles = _allTilemaps[5].GetTile(_mainGrid.WorldToCell(pCurrentPosition));
        if (walkableTiles != null)
        {
            Debug.Log("walkableTiles.name: " + walkableTiles.name);
            switch (walkableTiles.name)
            {
                case "tiles_225":
                case "tiles_226":
                case "tiles_228":
                case "tiles_229":
                    
                case "tiles_236":
                case "tiles_237":
                case "tiles_239":
                    
                case "tiles_240":
                case "tiles_247":
                case "tiles_248":
                    
                case "tiles_250":
                case "tiles_251":
                case "tiles_258":
                case "tiles_259":
                    
                case "tiles_262":
                    
                case "GroundSummerObjects_280":
                case "GroundSummerObjects_281":
                case "GroundSummerObjects_295":
                    return (int) FootstepsValues.WOOD;
                case "Grass 3":
                case "Grass 7":
                case "Grass 6":
                case "Grass_Corners 3":
                case "tiles_11":
                    return (int) FootstepsValues.GRASS;
                
                case "GroundSummerObjects_196":
                case "GroundSummerObjects_197":
                case "GroundSummerObjects_169":
                case "GroundSummerObjects_210":
                case "GroundSummerObjects_211":
                case "GroundSummerObjects_212":
                    
                case "GroundSummerObjects_225":
                case "GroundSummerObjects_226":
                    return (int) FootstepsValues.TILES;

                default: return (int) FootstepsValues.GRAVEL;
            }
        }
        TileBase grassTile = _allTilemaps[1].GetTile(_mainGrid.WorldToCell(pCurrentPosition));
        if (grassTile != null)
        {
            Debug.Log("grassTile.name: " + grassTile.name);
            switch (grassTile.name)
            {
                case "GroundSummer_239":
                    return (int) FootstepsValues.WOOD;
                default: return (int) FootstepsValues.GRASS;
            }
        }
        
        TileBase dirtTile = _allTilemaps[0].GetTile(_mainGrid.WorldToCell(pCurrentPosition));
        if (dirtTile != null)
        {
            Debug.Log("dirtTile.name: " + dirtTile.name);
            switch (dirtTile.name)
            {
                case "tiles_115":
                case "tiles_116":
                    return (int) FootstepsValues.SAND;
                default: return (int) FootstepsValues.GRAVEL;
            }
        }
        
        TileBase waterTiles = _allTilemaps[2].GetTile(_mainGrid.WorldToCell(pCurrentPosition));
        if (waterTiles != null)
        {
            Debug.Log("waterTiles.name: " + waterTiles.name);
            switch (waterTiles.name)
            {
                case "tiles_132":
                case "tiles_133":
                case "tiles_135":
                case "tiles_136":
                    
                case "tiles_143":
                case "tiles_145":
                case "tiles_147":
                    
                case "tiles_154":
                case "tiles_155":
                case "tiles_156":
                case "tiles_157":
                case "tiles_158":
                    return (int) FootstepsValues.SAND;
                default: return (int) FootstepsValues.WATER;
            }
        }
        Debug.Log("kanker kanker");
        return -1;
    }

    public string GetFootstepName(int pTileType)
    {
        //gravel 0
        //grass 1
        //wood 2
        //tiles 3
        //metal 4
        //water 5
        //snow 6
        //sand 7
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
            case 6:
                return "Snow";
            case 7:
                return "Sand";
        }
        return "DEFAULT";
    }
    
}

public enum FootstepsValues
{
    GRAVEL = 0,
    GRASS = 1,
    WOOD = 2,
    TILES = 3,
    METAL = 4,
    WATER = 5,
    SNOW = 6,
    SAND = 7
}
