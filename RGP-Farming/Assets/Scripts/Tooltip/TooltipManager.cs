using TMPro;
using UnityEngine;

public abstract class TooltipManager<T> : Singleton<T> where T : MonoBehaviour
{
    private ItemSnapperManager itemSnapperManager => ItemSnapperManager.Instance();
    
    public abstract Vector2 MinSize();
    public abstract Vector2 StartPosition();
    
    [SerializeField] private RectTransform mainBackground;
    [SerializeField] private TextMeshProUGUI itemName;
    [SerializeField] private TextMeshProUGUI itemDescription;

    protected float increasedY;
    private AbstractItemData hoveredItem;

    public void Awake()
    {
        SetTooltip(null);
        mainBackground.gameObject.SetActive(false);
    }

    private void Update()
    {
        SetPosition(new Vector2(Input.mousePosition.x, Input.mousePosition.y));
    }

    private void SetPosition(Vector2 mousePosition)
    {
        MoveAnchorPoint(mousePosition);
        
        float sizeX = StartPosition().x + itemName.preferredWidth;
        float sizeY = StartPosition().y + (increasedY != 0 ? increasedY : itemDescription.preferredHeight);
        
        if (sizeX < MinSize().x) sizeX = MinSize().x;
        if (sizeY < MinSize().y) sizeY = MinSize().y;
        
        mainBackground.sizeDelta = new Vector2(sizeX, sizeY);
        mainBackground.gameObject.SetActive(hoveredItem != null);
        
        mainBackground.position = new Vector3(mousePosition.x + (itemSnapperManager.isSnapped ? 110 : 65), mousePosition.y);
    }
    
    private void MoveAnchorPoint(Vector2 mousePosition)
    {
        Vector2 tooltipSize = mainBackground.rect.size;
        Vector2 screenSize = Camera.main.pixelRect.size;
        Vector2 pivot = new Vector2(0, 1);
        
        if (mousePosition.y - tooltipSize.y < 40) pivot = new Vector2(pivot.x, 0);
        if (mousePosition.x + tooltipSize.x > screenSize.x) pivot = new Vector2(1, pivot.y);

        mainBackground.pivot = pivot;
    }
    
    public virtual void SetTooltip(AbstractItemData hoveredItem)
    {
        this.hoveredItem = hoveredItem;
        if (this.hoveredItem == null)
        {
            ResetTooltip();
            return;
        }
        itemName.text = $"{Utility.UppercaseFirst(this.hoveredItem.itemName.ToLower())}";
        itemDescription.text = $"{this.hoveredItem.itemDescription}";
    }

    public virtual void ResetTooltip()
    {
        //mainBackground.gameObject.SetActive(false);
    }
}
