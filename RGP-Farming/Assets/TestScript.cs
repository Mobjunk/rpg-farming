using System;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TestScript : MonoBehaviour
{

    [SerializeField] private TileBase _occupiedTile;
    private Player _player => Player.Instance();

    private SpriteRenderer _spriteRenderer;

    [SerializeField] private Vector2 _objectSize;
    
    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        Vector2 spriteSize = _spriteRenderer.sprite.bounds.size;
        //Debug.Log(transform.name + " " + spriteSize);

        Vector3Int tilePos = _player.CharacterPlaceObject.Grid.WorldToCell(transform.position);
        //TileBase tile = _player.CharacterPlaceObject.GetPlayerTileMap.GetTile(tilePos);

        _player.CharacterPlaceObject.GetPlayerTileMap.SetTile(tilePos, _occupiedTile);
        GridManager.Instance().UpdateGrid(new Vector2(tilePos.x, tilePos.y), false);
    }
}
