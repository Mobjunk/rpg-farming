using UnityEngine;

public class ItemSnapperManager : Singleton<ItemSnapperManager>
{
    public bool IsSnapped;

    public UIContainerbase<GameItem> CurrentItemSnapped;

    public void SetSnappedItem(UIContainerbase<GameItem> pCurrentItemSnapped)
    {
        if (pCurrentItemSnapped.GetType() == typeof(ShopContainerGrid)) return;
        
        this.CurrentItemSnapped = pCurrentItemSnapped;
        
        this.CurrentItemSnapped.transform.localScale = new Vector3(1, 1, 1);

        Canvas canvas = this.CurrentItemSnapped.Icon.gameObject.AddComponent<Canvas>();
        canvas.overrideSorting = true;
        canvas.sortingOrder = 10;
        
        IsSnapped = true;
    }

    public void ResetSnappedItem(bool pIconEnabled = true)
    {
        if (CurrentItemSnapped == null) return;
        
        Destroy(CurrentItemSnapped.Icon.gameObject.GetComponent<Canvas>());
        CurrentItemSnapped.Icon.enabled = pIconEnabled;
        CurrentItemSnapped.Icon.transform.localPosition = Vector3.zero;
        CurrentItemSnapped.Amount.transform.localPosition = Vector3.zero;
        IsSnapped = false;
    }

    private void Update()
    {
        if (IsSnapped)
        {
            Vector3 movePoint = Input.mousePosition;
            
            Vector3 position = new Vector3(movePoint.x + 65, movePoint.y - 65);
            
            CurrentItemSnapped.Icon.transform.position = position;
            CurrentItemSnapped.Amount.transform.position = position;
        }
    }
}
