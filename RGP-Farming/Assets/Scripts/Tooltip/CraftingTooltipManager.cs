using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CraftingTooltipManager : TooltipManager<CraftingTooltipManager>
{

    [SerializeField] private RectTransform _itemRequiredParent;
    [SerializeField] private GameObject _itemPrefab;

    [SerializeField] private RectTransform _deviderRect;
    [SerializeField] private RectTransform _descriptionRect;
    
    public override Vector2 MinSize()
    {
        return new Vector2(115, 59);
    }

    public override Vector2 StartPosition()
    {
        return new Vector2(45, 95);
    }

    public override void SetTooltip(AbstractItemData pHoveredItem)
    {
        base.SetTooltip(pHoveredItem);

        if (pHoveredItem == null)
        {
            ResetTooltip();
            return;
        }

        _increasedY = pHoveredItem.craftingRecipe.requiredItems.Count * 20;
        _itemRequiredParent.sizeDelta = new Vector2(_itemRequiredParent.sizeDelta.x, _increasedY);
        
        foreach (GameItem item in pHoveredItem.craftingRecipe.requiredItems)
        {
            GameObject containment = Instantiate(_itemPrefab, _itemRequiredParent, true);
            containment.transform.localScale = new Vector3(1, 1, 1);

            Image icon = containment.GetComponentInChildren<Image>();
            icon.sprite = item.Item.uiSprite;

            TextMeshProUGUI amount = icon.GetComponentInChildren<TextMeshProUGUI>();
            amount.text = $"{item.Amount}";

            TextMeshProUGUI itemName = containment.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
            itemName.text = $"{item.Item.itemName}";
        }
        
        _deviderRect.anchoredPosition = new Vector2(_deviderRect.anchoredPosition.x, -21 - _increasedY);
        _descriptionRect.offsetMax = new Vector2(_descriptionRect.offsetMax.x, -22 - _increasedY);
    }

    public override void ResetTooltip()
    {
        base.ResetTooltip();
        _deviderRect.anchoredPosition = new Vector2(_deviderRect.anchoredPosition.x, -21);
        _descriptionRect.offsetMax  = new Vector2(_descriptionRect.offsetMax.x, -22);
        foreach(Transform childParent in _itemRequiredParent)
            Destroy(childParent.gameObject);
    }
}
