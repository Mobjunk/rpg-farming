using UnityEngine;
using UnityEngine.Tilemaps;

public class TileHover : Singleton<TileHover>
{
    private ItemBarManager _itemBarManager => ItemBarManager.Instance();
    private Player _player => Player.Instance();

    [SerializeField] private Grid _grid;
    [SerializeField] private Tilemap[] _tileMaps;
    [SerializeField] private Tile[] _tiles;

    [SerializeField] private Tilemap[] _tilesToCheck;
    
    private Vector3 mousePosition;
    private Vector3Int tileLocation;
    
    private void Update()
    {
        _tileMaps[0].ClearAllTiles();
       
        bool wearingCorrectTool = _itemBarManager.IsWearingCorrectTools(new[] {ToolType.HOE, ToolType.PICKAXE, ToolType.WATERING_CAN});
        
        if (CursorManager.Instance().IsPointerOverUIElement() || !wearingCorrectTool) return;
        
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        tileLocation = _tileMaps[0].WorldToCell(mousePosition);

        bool canInteract = CanInteract(ToolType.HOE, _tilesToCheck, _tileMaps[1], null) || 
                           CanInteract(ToolType.PICKAXE, _tilesToCheck, _tileMaps[1], _tiles[2]) && _player.CharacterPlaceObject.CurrentGameObjectHoverd == null || 
                           CanInteract(ToolType.WATERING_CAN, _tilesToCheck, _tileMaps[1], _tiles[2], true) && _player.CharacterPlaceObject.CurrentGameObjectHoverd != null && _player.CharacterInventory.Items[_itemBarManager.SelectedSlot].Durability > 0;

        if (!canInteract) return;
        
        _tileMaps[0].SetTile(tileLocation, _tiles[0]);
    }

    private bool CanInteract(ToolType pToolType, Tilemap[] pTilemaps, Tilemap pTilemap, Tile pTile, bool pWateringCan = false)
    {
        if (_itemBarManager.IsWearingCorrectTool(pToolType) && Utility.CanInteractWithTile(_grid, tileLocation, _player.TileChecker))
        {
            bool canInteract = pTilemap.GetTile(pTilemap.WorldToCell(mousePosition)) == pTile;
            foreach (Tilemap tilemap in pTilemaps)
            {
                if (tilemap.GetTile(tilemap.WorldToCell(mousePosition)) != null)
                {
                    canInteract = false;
                    break;
                }
            }

            if (canInteract)
            {
                if (!pWateringCan) return true;

                if (_player.CharacterPlaceObject.CurrentGameObjectHoverd == null) return false;
                
                //Checks if the crops the player is clicking is finished growing
                CropsGrowManager cropsGrowManager = _player.CharacterPlaceObject.CurrentGameObjectHoverd.GetComponent<CropsGrowManager>();
                if (cropsGrowManager != null && cropsGrowManager.ReadyToHarvest) return false;

                //Checks if the crops you are hovering is in the interactable list
                InteractionManager interactionManager = _player.CharacterPlaceObject.CurrentGameObjectHoverd.GetComponent<InteractionManager>();
                return _player.CharacterInteractionManager.GetInteractables().Contains(interactionManager);
            }
            /*if (pTilemap.GetTile(pTilemap.WorldToCell(mousePosition)) == pTile)
            {
                if (!pWateringCan) return true;

                if (_player.CharacterPlaceObject.CurrentGameObjectHoverd == null) return false;
                
                //Checks if the crops the player is clicking is finished growing
                CropsGrowManager cropsGrowManager = _player.CharacterPlaceObject.CurrentGameObjectHoverd.GetComponent<CropsGrowManager>();
                if (cropsGrowManager != null && cropsGrowManager.ReadyToHarvest) return false;

                //Checks if the crops you are hovering is in the interactable list
                InteractionManager interactionManager = _player.CharacterPlaceObject.CurrentGameObjectHoverd.GetComponent<InteractionManager>();
                return _player.CharacterInteractionManager.GetInteractables().Contains(interactionManager);
            }*/
        }
        return false;
    }
}
