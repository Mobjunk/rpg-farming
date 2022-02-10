using UnityEngine;

public class ShopInteraction : NpcInteraction
{
    private ShopUIManager shopUiManager;

    private void Awake()
    {
        shopUiManager = GetComponent<ShopUIManager>();
    }

    public override void HandleOthers()
    {
        shopUiManager.Interact();
    }
}
