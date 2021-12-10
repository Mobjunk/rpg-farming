using UnityEngine;
using UnityEngine.Tilemaps;

public class CharacterPlaceObject : Singleton<CharacterPlaceObject>
{
    private TileClickManager _tileClickManager => TileClickManager.Instance();
    
    private Player _player;
    public GameObject CurrentGameObjectHoverd;

    [SerializeField] private GameObject[] _tileChecker;
    [SerializeField] private Grid _grid;
    public Tilemap[] TileMaps;
    
    [SerializeField] private Tile _canPlace;
    [SerializeField] private Tile _cannotPlace;

    private Vector3Int _placeHolderPosition;
    private bool _canPlaceObject;

    public void Awake()
    {
        _player = GetComponent<Player>();
    }

    public bool IsDisabled = true;
    
    private void Update()
    {
        if (IsDisabled) return;
        
        if (_player.CharacterUIManager.CurrentUIOpened != null)
        {
            TileMaps[0].ClearAllTiles();
            return;
        }
        
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Vector3Int tilePosition = TileMaps[0].WorldToCell(mousePosition);
        
        TileMaps[0].ClearAllTiles();

        if (CursorManager.Instance().IsPointerOverUIElement()) return;

        bool canPlaceObject = Utility.CanInteractWithTile(_grid, tilePosition, _tileChecker);
        AbstractPlaceableItem placeableItem = null;
        if (_player.ItemAboveHeadRenderer.sprite != null && _player.ItemAboveHead.item != null)
        {
            placeableItem = (AbstractPlaceableItem) _player.ItemAboveHead.item;
            if (placeableItem == null) placeableItem = (AbstractPlantData) _player.ItemAboveHead.item;
            
            for (int width = 0; width < placeableItem.width; width++)
            {
                for (int height = 0; height < placeableItem.height; height++)
                {
                    Vector3Int currentTile = new Vector3Int(tilePosition.x + width, tilePosition.y + height, tilePosition.z);
                    
                    bool hasTile = TileMaps[1].GetTile(currentTile) != null;
                    TileMaps[0].SetTile(currentTile, hasTile || !canPlaceObject ? _cannotPlace : _canPlace);
                    
                    if (hasTile) canPlaceObject = false;
                }
            }
        }
        if (Input.GetMouseButtonDown(0) && canPlaceObject && placeableItem != null)
        {
            //Checks if you are trying to plant a crop on anything other then dirt
            if (placeableItem.GetType() == typeof(AbstractPlantData) && !_tileClickManager.CheckTileUnderObject(mousePosition, TileType.DIRT)) return;
            
            //Handles removing the item from the inventory
            _player.CharacterInventory.RemoveItem(placeableItem);
            //Checks if the player still has the item it has to remove
            if (_player.CharacterInventory.Items[ItemBarManager.Instance().selectedSlot].item == null)
            {
                _player.CharacterStateManager.SetAnimator("wieldingItem", false);
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
                    TileMaps[1].SetTile(currentTile, _cannotPlace);
                }
            }

            Vector3 position = TileMaps[1].GetCellCenterWorld(tilePosition);
            float additionalX = 0.005f * ((placeableItem.uiSprite.bounds.size.x * 100) - 16);
            float additionalY = 0.005f * ((placeableItem.uiSprite.bounds.size.y * 100) - 16);
            Instantiate(placeableItem.objectPrefab, new Vector3(position.x + additionalX, position.y + additionalY, position.z), Quaternion.identity);
        }
    }
}
