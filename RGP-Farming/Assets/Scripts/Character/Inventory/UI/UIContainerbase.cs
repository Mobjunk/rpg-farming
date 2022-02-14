using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[Serializable]
public abstract class UIContainerbase<T> : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
{
    /// <summary>
    /// Handles the references of the containers from before run time
    /// </summary>
    private static List<UIContainerbase<T>> ContainerInstances = new List<UIContainerbase<T>>();
    
    /// <summary>
    /// The containment
    /// </summary>
    [SerializeReference] protected T _containment;

    public T Containment => _containment;

    /// <summary>
    /// The index of the slot
    /// </summary>
    [HideInInspector] public int SlotIndex;
    
    //References
    #region References

    private AbstractItemInventory _container;

    public AbstractItemInventory Container
    {
        get => _container;
        set => _container = value;
    }
    
    /// <summary>
    /// Slot text of the containment
    /// </summary>
    [SerializeField] private TextMeshProUGUI _slot;

    public TextMeshProUGUI Slot => _slot;

    /// <summary>
    /// Amount text of the containment
    /// </summary>
    [SerializeField] private TextMeshProUGUI _amount;

    public TextMeshProUGUI Amount => _amount;

    /// <summary>
    /// Icon of the containment
    /// </summary>
    [SerializeField] private Image _icon;

    public Image Icon => _icon;

    /// <summary>
    /// Highlight of the containment
    /// </summary>
    [SerializeField] private Image _highlight;

    public Image Highlight => _highlight;

    /// <summary>
    /// Slider of the containment
    /// </summary>
    [SerializeField] private Slider _slider;

    public Slider Slider => _slider;

    #endregion

    //Settings
    #region Settings
    
    /// <summary>
    /// Handles the highlight of a slot in use
    /// </summary>
    private bool _isHighlighted;

    public void SetHighlighted(bool pSet)
    {
        _isHighlighted = pSet;
        Highlight.enabled = _isHighlighted;
    }

    /// <summary>
    /// Indicator of what key is used to select the slot
    /// </summary>
    private bool _showIndicator;

    public bool ShowIndicator => _showIndicator;

    public void SetIndicator(bool pSet)
    {
        _showIndicator = pSet;
        if (SlotIndex < slotIcon.Length && ShowIndicator) _slot.text = slotIcon[SlotIndex];
        else _slot.text = "";
    }
    
    /// <summary>
    /// To check if the slot is allowed to be moved
    /// </summary>
    private bool _allowMoving;

    public bool AllowMoving
    {
        get => _allowMoving;
        set => _allowMoving = value;
    }

    #endregion
    
    private bool _hoveringContainment;

