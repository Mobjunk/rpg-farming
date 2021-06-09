using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractInventoryUIManger : MonoBehaviour
{
    private ItemSnapperManager itemSnapper => ItemSnapperManager.Instance();
    
    public delegate void OnInventoryUIClosing();
    public OnInventoryUIClosing onInventoryUIClosing = delegate {  };

    /// <summary>
    /// The different containers with slots
    /// This is needed for the double inventory ui to work
    /// </summary>
    public List<AbstractItemContainer>[] containers = new List<AbstractItemContainer>[2];

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
    [SerializeField] private GameObject containmentPrefab;

    public GameObject ContainmentPrefab
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

    private void Awake()
    {
        //Handles setting up the array of lists
        for (int index = 0; index < containers.Length; index++)
            containers[index] = new List<AbstractItemContainer>();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)) Close();
    }

    /// <summary>
    /// Handles opening the UI
    /// </summary>
    public virtual void Open()
    {
        if(itemSnapper.isSnapped) itemSnapper.ResetSnappedItem();
        isOpened = true;
        InventoryUI.SetActive(isOpened);
    }

    /// <summary>
    /// Handles closing the UI
    /// </summary>
    public virtual void Close()
    {
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

        if (ContainmentPrefab == null)
        {
            Debug.LogError("ContainmentPrefab = null");
            return;
        }

        if (InventoryContainers == null)
        {
            Debug.LogError("InventoryContainer = null");
            return;
        }

        //Handles setting up the container
        for (int parentIndex = 0; parentIndex < InventoryContainers.Length; parentIndex++)
        {
            ParentData parent = InventoryContainers[parentIndex];
            for (int index = 0; index < parent.maxSlots; index++)
            {
                GameObject containment = Instantiate(ContainmentPrefab, parent.inventoryContainer, true);
                containment.name = $"{index}";
                containment.transform.localScale = new Vector3(1, 1, 1);

                AbstractItemContainer container = containment.GetComponent<AbstractItemContainer>();

                //TODO: CLEAN THIS SHIT
                container.Container = pInventory;
                container.SetIndicator(parent.showIdicator);
                container.AllowMoving = parent.maxSlots != 12;
                container.SetContainment(ContainmentContainer.items[index]);
                
                containers[parentIndex].Add(container);
            }
        }
    }

    /// <summary>
    /// Handles the updating of a containment
    /// </summary>
    /// <param name="slotsUpdated"></param>
    private void OnInventoryChanged(List<int> slotsUpdated)
    {
        foreach (int slot in slotsUpdated)
        {
            //Debug.LogWarning($"Slot {slot} needs updating");
            for (int index = 0; index < containers.Length; index++)
            {
                if (slot >= containers[index].Count) continue;
                containers[index][slot].SetContainment(ContainmentContainer.items[slot]);
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
}
