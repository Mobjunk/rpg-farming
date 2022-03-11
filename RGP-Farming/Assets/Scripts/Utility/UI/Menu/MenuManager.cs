using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MenuManager<T> : Singleton<T> where T : MonoBehaviour
{
    [SerializeField] private GameObject _content;
    [SerializeField] private RectTransform _contents;
    [SerializeField] private GameObject[] _buttons;
    
    public void SetAnchorPoint(AnchorsPresets pAnchor, Vector2 pOffset)
    {
        Vector2 anchorPoint = Utility.GetAnchor(pAnchor);
        _contents.pivot = anchorPoint;
        _contents.anchorMin = anchorPoint;
        _contents.anchorMax = anchorPoint;

        _contents.anchoredPosition = new Vector2(pOffset.x, pOffset.y);
        
    }
    
    public virtual void Unhide(bool pHideButtons = false)
    {
        if(pHideButtons)
            foreach(GameObject o in _buttons)
                o.SetActive(false);
        _content.SetActive(true);
    }

    public virtual void Hide(bool pUnhideButtons = false)
    {
        if(pUnhideButtons)
            foreach(GameObject o in _buttons)
                o.SetActive(true);
        _content.SetActive(false);
    }

    public void SetButtons(bool pUnhideButtons = false)
    {
        foreach(GameObject o in _buttons)
            o.SetActive(pUnhideButtons);
    }
}
