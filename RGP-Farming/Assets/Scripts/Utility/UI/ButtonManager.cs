using System;
using System.Collections;
using System.Collections.Generic;
using FMOD;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    private SoundManager _soundManager => SoundManager.Instance();
    
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

    [SerializeField] private bool _useCustomCode = true;

    [SerializeField] private Color _defaultColor;
    
    [SerializeField] private Color _highlightedColor;

    [SerializeField] private Color _clickColor;

    [SerializeField] private Image _hoveringImage;

    private bool _isHovering;
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        
        if (_useCustomCode)
        {
            _hoveringImage.color = _highlightedColor;
            _isHovering = true;
        }
        _soundManager.ExecuteSound("menu_hover");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (_useCustomCode)
        {
            _hoveringImage.color = _defaultColor;
            _isHovering = false;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (_useCustomCode)
        {
            _hoveringImage.color = _clickColor;
            StartCoroutine(ResetClick());
        }
        _soundManager.ExecuteSound("menu_select");
    }

    IEnumerator ResetClick()
    {
        yield return new WaitForSeconds(0.1f);
        _hoveringImage.color = _isHovering ? _highlightedColor : _defaultColor;
    }

    public void SwitchTab(int index)
    {
        Player.Instance().CharacterUIManager.CurrentUIOpened.SwitchToTab(index);
    }
}
