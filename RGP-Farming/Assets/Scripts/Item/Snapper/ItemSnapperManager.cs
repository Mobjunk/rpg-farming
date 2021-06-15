using UnityEngine;

public class ItemSnapperManager : Singleton<ItemSnapperManager>
{
    public bool isSnapped;

    public UIContainerbase<Item> currentItemSnapped;

    public void SetSnappedItem(UIContainerbase<Item> currentItemSnapped)
    {
        if (currentItemSnapped.GetType() == typeof(ShopContainerGrid)) return;
        
        this.currentItemSnapped = currentItemSnapped;
        
        this.currentItemSnapped.transform.localScale = new Vector3(1, 1, 1);

        Canvas canvas = this.currentItemSnapped.Icon.gameObject.AddComponent<Canvas>();
        canvas.overrideSorting = true;
        canvas.sortingOrder = 10;
        
        isSnapped = true;
    }

    public void ResetSnappedItem(bool iconEnabled = true)
    {
        if (currentItemSnapped == null) return;
        
        Destroy(currentItemSnapped.Icon.gameObject.GetComponent<Canvas>());
        currentItemSnapped.Icon.enabled = iconEnabled;
        currentItemSnapped.Icon.transform.localPosition = Vector3.zero;
        currentItemSnapped.Amount.transform.localPosition = Vector3.zero;
        isSnapped = false;
    }

    private void Update()
    {
        if (isSnapped)
        {
            Vector3 movePoint = Input.mousePosition;
            
            Vector3 position = new Vector3(movePoint.x + 65, movePoint.y - 65);
            
            currentItemSnapped.Icon.transform.position = position;
            currentItemSnapped.Amount.transform.position = position;
        }
    }
}
