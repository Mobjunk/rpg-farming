using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractInventoryUIManger : GameUIManager
{
    private ItemSnapperManager itemSnapper => ItemSnapperManager.Instance();
    
    public delegate void OnInventoryUIClosing();
    public OnInventoryUIClosing onInventoryUIClosing = delegate {  };

    /// <summary>
    /// The different containers with slots
    /// This is needed for the double inventory ui to work
    /// </summary>
    public List<UIContainerbase<Item>>[] containers = new List<UIContainerbase<Item>>[3];

    /// <summary>
    /// The character inventory linked to the UI
    /// </summary>
    private AbstractItemInventory containmentContainer;
    public AbstractItemInventory ContainmentContainer
    {
        get => containmentContainer;
        set => containmentContainer = value;
    }

    /// <summary>
    /// The slot prefab
    /// </summary>
    [Header("Abstract Inventory UI Settings")]
    [SerializeField] private GameObject[] containmentPrefab;

    public GameObject[] ContainmentPrefab
    {
        get => containmentPrefab;
    }
    
    /// <summary>
    /// The main ui that gets activated and hidden
    /// </summary>
    [SerializeField] private GameObject inventoryUI;

    public GameObject InventoryUI
    {
        get => inventoryUI;
        set => inventoryUI = value;
    }
    
    /// <summary>
    /// The different parents that the slots are placed in
    /// </summary>
    [SerializeField] private ParentData[] inventoryContainers;

    public ParentData[] InventoryContainers
    {
        get => inventoryContainers;
    }
    
    public bool isOpened;

    public virtual void Awake()
    {
        //Handles setting up the array of lists
        for (int index = 0; index < containers.Length; index++)
            containers[index] = new List<UIContainerbase<Item>>();
    }

    private void Update()
    {
        //if(Input.GetKeyDown(KeyCode.Escape)) Close();
    }

    /// <summary>
    /// Handles the interaction of this ui
    /// </summary>
    public virtual void Interact()
    {
        if (!isOpened) Open();
        else Close();
    }
    
    /// <summary>
    /// Handles opening the UI
    /// </summary>
    public override void Open()
    {
        base.Open();
        if(itemSnapper.isSnapped) itemSnapper.ResetSnappedItem();
        isOpened = true;
        InventoryUI.SetActive(isOpened);
    }

    /// <summary>
    /// Handles closing the UI
    /// </summary>
    public override void Close()
    {
        if (!isOpened) return;
        base.Close();
        if(itemSnapper.isSnapped) itemSnapper.ResetSnappedItem();
        isOpened = false;
        InventoryUI.SetActive(isOpened);
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
            containers[parentIndex].Clear();
            
            ParentData parent = InventoryContainers[parentIndex];
            for (int index = 0; index < parent.maxSlots; index++)
            {
                GameObject containment = Instantiate(ContainmentPrefab[parent.useSecondSlotPrefab ? 1 : 0], parent.inventoryContainer, true);
                containment.name = $"{index}";
                containment.transform.localScale = new Vector3(1, 1, 1);

                //TODO: ALSO CLEAN THIS SHIT
                UIContainerbase<Item> container = containment.GetComponent<UIContainerbase<Item>>();
                if (container == null) container = containment.GetComponent<AbstractShopContainer>();
                
                //TODO: CLEAN THIS SHIT
                container.Container = pInventory;
                container.SetIndicator(parent.showIdicator);
                container.AllowMoving = parent.allowSnapping;
                container.SetContainment(ContainmentContainer.Items[index]);
                
                containers[parentIndex].Add(container);
            }
        }
    }

    /// <summary>
    /// Handles the updating of a containment
    /// </summary>
    /// <param name="slotsUpdated"></param>
    public virtual void OnInventoryChanged(List<int> pSlotsUpdated)
    {
        foreach (int slot in pSlotsUpdated)
        {
            for (int index = 0; index < containers.Length; index++)
            {
                if (slot >= containers[index].Count) continue;
                containers[index][slot].SetContainment(ContainmentContainer.Items[slot]);
            }
        }
    }
}

[System.Serializable]
public class ParentData
{
    public Transform inventoryContainer;
    public int maxSlots;
    public bool showIdicator;
    public bool useSecondSlotPrefab;
    public bool allowSnapping = true;
}
