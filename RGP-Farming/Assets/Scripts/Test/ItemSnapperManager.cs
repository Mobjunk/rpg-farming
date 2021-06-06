using UnityEngine;

public class ItemSnapperManager : Singleton<ItemSnapperManager>
{
    public bool isSnapped;

    public UIContainerbase<Item> currentItemSnapped;

    public void SetSnappedItem(UIContainerbase<Item> currentItemSnapped)
    {
        this.currentItemSnapped = currentItemSnapped;

        Canvas canvas = this.currentItemSnapped.Icon.gameObject.AddComponent<Canvas>();
        canvas.overrideSorting = true;
        canvas.sortingOrder = 1;
        
        isSnapped = true;
    }

    public void ResetSnappedItem()
    {
        Destroy(currentItemSnapped.Icon.gameObject.GetComponent<Canvas>());
        currentItemSnapped.Icon.transform.localPosition = Vector3.zero;
        currentItemSnapped.Amount.transform.localPosition = Vector3.zero;
        isSnapped = false;
    }

    public void Swap(UIContainerbase<Item> itemSnapped)
    {
        ResetSnappedItem();
        SetSnappedItem(itemSnapped);
    }
    
    private void Update()
    {
        if (isSnapped)
        {
            Vector3 movePoint = Input.mousePosition;
            
            currentItemSnapped.Icon.transform.position = movePoint;
            currentItemSnapped.Amount.transform.position = movePoint;
        }
    }
}
