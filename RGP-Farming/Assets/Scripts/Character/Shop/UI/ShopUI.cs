using UnityEngine;

public class ShopUI : Singleton<ShopUI>
{
    [SerializeField] private GameObject _contents;

    public GameObject Contents
    {
        get => _contents;
        set => _contents = value;
    }

    [SerializeField] private GameObject _itemContainer;

    public GameObject ItemContainer
    {
        get => _itemContainer;
        set => _itemContainer = value;
    }

    [SerializeField] private GameObject[] _uiTabs;

    public GameObject[] UiTabs
    {
        get => _uiTabs;
        set => _uiTabs = value;
    }
}
