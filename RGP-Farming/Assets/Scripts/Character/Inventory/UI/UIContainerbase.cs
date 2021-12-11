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
    [SerializeReference] protected T containment;

    public T Containment => containment;

    /// <summary>
    /// The index of the slot
    /// </summary>
    [HideInInspector] public int slotIndex;
    
    //References
    #region References

    private AbstractItemInventory container;

    public AbstractItemInventory Container
    {
        get => container;
        set => container = value;
    }
    
    /// <summary>
    /// Slot text of the containment
    /// </summary>
    [SerializeField] private TextMeshProUGUI slot;

    public TextMeshProUGUI Slot => slot;

    /// <summary>
    /// Amount text of the containment
    /// </summary>
    [SerializeField] private TextMeshProUGUI amount;

    public TextMeshProUGUI Amount => amount;

    /// <summary>
    /// Icon of the containment
    /// </summary>
    [SerializeField] private Image icon;

    public Image Icon => icon;

    /// <summary>
    /// Highlight of the containment
    /// </summary>
    [SerializeField] private Image highlight;

    public Image Highlight => highlight;

    /// <summary>
    /// Slider of the containment
    /// </summary>
    [SerializeField] private Slider slider;

    public Slider Slider => slider;

    #endregion

    //Settings
    #region Settings
    
    /// <summary>
    /// Handles the highlight of a slot in use
    /// </summary>
    private bool isHighlighted;

    public void SetHighlighted(bool set)
    {
        isHighlighted = set;
        Highlight.enabled = isHighlighted;
    }

    /// <summary>
    /// Indicator of what key is used to select the slot
    /// </summary>
    private bool showIndicator;

    public bool ShowIndicator => showIndicator;

    public void SetIndicator(bool set)
    {
        showIndicator = set;
        if (slotIndex < slotIcon.Length && ShowIndicator) slot.text = slotIcon[slotIndex];
        else slot.text = "";
    }
    
    /// <summary>
    /// To check if the slot is allowed to be moved
    /// </summary>
    private bool allowMoving;

    public bool AllowMoving
    {
        get => allowMoving;
        set => allowMoving = value;
    }

    #endregion
    
    private bool hoveringContainment;

    private void Update()
    {
        if (hoveringContainment)
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
    /// <param name="containment"></param>
    public virtual void SetContainment(T containment)
    {
        this.containment = containment;
    }

    /// <summary>
    /// Handles clearing the containment
    /// </summary>
    public virtual void ClearContainment()
    {
        containment = default;
        amount.text = "";
        icon.enabled = false;
        if(highlight != null) highlight.enabled = false;
        if(slider != null) slider.gameObject.SetActive(false);
    }

    string[] slotIcon = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "0", "-", "=" };
    private void Start()
    {
        //slotIndex = ;
        if (!int.TryParse(gameObject.name, out slotIndex))
        {
            Debug.Log($"Cannot try parse {gameObject.name} to int...");
        }
        
        if (slotIndex < slotIcon.Length && ShowIndicator) slot.text = slotIcon[slotIndex];
        else slot.text = "";
        
        if (isHighlighted) highlight.enabled = true;
    }

    /// <summary>
    /// Handles switching to a containment
    /// </summary>
    void SwitchContainment()
    {
        ItemBarManager.Instance().UpdateSlot(slotIndex);
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
            if (currentSnap == this as UIContainerbase<Item> || containment == null) snapperManager.ResetSnappedItem();

            //Creates a placeholder of the current item
            Item placeHolder = new Item(Container.items[slotIndex].item, Container.items[slotIndex].amount);
            Item currentItem = currentSnap.Container.items[currentSnap.slotIndex];

            if (placeHolder.item == currentItem.item && slotIndex != currentSnap.slotIndex) //
            {
                Item updatedItem = new Item(currentItem.item, currentItem.amount + placeHolder.amount);
                Container.Set(slotIndex, updatedItem); 
                currentSnap.Container.Set(currentSnap.slotIndex, null);
                snapperManager.ResetSnappedItem(false);
            } else {
                //Handles updating the containers
                Container.Set(slotIndex, currentItem);
                currentSnap.Container.Set(currentSnap.slotIndex, placeHolder);
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
        Item currentItem = currentSnap.Container.items[currentSnap.slotIndex];
        Item clickedItem = Container.items[slotIndex];
        if (containment == null || currentItem.item == clickedItem.item)
        {
            currentItem.SetAmount(currentItem.amount - 1);
            currentSnap.Container.Set(currentSnap.slotIndex, currentItem);
            Container.Set(slotIndex, new Item(currentItem.item, clickedItem.amount + 1));
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
            if (allowMoving) SnapContainment();
            else SwitchContainment();
        } else if (eventData.button == PointerEventData.InputButton.Right && allowMoving)
        {
            ItemSnapperManager snapperManager = ItemSnapperManager.Instance();
            if (snapperManager.isSnapped) PlaceSingleItem();
            else SplitItemStack();
        }
    }

    public virtual void OnPointerEnter(PointerEventData eventData)
    {
        hoveringContainment = true;
    }

    public virtual void OnPointerExit(PointerEventData eventData)
    {
        hoveringContainment = false;
        Icon.transform.localScale = new Vector3(1, 1, 1);
    }
}