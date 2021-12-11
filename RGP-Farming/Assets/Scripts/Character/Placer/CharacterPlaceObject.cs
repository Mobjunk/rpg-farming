using UnityEngine;
using UnityEngine.Tilemaps;

public class CharacterPlaceObject : Singleton<CharacterPlaceObject>
{
    private TileClickManager tileClickManager => TileClickManager.Instance();
    
    private Player player;
    private GameObject currentGameObjectHoverd;

    [SerializeField] private GameObject[] tileChecker;
    [SerializeField] private Grid grid;
    [SerializeField] private Tilemap[] tileMaps;

    public Tilemap[] GetTilemaps() => tileMaps;
    
    [SerializeField] private Tile canPlace;
    [SerializeField] private Tile cannotPlace;

    private Vector3Int placeHolderPosition;
    private bool canPlaceObject;
    
    public GameObject CurrentGameObjectHoverd
    {
        get => currentGameObjectHoverd;
        set => currentGameObjectHoverd = value;
    }

    public void Awake()
    {
        player = GetComponent<Player>();
    }

    private void Update()
    {
        if (player.CharacterUIManager.CurrentUIOpened != null)
        {
            tileMaps[0].ClearAllTiles();
            return;
        }
        
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Vector3Int tilePosition = tileMaps[0].WorldToCell(mousePosition);
        
        tileMaps[0].ClearAllTiles();

        if (CursorManager.Instance().IsPointerOverUIElement()) return;

        bool canPlaceObject = Utility.CanInteractWithTile(grid, tilePosition, tileChecker);
        AbstractPlaceableItem placeableItem = null;
        if (player.ItemAboveHeadRenderer.sprite != null && player.ItemAboveHead.item != null)
        {
            placeableItem = (AbstractPlaceableItem) player.ItemAboveHead.item;
            if (placeableItem == null) placeableItem = (AbstractPlantData) player.ItemAboveHead.item;
            
            for (int width = 0; width < placeableItem.width; width++)
            {
                for (int height = 0; height < placeableItem.height; height++)
                {
                    Vector3Int currentTile = new Vector3Int(tilePosition.x + width, tilePosition.y + height, tilePosition.z);
                    
                    bool hasTile = tileMaps[1].GetTile(currentTile) != null;
                    tileMaps[0].SetTile(currentTile, hasTile || !canPlaceObject ? cannotPlace : canPlace);
                    
                    if (hasTile) canPlaceObject = false;
                }
            }
        }
        if (Input.GetMouseButtonDown(0) && canPlaceObject && placeableItem != null)
        {
            //Checks if you are trying to plant a crop on anything other then dirt
            if (placeableItem.GetType() == typeof(AbstractPlantData) && !tileClickManager.CheckTileUnderObject(mousePosition, TileType.DIRT)) return;
            
            //Handles removing the item from the inventory
            player.CharacterInventory.RemoveItem(placeableItem);
            //Checks if the player still has the item it has to remove
            if (player.CharacterInventory.items[ItemBarManager.Instance().selectedSlot].item == null)
            {
                player.CharacterStateManager.SetAnimator("wieldingItem", false);
                player.ItemAboveHeadRenderer.sprite = null;
                player.ItemAboveHead = null;
                ItemBarManager.Instance().ItemDisplayer.gameObject.SetActive(false);
                ItemSnapperManager.Instance().ResetSnappedItem();
            }

            for (int width = 0; width < placeableItem.width; width++)
            {
                for (int height = 0; height < placeableItem.height; height++)
                {
                    Vector3Int currentTile = new Vector3Int(tilePosition.x + width, tilePosition.y + height, tilePosition.z);
                    tileMaps[1].SetTile(currentTile, cannotPlace);
                }
            }

            Vector3 position = tileMaps[1].GetCellCenterWorld(tilePosition);
            float additionalX = 0.005f * ((placeableItem.uiSprite.bounds.size.x * 100) - 16);
            float additionalY = 0.005f * ((placeableItem.uiSprite.bounds.size.y * 100) - 16);
            Instantiate(placeableItem.objectPrefab, new Vector3(position.x + additionalX, position.y + additionalY, position.z), Quaternion.identity);
        }
    }
}
