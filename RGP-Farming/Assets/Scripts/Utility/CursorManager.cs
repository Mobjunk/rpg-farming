using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.PlayerLoop;

public class CursorManager : Singleton<CursorManager>
{
    [SerializeField] private Texture2D regularCursor;
    [SerializeField] private Texture2D useableInteractionCursor;
    [SerializeField] private Texture2D nonUseableInteractionCursor;
    
    private int UILayer;

    private void Awake()
    {
        SetDefaultCursor();
        UILayer = LayerMask.NameToLayer("UI");
    }

    public void SetDefaultCursor()
    {
        Cursor.SetCursor(regularCursor, Vector2.zero, CursorMode.ForceSoftware);
    }
    
    public void SetUsableInteractionCursor()
    {
        Cursor.SetCursor(useableInteractionCursor, Vector2.zero, CursorMode.ForceSoftware);
    }
    
    public void SetNonUsableInteractionCursor()
    {
        Cursor.SetCursor(nonUseableInteractionCursor, Vector2.zero, CursorMode.ForceSoftware);
    }
    
    public bool IsPointerOverUIElement()
    {
        return IsPointerOverUIElement(GetEventSystemRaycastResults());
    }
    
    private bool IsPointerOverUIElement(List<RaycastResult> eventSystemRaysastResults)
    {
        for (int index = 0; index < eventSystemRaysastResults.Count; index++)
        {
            RaycastResult curRaysastResult = eventSystemRaysastResults[index];
            if (curRaysastResult.gameObject.layer == UILayer)
                return true;
        }
        return false;
    }
    
    static List<RaycastResult> GetEventSystemRaycastResults()
    {
        PointerEventData eventData = new PointerEventData(EventSystem.current);
        eventData.position = Mouse.current.position.ReadValue();
        List<RaycastResult> raysastResults = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, raysastResults);
        return raysastResults;
    }
}