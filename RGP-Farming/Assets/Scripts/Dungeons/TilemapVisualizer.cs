using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapVisualizer : Singleton<TilemapVisualizer>
{
    private DungeonPrefabManager _dungeonPrefabManager => DungeonPrefabManager.Instance();
    
    [SerializeField] private Tilemap _floorTilemap;

    [SerializeField] private Tilemap _wallTilemap;

    public Tilemap WallTilemap => _wallTilemap;

    [SerializeField] private Tilemap _placeableTilemap;

    [SerializeField] private TileBase _placeableTile;

    [SerializeField] private TileBase _floorTile;

    [SerializeField] private TileBase _wallTop;
    
    [SerializeField] private TileBase _wallSideRight;
    
    [SerializeField] private TileBase _wallSideLeft;
    
    [SerializeField] private TileBase _wallBottom;
    
    [SerializeField] private TileBase _wallFull;

    [SerializeField] private TileBase _wallInnerCornerDownLeft;

    [SerializeField] private TileBase _wallInnerCornerDownRight;

    [SerializeField] private TileBase _wallDiagonalCornerDownRight;

    [SerializeField] private TileBase _wallDiagonalCornerDownLeft;

    [SerializeField] private TileBase _wallDiagonalCornerUpRight;
    
    [SerializeField] private TileBase _wallDiagonalCornerUpLeft;


    public void PaintPlaceableTiles(IEnumerable<Vector2Int> pFloorPositions)
    {
        PaintTiles(pFloorPositions, _placeableTilemap, _placeableTile);
    }
    
    public void PaintFloorTiles(IEnumerable<Vector2Int> pFloorPositions)
    {
        PaintTiles(pFloorPositions, _floorTilemap, _floorTile);
    }

    private void PaintTiles(IEnumerable<Vector2Int> pPosition, Tilemap pTilemap, TileBase pTile)
    {
        foreach (Vector2Int position in pPosition)
            PaintSingleTile(pTilemap, pTile, position);
    }

    private void PaintSingleTile(Tilemap pTilemap, TileBase pTile, Vector2Int pPosition)
    {
        pTilemap.SetTile(pTilemap.WorldToCell((Vector3Int) pPosition), pTile);
    }

    public void Clear()
    {
        _dungeonPrefabManager.Clear();
        _placeableTilemap.ClearAllTiles();
        _wallTilemap.ClearAllTiles();
        _floorTilemap.ClearAllTiles();
    }

    public void PaintSingleBasicWall(Vector2Int pPosition, string pBinaryType)
    {
        int typeAsInt = Convert.ToInt32(pBinaryType, 2);

        TileBase tile = null;

        if (WallByteTypes.wallTop.Contains(typeAsInt)) tile = _wallTop;
        else if (WallByteTypes.wallSideRight.Contains(typeAsInt)) tile = _wallSideRight;
        else if (WallByteTypes.wallSideLeft.Contains(typeAsInt)) tile = _wallSideLeft;
        else if (WallByteTypes.wallBottm.Contains(typeAsInt)) tile = _wallBottom;
        else if (WallByteTypes.wallFull.Contains(typeAsInt)) tile = _wallFull;
        //else Debug.LogError("[PaintSingleBasicWall] pBinaryType missing " + pBinaryType);
        
        if(tile != null) PaintSingleTile(_wallTilemap, tile, pPosition);
    }

    public void PaintSingleCornerWall(Vector2Int pPosition, string pBinaryType)
    {
        int typeAsInt = Convert.ToInt32(pBinaryType, 2);
        TileBase tile = null;

        if (WallByteTypes.wallInnerCornerDownLeft.Contains(typeAsInt)) tile = _wallInnerCornerDownLeft;
        else if (WallByteTypes.wallInnerCornerDownRight.Contains(typeAsInt)) tile = _wallInnerCornerDownRight;
        else if (WallByteTypes.wallDiagonalCornerDownLeft.Contains(typeAsInt)) tile = _wallDiagonalCornerDownLeft;
        else if (WallByteTypes.wallDiagonalCornerDownRight.Contains(typeAsInt)) tile = _wallDiagonalCornerDownRight;
        else if (WallByteTypes.wallDiagonalCornerUpLeft.Contains(typeAsInt)) tile = _wallDiagonalCornerUpLeft;
        else if (WallByteTypes.wallDiagonalCornerUpRight.Contains(typeAsInt)) tile = _wallDiagonalCornerUpRight;
        else if (WallByteTypes.wallFullEightDirections.Contains(typeAsInt)) tile = _wallFull;
        else if (WallByteTypes.wallBottmEightDirections.Contains(typeAsInt)) tile = _wallBottom;
        //else Debug.LogError("[PaintSingleCornerWall] pBinaryType missing " + pBinaryType);
        
        if(tile != null) PaintSingleTile(_wallTilemap, tile, pPosition);
    }
}