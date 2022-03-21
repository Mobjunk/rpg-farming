using System;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TestScript : MonoBehaviour
{
    private TilemapManager _tilemapManager => TilemapManager.Instance();

    [SerializeField] private TileBase _occupiedTile;
    private Player _player => Player.Instance();

    private SpriteRenderer _spriteRenderer;

    [SerializeField] private Vector2 _objectSize;

    [HideInInspector] public Vector3Int TilePosition;
    
    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        Vector2 spriteSize = _spriteRenderer.sprite.bounds.size;
        
        TilePosition = _tilemapManager.MainGrid.WorldToCell(transform.position);

        UpdateGrid();
    }

    public void UpdateGrid(bool pWalkable = false)
    {
        _tilemapManager.TilemapsToCheck[2].SetTile(TilePosition, pWalkable ? null : _occupiedTile);
        GridManager.Instance().UpdateGrid(new Vector2(TilePosition.x, TilePosition.y), pWalkable);
    }
}
