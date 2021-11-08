using UnityEngine;
using UnityEngine.Tilemaps;

public abstract class TileInteractionManager<T> : Singleton<T> where T : MonoBehaviour
{
    public ItemBarManager itemBarManager => ItemBarManager.Instance();
    public Player player => Player.Instance();
    
    public Grid grid;
    public Tilemap[] tileMaps;
    public Tile[] tiles;

    [HideInInspector] public Vector3 mousePosition;
    [HideInInspector] public Vector3Int tileLocation;
    
    public virtual void Update()
    {
        tileMaps[0].ClearAllTiles();
    }

    protected virtual bool CanInteract(ToolType toolType, Tilemap tilemap, Tile tile, bool wateringCan = false)
    {
        if (itemBarManager.IsWearingCorrectTool(toolType) && Utility.CanInteractWithTile(grid, tileLocation, player.TileChecker))
        {
            if (tilemap.GetTile(tilemap.WorldToCell(mousePosition)) == tile)
            {
                if (!wateringCan) return true;

                if (player.CharacterPlaceObject.CurrentGameObjectHoverd == null) return false;
                
                //Checks if the crops the player is clicking is finished growing
                CropsCycle cropsCycle = player.CharacterPlaceObject.CurrentGameObjectHoverd.GetComponent<CropsCycle>();
                if (cropsCycle != null && cropsCycle.HasFinishedGrowing()) return false;

                //Checks if the crops you are hovering is in the interactable list
                InteractionManager interactionManager = player.CharacterPlaceObject.CurrentGameObjectHoverd.GetComponent<InteractionManager>();
                return player.CharacterInteractionManager.Interactables.Contains(interactionManager);
            }
        }
        return false;
    }
    
    public virtual bool CheckTileUnderObject(Vector3 position, TileType tileType)
    {
        Tile checkTile = tiles[3];
        if (tileType == TileType.DIRT)
            checkTile = tiles[2];
        
        return tileMaps[2].GetTile(grid.WorldToCell(position)) == checkTile;
    }
    
    public virtual void UpdateTile(GameObject crop, TileType tileType)
    {
        Tile setTile = tiles[2];
        Tile checkTile = tiles[3];
        if (tileType == TileType.DIRT)
        {
            setTile = tiles[3];
            checkTile = tiles[2];
        }

        //Checks position on the grid
        Vector3Int pos = grid.WorldToCell(crop.transform.position);

        if (tileMaps[2].GetTile(pos) == checkTile)
            tileMaps[2].SetTile(pos, setTile);
    }
}

public enum TileType
{
    DIRT,
    WATER
}