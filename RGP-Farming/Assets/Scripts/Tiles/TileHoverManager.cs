using UnityEngine;

public class TileHoverManager : TileInteractionManager<TileHoverManager>
{
    public override void Update()
    {
        base.Update();
        
        bool wearingCorrectTool = itemBarManager.IsWearingCorrectTools(new[] {ToolType.HOE, ToolType.PICKAXE, ToolType.WATERING_CAN});
        if (CursorManager.Instance().IsPointerOverUIElement() || !wearingCorrectTool) return;
        
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        tileLocation = tileMaps[0].WorldToCell(mousePosition);

        bool canInteract = CanInteract(ToolType.HOE, tileMaps[2], null) || 
                           CanInteract(ToolType.PICKAXE, tileMaps[2], tiles[2]) && player.CharacterPlaceObject.CurrentGameObjectHoverd == null || 
                           CanInteract(ToolType.WATERING_CAN, tileMaps[2], tiles[2], true) && player.CharacterPlaceObject.CurrentGameObjectHoverd != null && player.CharacterInventory.Items[itemBarManager.selectedSlot].durability > 0;
        
        tileMaps[0].SetTile(tileLocation, canInteract ? tiles[0] : tiles[1]);
    }
}
