using UnityEngine;

public class InventoryMenuManager : MenuManager<InventoryMenuManager>
{
    public GameObject InventoryPanel;

    public override void Hide(bool pUnhideButtons = false)
    {
        base.Hide(pUnhideButtons);
        InventoryPanel.SetActive(false);
    }

    public override void Unhide(bool pHideButtons = false)
    {
        base.Unhide(pHideButtons);
        InventoryPanel.SetActive(true);
    }
}
