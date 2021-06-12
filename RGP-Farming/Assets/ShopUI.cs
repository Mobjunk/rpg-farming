using UnityEngine;

public class ShopUI : Singleton<ShopUI>
{
    [SerializeField] private GameObject contents;

    public GameObject Contents
    {
        get => contents;
        set => contents = value;
    }

    [SerializeField] private GameObject itemContainer;

    public GameObject ItemContainer
    {
        get => itemContainer;
        set => itemContainer = value;
    }

    [SerializeField] private GameObject[] uiTabs;

    public GameObject[] UiTabs
    {
        get => uiTabs;
        set => uiTabs = value;
    }
}
