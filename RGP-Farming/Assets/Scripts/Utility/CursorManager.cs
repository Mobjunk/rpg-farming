using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.PlayerLoop;

public class CursorManager : Singleton<CursorManager>
{
    [SerializeField] private Texture2D _regularCursor;
    [SerializeField] private Texture2D _useableInteractionCursor;
    [SerializeField] private Texture2D _nonUseableInteractionCursor;
    
    private int UILayer;

    private void Awake()
    {
        SetDefaultCursor();
        UILayer = LayerMask.NameToLayer("UI");
        //Cursor.lockState = CursorLockMode.Locked;
    }

    public void SetDefaultCursor()
    {
        //Cursor.SetCursor(_regularCursor, Vector2.zero, CursorMode.ForceSoftware);
    }
    
    public void SetUsableInteractionCursor()
    {
        //Cursor.SetCursor(_useableInteractionCursor, Vector2.zero, CursorMode.ForceSoftware);
    }
    
    public void SetNonUsableInteractionCursor()
    {
        //Cursor.SetCursor(_nonUseableInteractionCursor, Vector2.zero, CursorMode.ForceSoftware);
    }
    
    public bool IsPointerOverUIElement()
    {
        return IsPointerOverUIElement(GetEventSystemRaycastResults());
    }
    
    private bool IsPointerOverUIElement(List<RaycastResult> pEventSystemRaysastResults)
    {
        for (int index = 0; index < pEventSystemRaysastResults.Count; index++)
        {
            RaycastResult curRaysastResult = pEventSystemRaysastResults[index];
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