using System;
using System.Numerics;
using UnityEngine;
using UnityEngine.Tilemaps;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

public class CharacterPlaceObject : Singleton<CharacterPlaceObject>
{
    private Player player;
    private GameObject currentGameObjectHoverd;

    [SerializeField] public Tilemap placeableTiles;
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
        Vector3Int tilePosition = placeableTiles.WorldToCell(mousePosition);

        canPlaceObject = CurrentGameObjectHoverd == null;
        if (player.ItemAboveHeadRenderer.sprite != null)
        {
            //Checks if the current tile is still the same one as before
            //If not it removes the tile
            if (placeHolderPosition != tilePosition && placeableTiles.GetTile(placeHolderPosition) != null) placeableTiles.SetTile(placeHolderPosition, null);
            
            //Handles setting the placeholder position
            //And the tile to the correct one
            placeHolderPosition = tilePosition;
            placeableTiles.SetTile(tilePosition, CurrentGameObjectHoverd == null ? canPlace : cannotPlace);
        } else placeableTiles.ClearAllTiles();

        if (Input.GetMouseButtonDown(0) && player.ItemAboveHead.item != null && canPlaceObject)
        {
            
            AbstractPlaceableItem currentItem = (AbstractPlaceableItem) player.ItemAboveHead.item;
            //var onePixel = 0.08f / 16; //0.08f
            //var additionX = player.ItemAboveHead.item.uiSprite.bounds.size.x * onePixel;
            //var additionY = player.ItemAboveHead.item.uiSprite.bounds.size.y * onePixel;
            
            var test = placeableTiles.CellToWorld(tilePosition);
            Instantiate(currentItem.objectPrefab, new Vector3(test.x + 0.08f, test.y + 0.08f), Quaternion.identity);
        }
    }
}
