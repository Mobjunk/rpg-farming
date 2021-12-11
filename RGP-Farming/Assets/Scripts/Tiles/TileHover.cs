using UnityEngine;
using UnityEngine.Tilemaps;

public class TileHover : Singleton<TileHover>
{
    private ItemBarManager itemBarManager => ItemBarManager.Instance();
    private Player player => Player.Instance();

    [SerializeField] private Grid grid;
    [SerializeField] private Tilemap[] tileMaps;
    [SerializeField] private Tile[] tiles;

    private Vector3 mousePosition;
    private Vector3Int tileLocation;
    
    private void Update()
    {
        tileMaps[0].ClearAllTiles();
       
        bool wearingCorrectTool = itemBarManager.IsWearingCorrectTools(new[] {ToolType.HOE, ToolType.PICKAXE, ToolType.WATERING_CAN});
        if (CursorManager.Instance().IsPointerOverUIElement() || !wearingCorrectTool) return;
        
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        tileLocation = tileMaps[0].WorldToCell(mousePosition);

        bool canInteract = CanInteract(ToolType.HOE, tileMaps[2], null) || 
                           CanInteract(ToolType.PICKAXE, tileMaps[2], tiles[2]) && player.CharacterPlaceObject.CurrentGameObjectHoverd == null || 
                           CanInteract(ToolType.WATERING_CAN, tileMaps[2], tiles[2], true) && player.CharacterPlaceObject.CurrentGameObjectHoverd != null && player.CharacterInventory.items[itemBarManager.selectedSlot].durability > 0;
        
        tileMaps[0].SetTile(tileLocation, canInteract ? tiles[0] : tiles[1]);
    }

    private bool CanInteract(ToolType toolType, Tilemap tilemap, Tile tile, bool wateringCan = false)
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
                return player.CharacterInteractionManager.GetInteractables().Contains(interactionManager);
            }
        }
        return false;
    }
}