    private void Awake()
    {
        ContainerInstances.Add(this);
    }

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
        _containment = pContainment;
    }

    /// <summary>
    /// Handles clearing the containment
    /// </summary>
    public virtual void ClearContainment()
    {
        _containment = default;
        _amount.text = "";
        _icon.enabled = false;
        if(_highlight != null) _highlight.enabled = false;
        if(_slider != null) _slider.gameObject.SetActive(false);
    }

    string[] slotIcon = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "0", "-", "=" };
    private void Start()
    {
        if (!int.TryParse(gameObject.name, out SlotIndex))
            Debug.Log($"Cannot try parse {gameObject.name} to int...");
        
        if (SlotIndex < slotIcon.Length && ShowIndicator) _slot.text = slotIcon[SlotIndex];
        else _slot.text = "";
        
        if (_isHighlighted) _highlight.enabled = true;
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
        Debug.Log("789");
        ItemSnapperManager snapperManager = ItemSnapperManager.Instance();

        if (!snapperManager.IsSnapped)
        {
            if (_containment == null)
            {
                Debug.Log("Containment is null");
                return;
            }
            
            snapperManager.SetSnappedItem(this as UIContainerbase<GameItem>);
        }
        else
        {
            UIContainerbase<GameItem> currentSnap = snapperManager.CurrentItemSnapped;
            //Handles resetting snapping
            if (currentSnap == this as UIContainerbase<GameItem> || _containment == null)
            {
                snapperManager.ResetSnappedItem();
            }

            //Creates a placeholder of the current item
            GameItem placeHolder = new GameItem(Container.Items[SlotIndex].Item, Container.Items[SlotIndex].Amount);
            GameItem currentItem = currentSnap.Container.Items[currentSnap.SlotIndex];

            if (placeHolder.Item == currentItem.Item && SlotIndex != currentSnap.SlotIndex)
            {
                Debug.Log("a");
                GameItem updatedItem = new GameItem(currentItem.Item, currentItem.Amount + placeHolder.Amount);
                Container.Set(SlotIndex, updatedItem); 
                currentSnap.Container.Set(currentSnap.SlotIndex, new GameItem());//null
                snapperManager.ResetSnappedItem(false);
            } else {
                //Handles updating the containers
                Container.Set(SlotIndex, currentItem);
                currentSnap.Container.Set(currentSnap.SlotIndex, placeHolder.Item == null ? new GameItem() : placeHolder);
                snapperManager.ResetSnappedItem();
            }
        }
    }

    /// <summary>
    /// Handles placing a single item from a stack to a new slot
    /// </summary>
    public virtual void PlaceSingleItem()
    {
        Debug.Log("456");
        ItemSnapperManager snapperManager = ItemSnapperManager.Instance();
        
        UIContainerbase<GameItem> currentSnap = snapperManager.CurrentItemSnapped;
        if (currentSnap == this as UIContainerbase<GameItem>)
        {
            Debug.Log("Cannot do this function on the same item you got snapped...");
            return;
        }
        
        GameItem currentItem = currentSnap.Container.Items[currentSnap.SlotIndex];
        GameItem clickedItem = Container.Items[SlotIndex];
        if (_containment == null || currentItem.Item == clickedItem.Item)
        {
            currentItem.SetAmount(currentItem.Amount - 1);
            currentSnap.Container.Set(currentSnap.SlotIndex, currentItem);
            
            Container.Set(SlotIndex, new GameItem(currentItem.Item, clickedItem.Amount + 1));
            if (currentItem.Amount <= 0) snapperManager.ResetSnappedItem(false);
        }
    }

    /// <summary>
    /// Handles splitting item stacks into 
    /// </summary>
    public virtual void SplitItemStack()
    {
        Debug.Log("123");
        ItemSnapperManager snapperManager = ItemSnapperManager.Instance();
        
        GameItem currentItem = Container.Items[SlotIndex];
        if (currentItem == null || (currentItem.Item != null && !currentItem.Item.stackable)) return;
        
        int half = Mathf.FloorToInt(currentItem.Amount / 2);
        if (half <= 0) return;

        currentItem.SetAmount(currentItem.Amount - half);
        Container.Set(SlotIndex, currentItem);

        int nextFreeSlot = Container.GetFreeSlot();
        GameItem createdItem = new GameItem(currentItem.Item, half);
        Container.Set(nextFreeSlot, createdItem);

        snapperManager.SetSnappedItem(GetCorrectUIContainerbase(createdItem) as UIContainerbase<GameItem>);
    }

    public UIContainerbase<T> GetCorrectUIContainerbase(GameItem pCreatedItem)
    {
        return (from uiContainerbase in ContainerInstances let currentItem = uiContainerbase.Containment as GameItem where currentItem != null && currentItem.Equals(pCreatedItem) select uiContainerbase).FirstOrDefault();
    }

    public virtual void OnPointerDown(PointerEventData pEventData)
    {
        if (pEventData.button == PointerEventData.InputButton.Left || pEventData.button == PointerEventData.InputButton.Middle)
        {
            if (_allowMoving) SnapContainment();
            else SwitchContainment();
        } else if (pEventData.button == PointerEventData.InputButton.Right && _allowMoving)
        {
            ItemSnapperManager snapperManager = ItemSnapperManager.Instance();
            if (snapperManager.IsSnapped) PlaceSingleItem();
            else SplitItemStack();
        }
    }

    public virtual void OnPointerEnter(PointerEventData pEventData)
    {
        _hoveringContainment = true;
    }

    public virtual void OnPointerExit(PointerEventData pEventData)
    {
        _hoveringContainment = false;
        Icon.transform.localScale = new Vector3(1, 1, 1);
    }
}