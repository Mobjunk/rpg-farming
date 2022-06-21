using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerInvenotryUIManager : AbstractInventoryUIManger
{
    [SerializeField] private CraftingInventoryUIManager _craftingInventoryUIManager;
    
    public override void Open()
    {
        if (CharacterInputManager.Instance().GamepadActive)
        {
            GameObject gObject = InventoryUI[1].transform.GetChild(0).gameObject;
            EventSystem.current.SetSelectedGameObject(gObject);
        }

        base.Open();
        InventoryUI[1].SetActive(true);
        _craftingInventoryUIManager.CurrentTabId = 0;
        _craftingInventoryUIManager.ClearCraftingChildren();
    }
}