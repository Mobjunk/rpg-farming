using UnityEngine;
using UnityEngine.InputSystem;

public class ItemSnapperManager : Singleton<ItemSnapperManager>
{
    public bool IsSnapped;

    public UIContainerbase<GameItem> CurrentItemSnapped;

    public void SetSnappedItem(UIContainerbase<GameItem> pCurrentItemSnapped)
    {
        if (pCurrentItemSnapped.GetType() == typeof(ShopContainerGrid)) return;
        
        CurrentItemSnapped = pCurrentItemSnapped;
        
        CurrentItemSnapped.transform.localScale = new Vector3(1, 1, 1);

        Canvas canvas = CurrentItemSnapped.Icon.gameObject.AddComponent<Canvas>();
        canvas.overrideSorting = true;
        canvas.sortingOrder = 10;
        
        IsSnapped = true;
    }

    public void ResetSnappedItem(bool pIconEnabled = true)
    {
        if (CurrentItemSnapped == null) return;
        
        Destroy(CurrentItemSnapped.Icon.gameObject.GetComponent<Canvas>());
        //TODO: might cause a issue with swapping items
        if (pIconEnabled && CurrentItemSnapped.Containment == null) pIconEnabled = false; 
        CurrentItemSnapped.Icon.enabled = pIconEnabled;
        CurrentItemSnapped.Icon.transform.localPosition = Vector3.zero;
        CurrentItemSnapped.Amount.transform.localPosition = Vector3.zero;
        IsSnapped = false;
    }

    private void Update()
    {
        if (IsSnapped)
        {
            Vector3 movePoint = Mouse.current.position.ReadValue();

            bool gamePad = CharacterInputManager.Instance().GamepadActive;
            if (gamePad)
                movePoint = VirtualMouseManager.Instance().transform.position;
            
            Vector3 position = new Vector3(movePoint.x + (gamePad ? 45 : 65), movePoint.y - (gamePad ? 45 : 65));
            
            CurrentItemSnapped.Icon.transform.position = position;
            CurrentItemSnapped.Amount.transform.position = position;
        }
    }
}
