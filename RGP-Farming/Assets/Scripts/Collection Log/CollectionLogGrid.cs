using UnityEngine.EventSystems;

public class CollectionLogGrid : AbstractItemContainer<CollectionLogEntry>
{
    private CollectionTooltipManager _collectionTooltipManager => CollectionTooltipManager.Instance();
    
    public override void UpdateItemContainer()
    {
        if (Containment == null || Containment.Item == null)
        {
            base.ClearContainment();
            return;
        }
        Icon.sprite = Containment.Item.uiSprite;
        Icon.enabled = true;

        Amount.enabled = false;
        
        Slider.gameObject.SetActive(false);
        Highlight.gameObject.SetActive(false);
    }

    public override void OnPointerEnter(PointerEventData pEventData)
    {
        base.OnPointerEnter(pEventData);
        if (Containment == null) return;
        _collectionTooltipManager.SetTooltip(Containment);        
    }

    public override void OnPointerExit(PointerEventData pEventData)
    {
        base.OnPointerExit(pEventData);
        _collectionTooltipManager.SetTooltip(null);
    }
}