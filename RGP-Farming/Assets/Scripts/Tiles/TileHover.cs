using UnityEngine;
using UnityEngine.Tilemaps;

public class TileHover : Singleton<TileHover>
{
    private ItemBarManager _itemBarManager => ItemBarManager.Instance();
    private Player _player => Player.Instance();

    [SerializeField] private Grid _grid;
    [SerializeField] private Tilemap[] _tileMaps;
    [SerializeField] private Tile[] _tiles;

    private Vector3 mousePosition;
    private Vector3Int tileLocation;
    
    private void Update()
    {
        _tileMaps[0].ClearAllTiles();
       
        bool wearingCorrectTool = _itemBarManager.IsWearingCorrectTools(new[] {ToolType.HOE, ToolType.PICKAXE, ToolType.WATERING_CAN});
        
        if (CursorManager.Instance().IsPointerOverUIElement() || !wearingCorrectTool) return;
        
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        tileLocation = _tileMaps[0].WorldToCell(mousePosition);

        bool canInteract = CanInteract(ToolType.HOE, _tileMaps[1], null) || 
                           CanInteract(ToolType.PICKAXE, _tileMaps[1], _tiles[2]) && _player.CharacterPlaceObject.CurrentGameObjectHoverd == null || 
                           CanInteract(ToolType.WATERING_CAN, _tileMaps[1], _tiles[2], true) && _player.CharacterPlaceObject.CurrentGameObjectHoverd != null && _player.CharacterInventory.Items[_itemBarManager.SelectedSlot].Durability > 0;
        
        _tileMaps[0].SetTile(tileLocation, canInteract ? _tiles[0] : _tiles[1]);
    }

    private bool CanInteract(ToolType pToolType, Tilemap pTilemap, Tile pTile, bool pWateringCan = false)
    {
        if (_itemBarManager.IsWearingCorrectTool(pToolType) && Utility.CanInteractWithTile(_grid, tileLocation, _player.TileChecker))
        {
            if (pTilemap.GetTile(pTilemap.WorldToCell(mousePosition)) == pTile)
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
        }
        return false;
    }
}
