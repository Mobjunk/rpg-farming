using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;
using static Utility;

public class TileTester : MonoBehaviour
{
    private Player _player => Player.Instance();
    private ItemBarManager _itemBarManager => ItemBarManager.Instance();

    [Header("The grid within the world")]
    [SerializeField] private Grid _currentGrid;

    [Header("The tile maps in the scene")]
    [SerializeField] private Tilemap[] _tilemaps;
    
    [Header("The tiles that can be placed on the tile map")]
    [SerializeField] private Tile[] _placeableTiles;

    [Header("A placeholder tile for tiles that dont have the proper flags")]
    [SerializeField] private Tile _placeHolderTile;

    [Header("The tiles that have been placed in the world by the player")]
    [SerializeField] private List<TileData> _placedTiles = new List<TileData>();

    private Vector3Int _tileLocation;
    private Vector3 _mousePosition;

    private void Awake()
    {
        _currentGrid = GetComponent<Grid>();
    }

    private void Update()
    {
        _mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        _tileLocation = _tilemaps[0].WorldToCell(_mousePosition);
        if (Input.GetMouseButtonDown(0) && !CursorManager.Instance().IsPointerOverUIElement() && CanInteractWithTile(_currentGrid, _tileLocation, _player.TileChecker))
        {
            PlaceDirtTile();
            RemoveDirtTile();
        }
    }
    
    int[] possibleFlags = { N, NE, E, SE, S, SW, W, NW };

    /// <summary>
    /// Handles placing a dirt tile on the tile map
    /// </summary>
    public void PlaceDirtTile()
    {
        Tilemap dirtTilemap = _tilemaps[1];
        if (dirtTilemap.GetTile(dirtTilemap.WorldToCell(_mousePosition)) != _placeableTiles[0] && _itemBarManager.IsWearingCorrectTool(ToolType.HOE))
        {
            _player.CharacterStateManager.SetAnimator("hoe", true, true);
            Tile tile = _placeableTiles[0];
            PlaceTile(dirtTilemap, _tileLocation, tile, TileTypes.DIRT);

            List<TileData> tilesToEdit = GetEditedTiles(dirtTilemap);

            foreach (TileData editedTiles in tilesToEdit)
                UpdateTile(dirtTilemap, editedTiles.TileLocation, GetTileForFlag(GetFlag(dirtTilemap, editedTiles)));
        }
    }

    /// <summary>
    /// Removes a dirt tile from the tile map
    /// </summary>
    public void RemoveDirtTile()
    {
        Tilemap dirtTilemap = _tilemaps[1];
        if (dirtTilemap.GetTile(dirtTilemap.WorldToCell(_mousePosition)) == _placeableTiles[0] && _itemBarManager.IsWearingCorrectTool(ToolType.PICKAXE) && _player.CharacterPlaceObject.CurrentGameObjectHoverd == null)
        {
            Debug.Log("???");
        }
    }

    /// <summary>
    /// Handles placing a tile on a specific tilemap and location
    /// </summary>
    /// <param name="pTilemap">The tile map which the tile will be placed on</param>
    /// <param name="pTileLocation">The tile location</param>
    /// <param name="pTile">The tile being placed</param>
    /// <param name="pTileType">The type of the tile</param>
    /// <returns></returns>
    private TileData PlaceTile(Tilemap pTilemap, Vector3Int pTileLocation, Tile pTile, TileTypes pTileType)
    {
        if (TileAlreadyExist(pTileLocation))
        {
            if (pTileType.Equals(TileTypes.GRASS)) return null;
            
            TileData tData = GetTileData(pTileLocation);
            if (tData != null)
            {
                tData.TileType = pTileType;
                tData.Tile = pTile;
                pTilemap.SetTile(tData.TileLocation, pTile);
                return tData;
            }
        }
        
        TileData tileData = new TileData(pTileLocation, pTileType, pTile);
        
        if (!pTileType.Equals(TileTypes.GRASS)) pTilemap.SetTile(pTileLocation, pTile);
        
        _placedTiles.Add(tileData);

        return tileData;
    }

    /// <summary>
    /// Handles updating a tile
    /// </summary>
    /// <param name="pTilemap">The tilemap that we are updating</param>
    /// <param name="pTileLocation">The tile location</param>
    /// <param name="pTile">The tile we are updating to</param>
    private void UpdateTile(Tilemap pTilemap, Vector3Int pTileLocation, Tile pTile)
    {
        TileData tileData = GetExistingGrassTile(pTileLocation);
        if (tileData == null) return;
        
        pTilemap.SetTile(pTileLocation, pTile);
        tileData.Tile = pTile;
    }

    /// <summary>
    /// Checks if a tile already exists
    /// </summary>
    /// <param name="pTileLocation">The location of the tile</param>
    /// <returns></returns>
    private bool TileAlreadyExist(Vector3Int pTileLocation)
    {
        return _placedTiles.Any(tileData => tileData.TileLocation.Equals(pTileLocation));
    }

