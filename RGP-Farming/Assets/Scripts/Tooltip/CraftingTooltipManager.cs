using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CraftingTooltipManager : TooltipManager<CraftingTooltipManager>
{

    [SerializeField] private RectTransform itemRequiredParent;
    [SerializeField] private GameObject itemPrefab;

    [SerializeField] private RectTransform deviderRect;
    [SerializeField] private RectTransform descriptionRect;
    
    public override Vector2 MinSize()
    {
        return new Vector2(115, 59);
    }

    public override Vector2 StartPosition()
    {
        return new Vector2(45, 95);
    }

    public override void SetTooltip(AbstractItemData hoveredItem)
    {
        base.SetTooltip(hoveredItem);

        if (hoveredItem == null)
        {
            ResetTooltip();
            return;
        }

        increasedY = hoveredItem.craftingRecipe.requiredItems.Count * 20;
        itemRequiredParent.sizeDelta = new Vector2(itemRequiredParent.sizeDelta.x, increasedY);
        
        foreach (Item item in hoveredItem.craftingRecipe.requiredItems)
        {
            GameObject containment = Instantiate(itemPrefab, itemRequiredParent, true);
            containment.transform.localScale = new Vector3(1, 1, 1);

            Image icon = containment.GetComponentInChildren<Image>();
            icon.sprite = item.item.uiSprite;

            TextMeshProUGUI amount = icon.GetComponentInChildren<TextMeshProUGUI>();
            amount.text = $"{item.amount}";

            TextMeshProUGUI itemName = containment.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
            itemName.text = $"{item.item.itemName}";
        }
        
        deviderRect.anchoredPosition = new Vector2(deviderRect.anchoredPosition.x, -21 - increasedY);
        descriptionRect.offsetMax = new Vector2(descriptionRect.offsetMax.x, -22 - increasedY);
    }

    public override void ResetTooltip()
    {
        base.ResetTooltip();
        deviderRect.anchoredPosition = new Vector2(deviderRect.anchoredPosition.x, -21);
        descriptionRect.offsetMax  = new Vector2(descriptionRect.offsetMax.x, -22);
        foreach(Transform childParent in itemRequiredParent)
            Destroy(childParent.gameObject);
    }
}
