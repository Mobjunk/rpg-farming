using UnityEngine;
using UnityEngine.Tilemaps;

public class CharacterPlaceObject : Singleton<CharacterPlaceObject>
{
    private TilePlacer _tilePlacer => TilePlacer.Instance();
    
    private Player _player;
    private GameObject _currentGameObjectHoverd;

    [SerializeField] private GameObject[] _tileChecker;
    [SerializeField] private Grid _grid;
    [SerializeField] private Tilemap[] _tileMaps;

    public Tilemap[] GetTilemaps() => _tileMaps;
    
    [SerializeField] private Tile _canPlace;
    [SerializeField] private Tile _cannotPlace;

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
        if (_player.CharacterUIManager.CurrentUIOpened != null)
        {
            _tileMaps[0].ClearAllTiles();
            return;
        }
        
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Vector3Int tilePosition = _tileMaps[0].WorldToCell(mousePosition);
        
        _tileMaps[0].ClearAllTiles();

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
                    
                    bool hasTile = _tileMaps[1].GetTile(currentTile) != null;
                    
                    _tileMaps[0].SetTile(currentTile, hasTile || !canPlaceObject ? _cannotPlace : _canPlace);
                    
                    if (hasTile) canPlaceObject = false;
                }
            }
        }
        if (Input.GetMouseButtonDown(0) && canPlaceObject && placeableItem != null)
        {
            //Checks if you are trying to plant a crop on anything other then dirt
            if (placeableItem.GetType() == typeof(AbstractPlantData) && !_tilePlacer.CheckTileUnderObject(mousePosition, TileType.DIRT)) return;
            
            //Handles removing the item from the inventory
            _player.CharacterInventory.RemoveItem(placeableItem);
            //Checks if the player still has the item it has to remove
            if (_player.CharacterInventory.Items[ItemBarManager.Instance().SelectedSlot].Item == null)
            {
                _player.CharacterStateManager.SetAnimator("wielding", false);
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
                    _tileMaps[1].SetTile(currentTile, _cannotPlace);
                }
            }

            Vector3 position = _tileMaps[1].GetCellCenterWorld(tilePosition);
            float additionalX = 0.005f * ((placeableItem.uiSprite.bounds.size.x * 100) - 16);
            float additionalY = 0.005f * ((placeableItem.uiSprite.bounds.size.y * 100) - 16);
            Instantiate(placeableItem.objectPrefab, new Vector3(position.x + additionalX, position.y + additionalY, position.z), Quaternion.identity);
        }
    }
}
