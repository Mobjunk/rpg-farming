using UnityEngine;

public class GameUIManager : MonoBehaviour
{
    private Player player => Player.Instance();
    
    [Header("GameUI Settings")]
    [SerializeField] private GameObject[] uiTabs;
    [SerializeField] private int currentTabId;
    
    public GameObject[] UiTabs
    {
        get => uiTabs;
        set => uiTabs = value;
    }

    public virtual void Open()
    {
        player.CharacterUIManager.CurrentUIOpened = this;
    }

    public virtual void Close()
    {
        player.CharacterUIManager.CurrentUIOpened = null;
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
