using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MenuManager<T> : Singleton<T> where T : MonoBehaviour
{
    [SerializeField] private GameObject content;
    [SerializeField] private RectTransform contents;
    [SerializeField] private GameObject[] buttons;
    
    public void SetAnchorPoint(AnchorsPresets anchor, Vector2 offset)
    {
        Vector2 anchorPoint = Utility.GetAnchor(anchor);
        contents.pivot = anchorPoint;
        contents.anchorMin = anchorPoint;
        contents.anchorMax = anchorPoint;

        contents.anchoredPosition = new Vector2(offset.x, offset.y);
        
    }
    
    public void Unhide(bool hideButtons = false)
    {
        if(hideButtons)
            foreach(GameObject o in buttons)
                o.SetActive(false);
        content.SetActive(true);
    }

    public void Hide(bool unhideButtons = false)
    {
        if(unhideButtons)
            foreach(GameObject o in buttons)
                o.SetActive(true);
        content.SetActive(false);
    }

    public void SetButtons(bool unhideButtons = false)
    {
        foreach(GameObject o in buttons)
            o.SetActive(unhideButtons);
    }
}
