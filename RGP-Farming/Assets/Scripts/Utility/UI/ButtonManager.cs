using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    
    public void StartGame()
    {
        Utility.UnloadScene("MainMenu");
        Utility.AddSceneIfNotLoaded("Character Design");
    }
    
    public void Quit()
    {
        Application.Quit();
    }

    private void Awake()
    {
        if(_hoveringImage != null)
            _defaultColor = _hoveringImage.color;
    }

    [SerializeField] private Color _defaultColor;
    
    [SerializeField] private Color _highlightedColor;

    [SerializeField] private Color _clickColor;

    [SerializeField] private Image _hoveringImage;

    private bool _isHovering;
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        _hoveringImage.color = _highlightedColor;
        _isHovering = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _hoveringImage.color = _defaultColor;
        _isHovering = false;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        _hoveringImage.color = _clickColor;
        StartCoroutine(ResetClick());
    }

    IEnumerator ResetClick()
    {
        yield return new WaitForSeconds(0.1f);
        _hoveringImage.color = _isHovering ? _highlightedColor : _defaultColor;
    }
}