    /// <summary>
    /// Checks if a grass tile exists on a position
    /// </summary>
    /// <param name="pTileLocation">The position of the tile</param>
    /// <returns></returns>
    private TileData GetExistingGrassTile(Vector3Int pTileLocation)
    {
        return _placedTiles.Where(tileData => !tileData.TileType.Equals(TileTypes.DIRT)).FirstOrDefault(tileData => tileData.TileLocation.Equals(pTileLocation) && tileData.TileType.Equals(TileTypes.GRASS));
        //return _placedTiles.FirstOrDefault(tileData => tileData.TileLocation.Equals(pTileLocation) && tileData.TileType.Equals(TileTypes.GRASS));
    }

    /// <summary>
    /// Grabs the title data based on a tile location
    /// </summary>
    /// <param name="pTileLocation">The tile location</param>
    /// <returns></returns>
    private TileData GetTileData(Vector3Int pTileLocation)
    {
        return _placedTiles.FirstOrDefault(tileData => tileData.TileLocation.Equals(pTileLocation));
    }
    
        /// <summary>
    /// Gets the flag for a specific tile
    /// </summary>
    /// <param name="pTileMap">The tilemap needed to grab the location</param>
    /// <param name="pEditedTiles">The tile it gets the flags for</param>
    /// <returns></returns>
    private int GetFlag(Tilemap pTileMap, TileData pEditedTiles)
    {
        int flags = 0;
        for (int dir = 0; dir < 8; dir++)
        {
            int[] pos = GetPositionForDirection(pEditedTiles.TileLocation.x, pEditedTiles.TileLocation.y, dir);
            Vector3Int tilePosition = pTileMap.WorldToCell(new Vector3(pos[0], pos[1]));
            TileData tileData = GetTileData(tilePosition);

            if (tileData == null) continue;

            if (tileData.TileType.Equals(TileTypes.DIRT))
                flags |= possibleFlags[dir];
        }

        return flags;
    }

    /// <summary>
    /// Gets a tile for a specific tile
    /// </summary>
    /// <param name="pFlags">The current flag</param>
    /// <returns></returns>
    private Tile GetTileForFlag(int pFlags)
    {
        Tile tileBase = _placeHolderTile;
        if ((pFlags & N) != 0 && (pFlags & NE) != 0 && (pFlags & E) != 0) tileBase = _placeableTiles[11];
        else if ((pFlags & S) != 0 && (pFlags & SE) != 0 && (pFlags & E) != 0) tileBase = _placeableTiles[9];
        else if ((pFlags & S) != 0 && (pFlags & SW) != 0 && (pFlags & W) != 0) tileBase = _placeableTiles[10];
        else if ((pFlags & N) != 0 && (pFlags & NW) != 0 && (pFlags & W) != 0) tileBase = _placeableTiles[12];
                
        else if ((pFlags & N) != 0) tileBase = _placeableTiles[1];
        else if ((pFlags & E) != 0) tileBase = _placeableTiles[4];
        else if ((pFlags & S) != 0) tileBase = _placeableTiles[2];
        else if ((pFlags & W) != 0) tileBase = _placeableTiles[3];
                
        else if ((pFlags & NE) != 0) tileBase = _placeableTiles[7];
        else if ((pFlags & SE) != 0) tileBase = _placeableTiles[5];
        else if ((pFlags & SW) != 0) tileBase = _placeableTiles[6];
        else if ((pFlags & NW) != 0) tileBase = _placeableTiles[8];

        return tileBase;
    }

    /// <summary>
    /// Gets a list of edited tiles based on the tile the player clicked
    /// </summary>
    /// <param name="pTilemap">The tile map that is being edited</param>
    /// <returns></returns>
    private List<TileData> GetEditedTiles(Tilemap pTilemap)
    {
        List<TileData> tilesToEdit = new List<TileData>();
        for (int direction = 0; direction < 8; direction++)
        {
            int[] pos = GetPositionForDirection(_tileLocation.x, _tileLocation.y, direction);
            Vector3Int tilePosition = pTilemap.WorldToCell(new Vector3(pos[0], pos[1]));
            TileData tileData = PlaceTile(pTilemap, tilePosition, _placeableTiles[0], TileTypes.GRASS);
            if(tileData != null) tilesToEdit.Add(tileData);
            else
            {
                tileData = GetExistingGrassTile(tilePosition);
                if (tileData != null) tilesToEdit.Add(tileData);
            }
        }

        return tilesToEdit;
    }

    [Serializable]
    public class TileData
    {
        public Vector3Int TileLocation;
        public TileTypes TileType;
        public Tile Tile;

        public TileData(Vector3Int pTileLocation, TileTypes pTileType, Tile pTile)
        {
            TileLocation = pTileLocation;
            TileType = pTileType;
            Tile = pTile;
        }
    }

    public enum TileTypes
    {
        NONE,
        DIRT,
        GRASS
    }
}
