using System;
using UnityEngine;

public abstract class CharacterBodyPartManager : MonoBehaviour
{
    private CharacterStateManager _characterStateManager;
    private SpriteRenderer _spriteRenderer;
    private BodyPart _currentBodyPart;

    public BodyPart CurrentBodyPart
    {
        get => _currentBodyPart;
        set => _currentBodyPart = value;
    }

    public virtual void Awake()
    {
        _characterStateManager = GetComponentInParent<CharacterStateManager>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        if (_currentBodyPart != null)
        {
            _spriteRenderer.sprite = _currentBodyPart.defaultBottomSprites.sprites[0].sprite[0];
            _spriteRenderer.enabled = true;
        }

        _characterStateManager.OnStateChanged += OnStateChange;
    }

    /// <summary>
    /// Handles the state of the character changing
    /// </summary>
    private void OnStateChange()
    {
        /*CharacterManager cManager = GetComponentInParent<CharacterManager>();
        if (cManager.GetType() == typeof(Npc))
        {
            Debug.Log("characterStateManager: " + characterStateManager.GetCharacterState());
        }*/
        UpdateBodyPart(_currentBodyPart, _characterStateManager.GetDirection());
    }

    /// <summary>
    /// Handles updating the visual of a body part
    /// </summary>
    /// <param name="pBodyPart">The body part scriptable object</param>
    /// <param name="pRotation">The rotation of the character</param>
    /// <param name="pDarkSkin">Does the character have dark skin color</param>
    private void UpdateBodyPart(BodyPart pBodyPart, int pRotation, bool pDarkSkin = false)
    {
        _currentBodyPart = pBodyPart;

        Debug.Log("_currentBodyPart: " + _currentBodyPart.bodyType);

        int skinColor = 7;
        if (!_currentBodyPart.bodyType.Equals(BodyType.BODY)) skinColor = 0;
        
        int currentIndex = 0;
        string action = "IDLE";
        if (_characterStateManager.GetCharacterState().ToString().Contains("WALKING_HOLD"))
        {
            //currentIndex = 3 + int.Parse(_characterStateManager.GetCharacterState().ToString().Replace("WALKING_HOLD_", ""));
            action = "WALKING_HOLD";
        }
        else if (_characterStateManager.GetCharacterState().ToString().Contains("WALKING_"))
        {
            currentIndex = int.Parse(_characterStateManager.GetCharacterState().ToString().Replace("WALKING_", ""));
            action = "WALKING";
        }
        else if (_characterStateManager.GetCharacterState().ToString().StartsWith("PICKUP"))
        {
            //currentIndex = 6 + (int.Parse(_characterStateManager.GetCharacterState().ToString().Replace("PICKUP_", "")));
            action = "PICKUP";
        }
        else if (_characterStateManager.GetCharacterState().ToString().Equals("IDLE_HOLD"))
        {
            //currentIndex = 4;
            action = "IDLE_HOLD";
        }
        
        SpriteLayout bodySprites = GetSprites(action, pRotation);

        if (bodySprites == null)
        {
            _spriteRenderer.enabled = false;
            return;
        }

        if (bodySprites.sprites.Length > 0 && bodySprites.sprites[0].sprite.Length != 0)
        {
            //pDarkSkin ? 1 : 0
            if (currentIndex >= bodySprites.sprites[skinColor].sprite.Length)
            {
                //Debug.Log("bodySprites: " + gameObject.name);
                //Debug.Log("currentIndex: " + currentIndex);
                //Debug.Log("bodySprites.sprites[darkSkin ? 1 : 0].sprite.Length: " + bodySprites.sprites[darkSkin ? 1 : 0].sprite.Length);
                return;
            }
            
            _spriteRenderer.sprite = bodySprites.sprites[skinColor].sprite[currentIndex];
            _spriteRenderer.enabled = true;
        } else _spriteRenderer.enabled = false;
    }

    /// <summary>
    /// Handles grabbign the right array of sprites based on the characters rotation
    /// </summary>
    /// <param name="pRotation"></param>
    /// <returns></returns>
    private SpriteLayout GetSprites(string pAction, int pRotation)
    {
        if (_currentBodyPart == null) return null;

        switch (pRotation)
        {
            case 0: //Down
                return _currentBodyPart.defaultBottomSprites;
            case 1: //Left
                return _currentBodyPart.defaultLeftSprites;
            case 2: //Right
                return _currentBodyPart.defaultRightSprites;
            case 3: //Up
                return _currentBodyPart.defaultTopSprites;
        }

        return null;
    }
}
