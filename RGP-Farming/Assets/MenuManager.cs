using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MenuManager<T> : Singleton<T> where T : MonoBehaviour
{
    [SerializeField] private GameObject content;
    [SerializeField] private RectTransform contents;
    
    public void SetAnchorPoint(AnchorsPresets anchor, Vector2 offset)
    {
        Vector2 anchorPoint = Utility.GetAnchor(anchor);
        contents.pivot = anchorPoint;
        contents.anchorMin = anchorPoint;
        contents.anchorMax = anchorPoint;

        contents.anchoredPosition = new Vector2(offset.x, offset.y);
        
    }
    
    public void Unhide()
    {
        content.SetActive(true);
    }

    public void Hide()
    {
        content.SetActive(false);
    }
}
