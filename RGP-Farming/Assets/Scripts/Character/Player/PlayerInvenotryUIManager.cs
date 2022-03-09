using UnityEngine;

public class PlayerInvenotryUIManager : AbstractInventoryUIManger
{
    [SerializeField] private CraftingInventoryUIManager _craftingInventoryUIManager;
    
    public override void Open()
    {
        base.Open();
        InventoryUI[1].SetActive(true);
        _craftingInventoryUIManager.CurrentTabId = 0;
        _craftingInventoryUIManager.ClearCraftingChildren();
    }
}