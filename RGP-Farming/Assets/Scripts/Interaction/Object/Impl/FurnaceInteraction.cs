using System;

public class FurnaceInteraction : ObjectInteractionManager
{
    private FurnaceUIManager furnaceUIManager;

    private void Awake()
    {
        furnaceUIManager = GetComponent<FurnaceUIManager>();
    }

    public override void OnInteraction(CharacterManager characterManager)
    {
        furnaceUIManager.Interact();
    }
}
