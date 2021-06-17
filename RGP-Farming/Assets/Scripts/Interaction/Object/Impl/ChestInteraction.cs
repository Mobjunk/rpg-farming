using UnityEngine;

public class ChestInteraction : ObjectInteractionManager
{
    public override void OnInteraction(CharacterManager characterManager)
    {
        base.OnInteraction(characterManager);

        ChestInventory chestInventory = GetComponent<ChestInventory>();
        if (chestInventory.SlotsOccupied() > 0) return;

        if (ItemBarManager.Instance().IsWearingCorrectTools(new[] { ToolType.AXE, ToolType.HOE, ToolType.PICKAXE }))
        {
            AbstractPlaceableItem placeableItem = (AbstractPlaceableItem) ItemManager.Instance().ForName("Chest");
            GroundItemsManager.Instance().Add(new Item(placeableItem), gameObject.transform.position);
            DestroyObject(gameObject, placeableItem);
        }
    }

    public override void OnSecondaryInteraction(CharacterManager characterManager)
    {
        base.OnSecondaryInteraction(characterManager);
        
        ChestOpener chestOpener = GetComponent<ChestOpener>();

        if (chestOpener == null)
        {
            Debug.Log("Chest opener is null");
            return;
        }
        
        chestOpener.Interact(characterManager);
    }
}
