using UnityEngine;
using UnityEngine.Tilemaps;

public class CharacterPlaceObject : Singleton<CharacterPlaceObject>
{
    private TilePlacer _tilePlacer => TilePlacer.Instance();
    private TilemapManager _tilemapManager => TilemapManager.Instance();
    
    private Player _player;
    private GameObject _currentGameObjectHoverd;

    [SerializeField] private GameObject[] _tileChecker;
    [SerializeField] private Grid _grid;

    public Grid Grid => _grid;
    
    [SerializeField] private Tilemap _hoverTilemap;

    [SerializeField] private Tilemap[] _tilemapsToCheck;

    [SerializeField] private Tilemap _playerDirtTiles;

    public Tilemap GetPlayerTileMap => _tilemapsToCheck[2];
    
    [SerializeField] private Tile _canPlace;
    [SerializeField] private Tile _cannotPlace;
    [SerializeField] private Tile _occupiedTile;

    private Vector3Int _placeHolderPosition;
    private bool _canPlaceObject;
    
    public GameObject CurrentGameObjectHoverd
    {
        get => _currentGameObjectHoverd;
        set => _currentGameObjectHoverd = value;
    }

    public void Awake()
    {
        _player = GetComponent<Player>();
    }

    private void Update()
    {
        if (_grid == null)
        {
            _grid = _tilemapManager.MainGrid;
            _hoverTilemap = _tilemapManager.HoverTilemap;
            _tilemapsToCheck = _tilemapManager.TilemapsToCheck;
        }
        
        if (_player.CharacterUIManager.CurrentUIOpened != null)
        {
            _hoverTilemap.ClearAllTiles();
            return;
        }
        
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Vector3Int tilePosition = _hoverTilemap.WorldToCell(mousePosition);
        
        _hoverTilemap.ClearAllTiles();

        if (CursorManager.Instance().IsPointerOverUIElement()) return;

        bool canPlaceObject = Utility.CanInteractWithTile(_grid, tilePosition, _tileChecker);
        AbstractPlaceableItem placeableItem = null;
        if (_player.ItemAboveHeadRenderer.sprite != null && _player.ItemAboveHead.Item != null)
        {
            placeableItem = (AbstractPlaceableItem) _player.ItemAboveHead.Item;
            if (placeableItem == null) placeableItem = (AbstractPlantData) _player.ItemAboveHead.Item;

            for (int width = 0; width < placeableItem.width; width++)
            {
                for (int height = 0; height < placeableItem.height; height++)
                {
                    Vector3Int currentTile = new Vector3Int(tilePosition.x + width, tilePosition.y + height, tilePosition.z);

                    bool hasTile = false;

                    if (placeableItem.GetType() == typeof(AbstractPlantData))
                    {
                        canPlaceObject = _playerDirtTiles.GetTile(currentTile) != null;
                        hasTile = GetPlayerTileMap.GetTile(currentTile) != null;
                    }
                    else
                    {
                        foreach (Tilemap tilemap in _tilemapsToCheck)
                        {
                            if (tilemap.GetTile(currentTile) != null)
                            {
                                hasTile = true;
                                break;
                            }
                        }
                    }

                    _hoverTilemap.SetTile(currentTile, hasTile || !canPlaceObject ? _cannotPlace : _canPlace);
                    
                    if (hasTile) canPlaceObject = false;
                }
            }
        }
        if (Input.GetMouseButtonDown(0) && canPlaceObject && placeableItem != null)
        {
            //Checks if you are trying to plant a crop on anything other then dirt
            if (placeableItem is AbstractPlantData && !_tilePlacer.CheckTileUnderObject(mousePosition, TileType.DIRT)) return;
            
            if(placeableItem is AbstractPlantData) SoundManager.Instance().ExecuteSound("PlantSeed");
            
            //Handles removing the item from the inventory
            _player.CharacterInventory.RemoveItem(placeableItem);
            //Checks if the player still has the item it has to remove
            if (_player.CharacterInventory.Items[ItemBarManager.Instance().SelectedSlot].Item == null)
            {
                Utility.SetAnimator(_player.CharacterStateManager.GetAnimator(), "wielding", false);
                _player.ItemAboveHeadRenderer.sprite = null;
                _player.ItemAboveHead = null;
                ItemBarManager.Instance().ItemDisplayer.gameObject.SetActive(false);
                ItemSnapperManager.Instance().ResetSnappedItem();
            }

            for (int width = 0; width < placeableItem.width; width++)
            {
                for (int height = 0; height < placeableItem.height; height++)
                {
                    Vector3Int currentTile = new Vector3Int(tilePosition.x + width, tilePosition.y + height, tilePosition.z);
                    GetPlayerTileMap.SetTile(currentTile, _occupiedTile);
                    GridManager.Instance().UpdateGrid(new Vector2(currentTile.x, currentTile.y), placeableItem.walkable);
                    
                }
            }

            Vector3 position = GetPlayerTileMap.GetCellCenterWorld(tilePosition);
            float additionalX = 0;//0.005f * ((placeableItem.uiSprite.bounds.size.x * 100) - 16);
            float additionalY = 0;//0.005f * ((placeableItem.uiSprite.bounds.size.y * 100) - 16);
            Instantiate(placeableItem.objectPrefab, new Vector3(position.x + additionalX, position.y + additionalY, position.z), Quaternion.identity);
        }
    }
}
