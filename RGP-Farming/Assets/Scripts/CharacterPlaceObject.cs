using UnityEngine;
using UnityEngine.Tilemaps;

public class CharacterPlaceObject : Singleton<CharacterPlaceObject>
{
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
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        //float distance = Vector2.Distance(playerFeet.transform.position, mousePosition);
        
        //Debug.Log("distance: " + distance);

        Vector3Int tilePosition = placeableTiles.WorldToCell(mousePosition);
        /*for (int index = 0; index < tileChecker.Length; index++)
        {
            Vector3Int pos = grid.WorldToCell(tileChecker[index].transform.position);
            int distance = Mathf.FloorToInt(Vector2.Distance(new Vector2(tilePosition.x, tilePosition.y), new Vector2(pos.x, pos.y)));
            
            canPlaceObject = CurrentGameObjectHoverd == null && !CursorManager.Instance().IsPointerOverUIElement() && distance <= 1;
            if (canPlaceObject) break;
        }*/
        
        canPlaceObject = CurrentGameObjectHoverd == null && !CursorManager.Instance().IsPointerOverUIElement() && Utility.CanInteractWithTile(grid, tilePosition, tileChecker);
        if (player.ItemAboveHeadRenderer.sprite != null)
        {
            //Checks if the current tile is still the same one as before
            //If not it removes the tile
            if (placeHolderPosition != tilePosition && placeableTiles.GetTile(placeHolderPosition) != null) placeableTiles.SetTile(placeHolderPosition, null);
            
            //Handles setting the placeholder position
            //And the tile to the correct one
            placeHolderPosition = tilePosition;
            placeableTiles.SetTile(tilePosition, canPlaceObject ? canPlace : cannotPlace);
        } else placeableTiles.ClearAllTiles();

        if (Input.GetMouseButtonDown(0) && player.ItemAboveHead != null && canPlaceObject)
        {
            
            AbstractPlaceableItem currentItem = (AbstractPlaceableItem) player.ItemAboveHead.item;
            if (currentItem == null) return;
            
            //var onePixel = 0.08f / 16; //0.08f
            //var additionX = player.ItemAboveHead.item.uiSprite.bounds.size.x * onePixel;
            //var additionY = player.ItemAboveHead.item.uiSprite.bounds.size.y * onePixel;
            
            player.CharacterInventory.RemoveItem(currentItem);
            if (!player.CharacterInventory.HasItem(currentItem))
            {
                player.ItemAboveHeadRenderer.sprite = null;
                player.ItemAboveHead = null;
                ItemSnapperManager.Instance().ResetSnappedItem();
            }
            
            var test = placeableTiles.CellToWorld(tilePosition);
            Instantiate(currentItem.objectPrefab, new Vector3(test.x + 0.08f, test.y + 0.08f), Quaternion.identity);
        }
    }
}
