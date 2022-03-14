using System;
using UnityEngine;
using UnityEngine.UI;
using Object = UnityEngine.Object;

public abstract class CharacterBodyPartManager : BodyPartManager
{
    private PlayerInformationManager _playerInformationManager => PlayerInformationManager.Instance();
    
    private Animator _animator;
    private CharacterStateManager _characterStateManager;
    private SpriteRenderer _spriteRenderer;
    
    public override void Awake()
    {
        base.Awake();
        
        LoadSprites();
        
        _animator = GetComponentInParent<Animator>();
        _characterStateManager = GetComponentInParent<CharacterStateManager>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        
        if(_characterStateManager != null) _characterStateManager.OnStateChanged += OnStateChange;
    }

    /// <summary>
    /// Handles the state of the character changing
    /// </summary>
    private void OnStateChange()
    {
        UpdateBodyPart(CurrentBodyPart, _characterStateManager == null ? 0 : _characterStateManager.GetDirection(),_playerInformationManager == null ? 0 : _playerInformationManager.CharacterSkinColor, _playerInformationManager == null ? 0 : _playerInformationManager.CharacterHairColor);
    }

    /// <summary>
    /// Handles updating the visual of a body part
    /// </summary>
    /// <param name="pBodyPart">The body part scriptable object</param>
    /// <param name="pRotation">The rotation of the character</param>
    /// <param name="pDarkSkin">Does the character have dark skin color</param>
    private void UpdateBodyPart(BodyPart pBodyPart, int pRotation, int pSkinColor, int pHairColor)
    {
        CurrentBodyPart = pBodyPart;

        if (CurrentBodyPart == null)
        {
            _spriteRenderer.enabled = false;
            return;
        }
        
        int totalFrames = _animator.GetCurrentAnimatorClipInfo(0)[0].clip.events.Length;

        int currentIndex = 0;
        string action = "IDLE";
        bool carry_idle = _characterStateManager.GetCharacterState().Equals(CharacterStates.CARRY_IDLE);
        
        //TODO: Clean this mess up
        if (_characterStateManager.GetCharacterState().ToString().Contains("WALKING_"))
        {
            currentIndex = int.Parse(_characterStateManager.GetCharacterState().ToString().Replace("WALKING_", ""));
            action = "WALKING";
        }
        else if (_characterStateManager.GetCharacterState().ToString().StartsWith("AXE_SWING_"))
        {
            currentIndex = int.Parse(_characterStateManager.GetCharacterState().ToString().Replace("AXE_SWING_", ""));
            action = "AXE_SWING";
        }
        else if (_characterStateManager.GetCharacterState().ToString().StartsWith("PICKAXE_SWING_"))
        {
            currentIndex = int.Parse(_characterStateManager.GetCharacterState().ToString().Replace("PICKAXE_SWING_", ""));
            action = "PICKAXE_SWING";
        }
        else if (_characterStateManager.GetCharacterState().ToString().StartsWith("WATERING_"))
        {
            currentIndex = int.Parse(_characterStateManager.GetCharacterState().ToString().Replace("WATERING_", ""));
            action = "WATERING";
        }
        else if (_characterStateManager.GetCharacterState().ToString().StartsWith("HOE_SWING_"))
        {
            currentIndex = int.Parse(_characterStateManager.GetCharacterState().ToString().Replace("HOE_SWING_", ""));
            action = "HOE_SWING";
        }
        else if (_characterStateManager.GetCharacterState().ToString().StartsWith("CARRY_"))
        {
            currentIndex = carry_idle ? 0 : int.Parse(_characterStateManager.GetCharacterState().ToString().Replace("CARRY_", ""));
            action = "CARRY";
        }
        else if (_characterStateManager.GetCharacterState().ToString().StartsWith("FISHING_"))
        {
            currentIndex = _characterStateManager.GetCharacterState().ToString().Equals("FISHING_IDLE") ? 0 : int.Parse(_characterStateManager.GetCharacterState().ToString().Replace("FISHING_", ""));
            action = "FISHING";
        }
        else if (_characterStateManager.GetCharacterState().ToString().StartsWith("SWORD_SWING_"))
        {
            currentIndex = int.Parse(_characterStateManager.GetCharacterState().ToString().Replace("SWORD_SWING_", ""));
            action = "SWORD_SWING";
        }
        if (action.Equals("IDLE") || carry_idle) totalFrames = 8;
        else if (action.Equals("WATERING")) totalFrames = 2;
        else if (_characterStateManager.GetCharacterState().Equals(CharacterStates.FISHING_IDLE)) totalFrames = 5;

        
        string fileName = pBodyPart.GetFileName(action, !pBodyPart.bodyType.Equals(BodyType.BODY) ? 0 : pSkinColor);
        int baseIndex = pBodyPart.GetSpriteIndex(action, pRotation);
        
        int multiplier = GetMultiplier(pBodyPart);
        if (pBodyPart.UseHairColor()) multiplier = pHairColor;
        
        
        int modifiedIndex = baseIndex + currentIndex;
        if (pBodyPart.RequiresMultiplier()) modifiedIndex += (totalFrames * multiplier);

        string realFileName = fileName + "" + modifiedIndex;
        Sprite sprite = CachedSpritesManager.GetCachedSprite(realFileName);
        if (sprite == null)
        {
            sprite = CachedSpritesManager.GetSprite(realFileName);
            CachedSpritesManager.CachedSprites.Add(sprite);
        }
        if (sprite != null)
        {
            _spriteRenderer.sprite = sprite;
            _spriteRenderer.enabled = true;
        } else _spriteRenderer.enabled = false;
    }

    public void SetBodyPart(BodyPart pBodyPart)
    {
        CurrentBodyPart = pBodyPart;
        //LoadSprites();
        OnStateChange();
    }
}
