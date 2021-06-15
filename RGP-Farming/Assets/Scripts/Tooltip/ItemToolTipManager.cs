using System;
using TMPro;
using UnityEngine;

public class ItemToolTipManager : Singleton<ItemToolTipManager>
{
    /// <summary>
    /// The minimum size the tooltip needs to be
    /// </summary>
    private Vector2 minSize = new Vector2(115, 80);

    [SerializeField] private RectTransform mainBackground;
    [SerializeField] private TextMeshProUGUI itemName;
    [SerializeField] private TextMeshProUGUI itemType;
    [SerializeField] private TextMeshProUGUI itemDescription;

    private void Awake()
    {
        SetTooltip(null);
        mainBackground.gameObject.SetActive(false);
    }

    private void Update()
    {
        Vector3 movePoint = Input.mousePosition;
            
        
        //TODO: Check if the box is outside the screen
        Debug.Log("mainBackground: " + mainBackground.sizeDelta);
        Debug.Log("mouse + size: " + (new Vector2(movePoint.x, movePoint.y) + mainBackground.sizeDelta));
        
        Vector3[] corners = new Vector3[4];
        mainBackground.GetWorldCorners(corners);
        
        //movePoint + mainBackground.sizeDelta
        Vector3 position = new Vector3(movePoint.x + 65, movePoint.y - 65);
            
        mainBackground.position = position;
    }
    
    public void SetTooltip(AbstractItemData hoveredItem)
    {
        if (hoveredItem == null)
        {
            mainBackground.gameObject.SetActive(false);
            return;
        } 
        itemName.text = $"{hoveredItem.itemName}";
        itemType.text = $"{hoveredItem.tooltype.ToString()}";
        itemDescription.text = $"{hoveredItem.itemDescription}";
        
        float sizeX = 45 + itemName.preferredWidth;
        float sizeY = 80 + itemDescription.preferredHeight;
        
        if (sizeX < minSize.x) sizeX = minSize.x;
        if (sizeY < minSize.y) sizeY = minSize.y;
        
        mainBackground.sizeDelta = new Vector2(sizeX, sizeY);
        mainBackground.gameObject.SetActive(true);
    }
    
}
