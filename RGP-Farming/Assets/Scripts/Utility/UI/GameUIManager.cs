using UnityEngine;

public class GameUIManager : MonoBehaviour
{
    private Player _player => Player.Instance();

    [Header("Allow to open inventory")]
    [SerializeField] private bool _allowedToOpenInvnetory;

    public bool AllowedToOpenInvnetory
    {
        get => _allowedToOpenInvnetory;
    }
    
    [Header("GameUI Settings")]
    [SerializeField] private GameObject[] _uiTabs;
    private int _currentTabId;
    
    public GameObject[] UiTabs
    {
        get => _uiTabs;
        set => _uiTabs = value;
    }

    public virtual void Open()
    {
        _player.CharacterUIManager.CurrentUIOpened = this;
        if (_player.InputEnabled) _player.ToggleInput();
    }

    public virtual void Close()
    {
        ItemTooltipManager.Instance().SetTooltip(null);
        CraftingTooltipManager.Instance().SetTooltip(null);
        _player.CharacterUIManager.CurrentUIOpened = null;
        if (!_player.InputEnabled) _player.ToggleInput();
    }

    public virtual void Set()
    {
        _player.CharacterUIManager.CurrentUIOpened = this;
    }

    public void SwitchToTab(int pIndex)
    {
        if (_currentTabId == pIndex)
        {
            Debug.LogError("Cannot switch to the same tab.");
            return;
        }
        if (pIndex > UiTabs.Length)
        {
            Debug.LogError($"Cannot switch to tab {pIndex} because max is {_uiTabs.Length}!");
            return;
        }
        _uiTabs[_currentTabId].SetActive(false);
        _currentTabId = pIndex;
        _uiTabs[_currentTabId].SetActive(true);
    }
}
