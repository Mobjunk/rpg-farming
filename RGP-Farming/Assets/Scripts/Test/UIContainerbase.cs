using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[Serializable]
public abstract class UIContainerbase<T> : MonoBehaviour, IPointerDownHandler, IDragHandler
{
    /// <summary>
    /// The containment
    /// </summary>
    [SerializeReference] protected T containment;

    public T Containment => containment;
    
    //References
    /// <summary>
    /// Slot text of the containment
    /// </summary>
    [SerializeField] private TextMeshProUGUI slot;

    public TextMeshProUGUI Slot
    {
        get => slot;
    }
    
    /// <summary>
    /// Amount text of the containment
    /// </summary>
    [SerializeField] private TextMeshProUGUI amount;

    public TextMeshProUGUI Amount
    {
        get => amount;
    }
    
    /// <summary>
    /// Icon of the containment
    /// </summary>
    [SerializeField] private Image icon;

    public Image Icon
    {
        get => icon;
    }
    
    /// <summary>
    /// Highlight of the containment
    /// </summary>
    [SerializeField] private Image highlight;

    public Image Highlight
    {
        get => highlight;
    }
    
    /// <summary>
    /// Slider of the containment
    /// </summary>
    [SerializeField] private Slider slider;

    public Slider Slider
    {
        get => slider;
    }
    
    //Settings
    public bool isHighlighted;
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
        int slotIndex = int.Parse(gameObject.name);
        
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
            if (containment == null) return;

            if (allowMoving) SnapContainment();
            else SwitchContainment();
        }
    }

    void SwitchContainment()
    {
        
    }

    void SnapContainment()
    {
        
    }

    public void OnDrag(PointerEventData eventData)
    {
        
    }
}