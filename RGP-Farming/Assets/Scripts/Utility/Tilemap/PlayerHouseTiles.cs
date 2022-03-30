using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerHouseTiles : Singleton<PlayerHouseTiles>
{
    private TilemapManager _tilemapManager => TilemapManager.Instance();
    
    [SerializeField] private Tilemap _floorTiles;

    [SerializeField] private Grid _houseGrid;

    private void Awake()
    {
        _tilemapManager.PlayerHouseTiles = _floorTiles;
        _tilemapManager.HouseGrid = _houseGrid;
    }
}
