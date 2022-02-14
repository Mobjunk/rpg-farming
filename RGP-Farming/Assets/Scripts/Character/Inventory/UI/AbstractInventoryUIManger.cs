using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractInventoryUIManger : GameUIManager
{
    private ItemSnapperManager _itemSnapper => ItemSnapperManager.Instance();
    
    public delegate void OnInventoryUIClosing();
    public OnInventoryUIClosing onInventoryUIClosing = delegate {  };

    /// <summary>
    /// The different containers with slots
    /// This is needed for the double inventory ui to work
    /// </summary>
    public List<UIContainerbase<GameItem>>[] _containers = new List<UIContainerbase<GameItem>>[3];

    /// <summary>
    /// The character inventory linked to the UI
    /// </summary>
    private AbstractItemInventory _containmentContainer;
    public AbstractItemInventory ContainmentContainer
    {
        get => _containmentContainer;
        set => _containmentContainer = value;
    }

    /// <summary>
    /// The slot prefab
    /// </summary>
    [Header("Abstract Inventory UI Settings")]
    [SerializeField] private GameObject[] _containmentPrefab;

    public GameObject[] ContainmentPrefab
    {
        get => _containmentPrefab;
    }
    
    /// <summary>
    /// The main ui that gets activated and hidden
    /// </summary>
    [SerializeField] private GameObject _inventoryUI;

    public GameObject InventoryUI
    {
        get => _inventoryUI;
        set => _inventoryUI = value;
    }
    
    /// <summary>
    /// The different parents that the slots are placed in
    /// </summary>
    [SerializeField] private ParentData[] _inventoryContainers;

    public ParentData[] InventoryContainers
    {
        get => _inventoryContainers;
    }
    
    public bool IsOpened;

    public virtual void Awake()
    {
        //Handles setting up the array of lists
        for (int index = 0; index < _containers.Length; index++)
            _containers[index] = new List<UIContainerbase<GameItem>>();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)) Close();
    }

    /// <summary>
    /// Handles the interaction of this ui
    /// </summary>
    public virtual void Interact()
    {
        if (!IsOpened) Open();
        else Close();
    }
    
    /// <summary>
    /// Handles opening the UI
    /// </summary>
    public override void Open()
    {
        base.Open();
        if(_itemSnapper.IsSnapped) _itemSnapper.ResetSnappedItem();
        IsOpened = true;
        InventoryUI.SetActive(IsOpened);
    }

    /// <summary>
    /// Handles closing the UI
    /// </summary>
    public override void Close()
    {
        if (!IsOpened) return;
        base.Close();
        if(_itemSnapper.IsSnapped) _itemSnapper.ResetSnappedItem();
        IsOpened = false;
        InventoryUI.SetActive(IsOpened);
        onInventoryUIClosing.Invoke();
    }

    /// <summary>
    /// Initialze the container
    /// </summary>
    /// <param name="pInventory">The character inventory</param>
    public virtual void Initialize(AbstractItemInventory pInventory)
    {
        ContainmentContainer = pInventory;
        ContainmentContainer.onInventoryChanged += OnInventoryChanged;

        //Handles setting up the container
        for (int parentIndex = 0; parentIndex < InventoryContainers.Length; parentIndex++)
        {
            _containers[parentIndex].Clear();
            
            ParentData parent = InventoryContainers[parentIndex];
            for (int index = 0; index < parent.MaxSlots; index++)
            {
                GameObject containment = Instantiate(ContainmentPrefab[parent.UseSecondSlotPrefab ? 1 : 0], parent.InventoryContainer, true);
                containment.name = $"{index}";
                containment.transform.localScale = new Vector3(1, 1, 1);

                //TODO: ALSO CLEAN THIS SHIT
                UIContainerbase<GameItem> container = containment.GetComponent<UIContainerbase<GameItem>>();
                if (container == null) container = containment.GetComponent<AbstractShopContainer>();
                
                //TODO: CLEAN THIS SHIT
                container.Container = pInventory;
                container.SetIndicator(parent.ShowIdicator);
                container.AllowMoving = parent.AllowSnapping;
                container.SetContainment(ContainmentContainer.Items[index]);
                
                _containers[parentIndex].Add(container);
            }
        }
    }

    /// <summary>
    /// Handles the updating of a containment
    /// </summary>
    /// <param name="pSlotsUpdated"></param>
    public virtual void OnInventoryChanged(List<int> pSlotsUpdated)
    {
        foreach (int slot in pSlotsUpdated)
        {
            for (int index = 0; index < _containers.Length; index++)
            {
                if (slot >= _containers[index].Count) continue;
                if (ContainmentContainer.Items[slot].Item == null) _containers[index][slot].ClearContainment();
                else _containers[index][slot].SetContainment(ContainmentContainer.Items[slot]);
            }
        }
        
    }
}

[System.Serializable]
public class ParentData
{
    public Transform InventoryContainer;
    public int MaxSlots;
    public bool ShowIdicator;
    public bool UseSecondSlotPrefab;
    public bool AllowSnapping = true;
}
