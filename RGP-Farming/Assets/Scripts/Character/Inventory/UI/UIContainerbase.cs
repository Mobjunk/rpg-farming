using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[Serializable]
public abstract class UIContainerbase<T> : MonoBehaviour, IPointerDownHandler
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
        if(slider != null) slider.enabled = false;
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
    
    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left || eventData.button == PointerEventData.InputButton.Middle)
        {
            if (allowMoving) SnapContainment();
            else SwitchContainment();
        }
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
            
            //Handles updating the containers
            Container.Set(slotIndex, currentItem);
            currentSnap.Container.Set(currentSnap.slotIndex, placeHolder);
        }
    }
}