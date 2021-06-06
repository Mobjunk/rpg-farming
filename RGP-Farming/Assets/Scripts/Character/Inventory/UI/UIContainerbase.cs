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

    public int slotIndex;
    
    //References
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

    //Settings
    private bool isHighlighted;

    public bool IsHighlighted => isHighlighted;

    public void SetHighlighted(bool set)
    {
        isHighlighted = set;
        Highlight.enabled = isHighlighted;
    }
    
    public bool allowMoving;
    
    public virtual void SetContainment(T containment)
    {
        this.containment = containment;
    }

    public virtual void ClearContainment()
    {
        containment = default;
        amount.text = "";
        icon.enabled = false;
        highlight.enabled = false;
        slider.enabled = false;
    }

    private void Start()
    {
        string[] slotIcon = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "0", "-", "=" };
        slotIndex = int.Parse(gameObject.name);
        
        if (slotIndex < slotIcon.Length) slot.text = slotIcon[slotIndex];
        else slot.text = "";
        
        if (isHighlighted) highlight.enabled = true;
    }
    
    //When clicking a item you cannot close the inventory
    //When clicking a item it locks the item to the mouse untill clicked on a slot (In inventory menu)
    //When clickign a item it selects that item (In inventory bar)
    
    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left || eventData.button == PointerEventData.InputButton.Middle)
        {
            if (allowMoving) SnapContainment();
            else SwitchContainment();
        }
    }

    void SwitchContainment()
    {
        
    }

    void SnapContainment()
    {

        if (!ItemSnapperManager.Instance().isSnapped) ItemSnapperManager.Instance().SetSnappedItem(this as UIContainerbase<Item>);
        else
        {
            if (ItemSnapperManager.Instance().currentItemSnapped == this as UIContainerbase<Item>) ItemSnapperManager.Instance().ResetSnappedItem();
            else if (containment == null) ItemSnapperManager.Instance().ResetSnappedItem();
            Player.Instance().CharacterInventory
                .Swap(ItemSnapperManager.Instance().currentItemSnapped.slotIndex, slotIndex);
        }
    }
}