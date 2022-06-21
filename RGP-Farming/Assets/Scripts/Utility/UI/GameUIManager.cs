using UnityEngine;
using UnityEngine.EventSystems;

public class GameUIManager : MonoBehaviour
{
    protected Player Player => Player.Instance();

    [Header("Allow to open inventory")]
    [SerializeField] private bool _allowedToOpenInvnetory;

    public bool AllowedToOpenInvnetory
    {
        get => _allowedToOpenInvnetory;
    }
    
    [Header("GameUI Settings")]
    [SerializeField] private GameObject[] _uiTabs;
    
    private int _currentTabId;

    public int CurrentTabId
    {
        get => _currentTabId;
        set => _currentTabId = value;
    }
    
    public GameObject[] UiTabs
    {
        get => _uiTabs;
        set => _uiTabs = value;
    }

    private float _timeSinceInteracting;

    public float TimeSinceInteracting
    {
        get => _timeSinceInteracting;
        set => _timeSinceInteracting = value;
    }
    
    public virtual void Update()
    {
        if (_timeSinceInteracting > 0) _timeSinceInteracting -= Time.deltaTime;
        
        if (CharacterInputManager.Instance().EscapeAction.WasPressedThisFrame() && Player.CharacterUIManager.CurrentUIOpened == this &&  TimeSinceInteracting <= 0 && !DialogueManager.Instance().DialogueIsPlaying)
        {
            Debug.Log("JE MOEDER" + gameObject.name);
            Close();
        }
    }
    
    public virtual void Open()
    {
        Player.CharacterUIManager.CurrentUIOpened = this;
        if (Player.InputEnabled) Player.ToggleInput();
        TimeSinceInteracting = 0.1f;
        Debug.Log("1");
    }

    public virtual void Close()
    {
        _currentTabId = 0;
        ItemTooltipManager.Instance().SetTooltip(null);
        CraftingTooltipManager.Instance().SetTooltip(null);
        CollectionTooltipManager.Instance().SetTooltip(null);
        Player.CharacterUIManager.CurrentUIOpened = null;
        if (!Player.InputEnabled) Player.ToggleInput();
        EventSystem.current.SetSelectedGameObject(null);
        TimeSinceInteracting = 0.1f;
        Debug.Log("2");
    }

    public virtual void Set()
    {
        Player.CharacterUIManager.CurrentUIOpened = this;
    }

    public virtual bool SwitchToTab(int pIndex)
    {
        if (_currentTabId == pIndex)
        {
            Debug.LogError("Cannot switch to the same tab.");
            return false;
        }
        if (pIndex > UiTabs.Length)
        {
            Debug.LogError($"Cannot switch to tab {pIndex} because max is {_uiTabs.Length}!");
            return false;
        }
        _uiTabs[_currentTabId].SetActive(false);
        _currentTabId = pIndex;
        _uiTabs[_currentTabId].SetActive(true);
        return true;
    }
}
