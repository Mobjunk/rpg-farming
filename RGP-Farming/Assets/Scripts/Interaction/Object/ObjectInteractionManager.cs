using UnityEngine;

public class ObjectInteractionManager : InteractionManager
{
    public override void OnInteraction(CharacterManager characterManager)
    {
        base.OnInteraction(characterManager);
        Debug.Log("Object interaction...");
    }
}
