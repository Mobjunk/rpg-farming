using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class VirtualMouseManager : Singleton<VirtualMouseManager>
{
    [SerializeField] private Image _cursorImage;

    private void Awake()
    {
        _cursorImage = GetComponent<Image>();
    }

    void Update()
    {
        GameObject selectedObject = EventSystem.current.currentSelectedGameObject;
        if (selectedObject == null)
        {
            //Debug.Log("??????????");
            _cursorImage.enabled = false;
            return;
        }
        
        _cursorImage.enabled = true;
        SetPosition(selectedObject.transform.position);
    }

    public void SetPosition(Vector3 pPosition)
    {
        transform.position = new Vector3(pPosition.x + 50f, pPosition.y - 50f, transform.position.z);
    }
}
