using System;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.Tilemaps;

public class TestScript : MonoBehaviour
{
    private TilemapManager _tilemapManager => TilemapManager.Instance();

    [SerializeField] private TileBase _occupiedTile;
    private Player _player => Player.Instance();

    private SpriteRenderer _spriteRenderer;

    public Vector2 Offset;
    
    [SerializeField] private Vector2 _objectSize;

    [HideInInspector] public Vector3Int TilePosition;
    
    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        Vector2 spriteSize = _spriteRenderer.sprite.bounds.size;
        
        TilePosition = _tilemapManager.MainGrid.WorldToCell(transform.position);

        if (!_objectSize.Equals(Vector2.zero)) UpdateGrid((int)_objectSize.x, (int)_objectSize.y);
        else if (spriteSize.x > 1 && spriteSize.y > 1) UpdateGrid((int)spriteSize.x, (int)spriteSize.y);
        else UpdateGrid();
    }

    public void UpdateGrid(bool pWalkable = false)
    {
        UpdateGrid(TilePosition, pWalkable);
    }

    private void UpdateGrid(Vector3Int pTilePosition, bool pWalkable = false)
    {
        _tilemapManager.TilemapsToCheck[2].SetTile(pTilePosition, pWalkable ? null : _occupiedTile);
        GridManager.Instance().UpdateGrid(new Vector2(pTilePosition.x, pTilePosition.y), pWalkable);
    }

    private void UpdateGrid(int pSizeX, int pSizeY)
    {
        for (int x = 0; x < pSizeX; x++)
        {
            for (int y = 0; y < pSizeY; y++)
            {
                Vector3Int tilePosition = _tilemapManager.MainGrid.WorldToCell(new Vector3((transform.parent.position.x - (pSizeX / 2.5f) + Offset.x) + x, (transform.parent.position.y + y) + Offset.y, transform.parent.position.z));

                UpdateGrid(tilePosition);
            }
        }
    }
}
