using UnityEngine;

public class ChestInteraction : ObjectInteractionManager
{
    public override void OnInteraction(CharacterManager characterManager)
    {
        ChestOpener chestOpener = GetComponent<ChestOpener>();

        if (chestOpener == null)
        {
            Debug.Log("Chest opener is null");
            return;
        }
        
        chestOpener.Interact(characterManager);
    }
}
