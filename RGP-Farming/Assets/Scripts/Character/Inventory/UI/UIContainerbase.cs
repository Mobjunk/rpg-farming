using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[Serializable]
public abstract class UIContainerbase<T> : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
{
    /// <summary>
    /// The containment
    /// </summary>
    public T Containment;

    /// <summary>
    /// The index of the slot
    /// </summary>
    [HideInInspector] public int SlotIndex;
    
    //References
    #region References

    public AbstractItemInventory Container;
    
    /// <summary>
    /// Slot text of the containment
    /// </summary>
    public TextMeshProUGUI Slot;

    /// <summary>
    /// Amount text of the containment
    /// </summary>
    public TextMeshProUGUI Amount;

    /// <summary>
    /// Icon of the containment
    /// </summary>
    public Image Icon;

    /// <summary>
    /// Highlight of the containment
    /// </summary>
    public Image Highlight;

    /// <summary>
    /// Slider of the containment
    /// </summary>
    public Slider Slider;

    #endregion

    //Settings
    #region Settings
    
    /// <summary>
    /// Handles the highlight of a slot in use
    /// </summary>
    private bool isHighlighted;

    public void SetHighlighted(bool pSet)
    {
        isHighlighted = pSet;
        Highlight.enabled = isHighlighted;
    }

    /// <summary>
    /// Indicator of what key is used to select the slot
    /// </summary>
    public bool ShowIndicator;

    public void SetIndicator(bool pSet)
    {
        ShowIndicator = pSet;
        if (SlotIndex < slotIcon.Length && ShowIndicator) Slot.text = slotIcon[SlotIndex];
        else
        {
            if(Slot == null) Debug.Log($"Slot is null for {gameObject.name}");
            Slot.text = "";
        }
    }
    
    /// <summary>
    /// To check if the slot is allowed to be moved
    /// </summary>
    public bool AllowMoving;

    #endregion
    
    private bool _hoveringContainment;

    private void Update()
    {
        if (_hoveringContainment)
        {
            var iconTransform = Icon.transform;
            bool increase = iconTransform.localScale.x < 1.1f;
            if(increase)
            {
                var localScale = iconTransform.localScale;
                iconTransform.localScale = new Vector3(localScale.x + 0.01f, localScale.y + 0.01f, localScale.z);
            }
        }
    }

    /// <summary>
    /// Handles setting the containment
    /// </summary>
    /// <param name="pContainment"></param>
    public virtual void SetContainment(T pContainment)
    {
        this.Containment = pContainment;
    }

    /// <summary>
    /// Handles clearing the containment
    /// </summary>
    public virtual void ClearContainment()
    {
        Containment = default;
        Amount.text = "";
        Icon.enabled = false;
        if(Highlight != null) Highlight.enabled = false;
        if(Slider != null) Slider.gameObject.SetActive(false);
    }

    string[] slotIcon = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "0", "-", "=" };
    private void Start()
    {
        //slotIndex = ;
        if (!int.TryParse(gameObject.name, out SlotIndex))
        {
            Debug.Log($"Cannot try parse {gameObject.name} to int...");
        }
        
        if (SlotIndex < slotIcon.Length && ShowIndicator) Slot.text = slotIcon[SlotIndex];
        else Slot.text = "";
        
        if (isHighlighted) Highlight.enabled = true;
    }

    /// <summary>
    /// Handles switching to a containment
    /// </summary>
    void SwitchContainment()
    {
        ItemBarManager.Instance().UpdateSlot(SlotIndex);
    }

    /// <summary>
    /// Handles the item snapping to the mouse when clicking a containment
    /// </summary>
    void SnapContainment()
    {
        ItemSnapperManager snapperManager = ItemSnapperManager.Instance();

        if (!snapperManager.isSnapped) snapperManager.SetSnappedItem(this as UIContainerbase<Item>);
        else
        {
            UIContainerbase<Item> currentSnap = snapperManager.currentItemSnapped;
            //Handles resetting snapping
            if (currentSnap == this as UIContainerbase<Item> || Containment == null) snapperManager.ResetSnappedItem();

            //Creates a placeholder of the current item
            Item placeHolder = new Item(Container.Items[SlotIndex].item, Container.Items[SlotIndex].amount);
            Item currentItem = currentSnap.Container.Items[currentSnap.SlotIndex];

            if (placeHolder.item == currentItem.item && SlotIndex != currentSnap.SlotIndex) //
            {
                Item updatedItem = new Item(currentItem.item, currentItem.amount + placeHolder.amount);
                Container.Set(SlotIndex, updatedItem); 
                currentSnap.Container.Set(currentSnap.SlotIndex, null);
                snapperManager.ResetSnappedItem(false);
            } else {
                //Handles updating the containers
                Container.Set(SlotIndex, currentItem);
                currentSnap.Container.Set(currentSnap.SlotIndex, placeHolder);
            }
        }
    }

    public virtual void PlaceSingleItem()
    {
        ItemSnapperManager snapperManager = ItemSnapperManager.Instance();
        
        UIContainerbase<Item> currentSnap = snapperManager.currentItemSnapped;
        if (currentSnap == this as UIContainerbase<Item>)
        {
            Debug.Log("Cannot do this function on the same item you got snapped...");
            return;
        }
        Item currentItem = currentSnap.Container.Items[currentSnap.SlotIndex];
        Item clickedItem = Container.Items[SlotIndex];
        if (Containment == null || currentItem.item == clickedItem.item)
        {
            currentItem.SetAmount(currentItem.amount - 1);
            currentSnap.Container.Set(currentSnap.SlotIndex, currentItem);
            Container.Set(SlotIndex, new Item(currentItem.item, clickedItem.amount + 1));
            if (currentItem.amount <= 0) snapperManager.ResetSnappedItem(false);
        }
    }

    public virtual void SplitItemStack()
    {
        ItemSnapperManager snapperManager = ItemSnapperManager.Instance();
        
        UIContainerbase<Item> clickedItem = this as UIContainerbase<Item>;
        Debug.Log("clickedItem: " + clickedItem.Containment);
        
        /*Item currentItem = Container.items[slotIndex];
        int half = Mathf.FloorToInt(currentItem.amount / 2);
        
        currentItem.SetAmount(currentItem.amount - half);
        Container.Set(slotIndex, currentItem);
        
        Debug.Log("half: " + half);*/
    }
    
    public virtual void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left || eventData.button == PointerEventData.InputButton.Middle)
        {
            if (AllowMoving) SnapContainment();
            else SwitchContainment();
        } else if (eventData.button == PointerEventData.InputButton.Right && AllowMoving)
        {
            ItemSnapperManager snapperManager = ItemSnapperManager.Instance();
            if (snapperManager.isSnapped) PlaceSingleItem();
            else SplitItemStack();
        }
    }

    public virtual void OnPointerEnter(PointerEventData eventData)
    {
        _hoveringContainment = true;
    }

    public virtual void OnPointerExit(PointerEventData eventData)
    {
        _hoveringContainment = false;
        Icon.transform.localScale = new Vector3(1, 1, 1);
    }
}