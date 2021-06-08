using UnityEngine;

public class ChestInteraction : ObjectInteractionManager
{
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
