using UnityEngine;
using UnityEngine.Tilemaps;

public class CharacterPlaceObject : Singleton<CharacterPlaceObject>
{
    private TilePlacer tilePlacer => TilePlacer.Instance();
    
    private Player player;
    private GameObject currentGameObjectHoverd;

    [SerializeField] private GameObject[] tileChecker;
    [SerializeField] private Grid grid;
    [SerializeField] private Tilemap placeableTiles;
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
            placeableTiles.ClearAllTiles();
            return;
        }
        
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Vector3Int tilePosition = placeableTiles.WorldToCell(mousePosition);

        canPlaceObject = CurrentGameObjectHoverd == null && !CursorManager.Instance().IsPointerOverUIElement() && Utility.CanInteractWithTile(grid, tilePosition, tileChecker);
        bool meetsRequirment = true;
        
        placeableTiles.ClearAllTiles();
        
        if (player.ItemAboveHeadRenderer.sprite != null)
        {
            //Checks if the item in the hand is a plantable object
            //TODO: Clean this shit
            if (tilePlacer != null && player.ItemAboveHead != null && player.ItemAboveHead.item != null && player.ItemAboveHead.item.GetType() == typeof(AbstractPlantData) && !tilePlacer.CheckTileUnderObject(mousePosition, TileType.DIRT)) meetsRequirment = false;
            
            AbstractPlaceableItem placeableItem = (AbstractPlaceableItem) player.ItemAboveHead.item;

            for (int width = 0; width < placeableItem.width; width++)
            {
                for (int height = 0; height < placeableItem.height; height++)
                {
                    Vector3Int currentTile = new Vector3Int(tilePosition.x + width, tilePosition.y + height, tilePosition.z);
                    //TODO: Check if any of the tiles comes in contact with some sort of thing on the tile pallet or game object
                    placeableTiles.SetTile(currentTile, canPlaceObject && meetsRequirment ? canPlace : cannotPlace);
                }
            }
        }

        //Checks if the player clicked the mouse and has all required requirments
        if (Input.GetMouseButtonDown(0) && player.ItemAboveHead != null && canPlaceObject && meetsRequirment)
        {
            //Checks if its a placeable 
            AbstractPlaceableItem currentItem = (AbstractPlaceableItem) player.ItemAboveHead.item;
            if (currentItem == null)
            {
                currentItem = (AbstractPlantData) player.ItemAboveHead.item;
                if (currentItem == null) return;
            }
            
            //Checks if you are trying to plant a crop on anything other then dirt
            if (currentItem.GetType() == typeof(AbstractPlantData) && !tilePlacer.CheckTileUnderObject(mousePosition, TileType.DIRT)) return;
            
            //Handles removing the item from the inventory
            player.CharacterInventory.RemoveItem(currentItem);
            //Checks if the player still has the item it has to remove
            if (player.CharacterInventory.items[ItemBarManager.Instance().selectedSlot].item == null)
            {
                player.CharacterStateManager.SetAnimator("wieldingItem", false);
                player.ItemAboveHeadRenderer.sprite = null;
                player.ItemAboveHead = null;
                ItemSnapperManager.Instance().ResetSnappedItem();
            }
            
            //Handles spawning the game object
            Instantiate(currentItem.objectPrefab, placeableTiles.GetCellCenterWorld(tilePosition), Quaternion.identity);
        }
    }
}
