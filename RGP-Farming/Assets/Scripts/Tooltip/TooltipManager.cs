using TMPro;
using UnityEngine;

public abstract class TooltipManager<T> : Singleton<T> where T : MonoBehaviour
{
    private ItemSnapperManager _itemSnapperManager => ItemSnapperManager.Instance();
    
    public abstract Vector2 MinSize();
    public abstract Vector2 StartPosition();
    
    [SerializeField] private RectTransform _mainBackground;
    [SerializeField] private TextMeshProUGUI _itemName;
    [SerializeField] private TextMeshProUGUI _itemDescription;

    protected float _increasedY;
    private AbstractItemData _hoveredItem;

    public void Awake()
    {
        SetTooltip(null);
        _mainBackground.gameObject.SetActive(false);
    }

    private void Update()
    {
        SetPosition(new Vector2(Input.mousePosition.x, Input.mousePosition.y));
    }

    private void SetPosition(Vector2 pMousePosition)
    {
        
        float sizeX = StartPosition().x + _itemName.preferredWidth;
        float sizeY = StartPosition().y + (_increasedY != 0 ? _increasedY : _itemDescription.preferredHeight);
        
        if (sizeX < MinSize().x) sizeX = MinSize().x;
        if (sizeY < MinSize().y) sizeY = MinSize().y;
        
        _mainBackground.sizeDelta = new Vector2(sizeX, sizeY);
        _mainBackground.gameObject.SetActive(_hoveredItem != null);
        
        MoveAnchorPoint(pMousePosition);
        
        _mainBackground.position = new Vector3(pMousePosition.x + (_itemSnapperManager.IsSnapped ? 110 : 65), pMousePosition.y);
    }
    
    private void MoveAnchorPoint(Vector2 pMousePosition)
    {
        Vector2 tooltipSize = _mainBackground.rect.size;
        Vector2 screenSize = Camera.main.pixelRect.size;
        Vector2 pivot = new Vector2(0, 1);
        
        if (pMousePosition.y - tooltipSize.y < tooltipSize.y) pivot = new Vector2(pivot.x, 0);
        if (pMousePosition.x + tooltipSize.x > screenSize.x) pivot = new Vector2(1, pivot.y);

        _mainBackground.pivot = pivot;
    }
    
    public virtual void SetTooltip(AbstractItemData pHoveredItem)
    {
        this._hoveredItem = pHoveredItem;
        if (this._hoveredItem == null)
        {
            ResetTooltip();
            return;
        }
        _itemName.text = $"{Utility.UppercaseFirst(this._hoveredItem.itemName.ToLower())}";
        _itemDescription.text = $"{this._hoveredItem.itemDescription}";
    }

    public virtual void ResetTooltip()
    {
        _mainBackground.gameObject.SetActive(false);
    }
}
