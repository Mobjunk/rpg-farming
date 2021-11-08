using UnityEngine;

public class GameUIManager : MonoBehaviour
{
    private Player player => Player.Instance();

    [Header("Allow to open inventory")]
    [SerializeField] private bool allowedToOpenInvnetory;

    public bool AllowedToOpenInvnetory
    {
        get => allowedToOpenInvnetory;
    }
    
    [Header("GameUI Settings")]
    [SerializeField] private GameObject[] uiTabs;
    private int currentTabId;
    
    public GameObject[] UiTabs
    {
        get => uiTabs;
        set => uiTabs = value;
    }

    public virtual void Open()
    {
        player.CharacterUIManager.CurrentUIOpened = this;
        if (player.CharacterControllerManager.InputEnabled) player.CharacterControllerManager.ToggleInput();
    }

    public virtual void Close()
    {
        ItemTooltipManager.Instance().SetTooltip(null);
        CraftingTooltipManager.Instance().SetTooltip(null);
        player.CharacterUIManager.CurrentUIOpened = null;
        if (!player.CharacterControllerManager.InputEnabled) player.CharacterControllerManager.ToggleInput();
    }

    public virtual void Set()
    {
        player.CharacterUIManager.CurrentUIOpened = this;
    }

    public void SwitchToTab(int index)
    {
        if (currentTabId == index)
        {
            Debug.LogError("Cannot switch to the same tab.");
            return;
        }
        if (index > UiTabs.Length)
        {
            Debug.LogError($"Cannot switch to tab {index} because max is {uiTabs.Length}!");
            return;
        }
        uiTabs[currentTabId].SetActive(false);
        currentTabId = index;
        uiTabs[currentTabId].SetActive(true);
    }
}
