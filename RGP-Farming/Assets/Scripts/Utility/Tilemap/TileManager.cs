using UnityEngine;
using UnityEngine.Tilemaps;

public class TileManager : Singleton<TileManager>
{
    [SerializeField] private Tile _canPlace;

    public Tile CanPlace => _canPlace;
    
    [SerializeField] private Tile _cannotPlace;

    public Tile CannotPlace => _cannotPlace;
    
    [SerializeField] private Tile _occupiedTile;

    public Tile OccupiedTile => _occupiedTile;

    [SerializeField] private Tile _dryFarmTile;

    public Tile DryFarmTile => _dryFarmTile;

    [SerializeField] private Tile _wateredFarmTile;

    public Tile WateredFarmTile => _wateredFarmTile;
}
