using System;
using UnityEngine;
using Object = UnityEngine.Object;

public abstract class CharacterBodyPartManager : MonoBehaviour
{
    private CachedSpritesManager _cachedSpritesManager => CachedSpritesManager.Instance();

    private Animator _animator;
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
        _animator = GetComponentInParent<Animator>();
        _characterStateManager = GetComponentInParent<CharacterStateManager>();
        _spriteRenderer = GetComponent<SpriteRenderer>();


        LoadSpritesForPath(_currentBodyPart.walkPathName);
        LoadSpritesForPath(_currentBodyPart.axePathName);
        LoadSpritesForPath(_currentBodyPart.pickaxePathName);
        LoadSpritesForPath(_currentBodyPart.wateringCanPathName);
        LoadSpritesForPath(_currentBodyPart.hoePathName);
        LoadSpritesForPath(_currentBodyPart.carryPathName);
        LoadSpritesForPath(_currentBodyPart.fishingPathName);
        LoadSpritesForPath(_currentBodyPart.swordPathName);
        
        /*if (_currentBodyPart != null)
        {
            _spriteRenderer.sprite = _currentBodyPart.defaultBottomSprites.sprites[0].sprite[0];
            _spriteRenderer.enabled = true;
        }*/

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
    private void UpdateBodyPart(BodyPart pBodyPart, int pRotation, int pSkinColor = 0)
    {
        _currentBodyPart = pBodyPart;
        
        int totalFrames = _animator.GetCurrentAnimatorClipInfo(0)[0].clip.events.Length;

        int skinColor = pSkinColor;
        if (!_currentBodyPart.bodyType.Equals(BodyType.BODY)) skinColor = 0;
        
        int currentIndex = 0;
        string action = "IDLE";
        bool carry_idle = _characterStateManager.GetCharacterState().Equals(CharacterStates.CARRY_IDLE);
        
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
            currentIndex = int.Parse(_characterStateManager.GetCharacterState().ToString().Replace("FISHING_", ""));
            action = "FISHING";
        }
        else if (_characterStateManager.GetCharacterState().ToString().StartsWith("SWORD_SWING_"))
        {
            currentIndex = int.Parse(_characterStateManager.GetCharacterState().ToString().Replace("SWORD_SWING_", ""));
            action = "SWORD_SWING";
        }
        if (action.Equals("IDLE") || carry_idle) totalFrames = 8;
        else if (action.Equals("WATERING")) totalFrames = 2;
        
        Debug.Log("action: " + action + ", totalFrames: " + totalFrames);

        string fileName = _currentBodyPart.GetFileName(action, skinColor);
        int baseIndex = _currentBodyPart.GetSpriteIndex(action, pRotation);
        int multiplier = GetMultiplier(pBodyPart);
        int offset = (totalFrames * multiplier);
        
        
        int modifiedIndex = baseIndex + currentIndex;
        if (_currentBodyPart.bodyType.Equals(BodyType.CHEST) || _currentBodyPart.bodyType.Equals(BodyType.LEGS) || _currentBodyPart.bodyType.Equals(BodyType.FEET)) modifiedIndex += offset;
        string realFileName = fileName + "" + modifiedIndex;
        
        Sprite sprite = _cachedSpritesManager.GetSprite(realFileName);
        if (sprite != null)
        {
            _spriteRenderer.sprite = sprite;
            _spriteRenderer.enabled = true;
        } else _spriteRenderer.enabled = false;
    }
    private void LoadSpritesForPath(string[] pPath)
    {
        foreach (string path in pPath)
        {
            Object[] sprites = Resources.LoadAll(path, typeof(Sprite));
            foreach (Object sprite in sprites)
                _cachedSpritesManager.CachedSprites.Add((Sprite) sprite);
        }
    }

    private int GetMultiplier(BodyPart pBodyPart)
    {
        switch (pBodyPart.bodyType)
        {
            case BodyType.CHEST:
                Chest chest = pBodyPart as Chest;
                return chest.chestId;
            case BodyType.LEGS:
                Legs legs = pBodyPart as Legs;
                return legs.legsId;
            case BodyType.FEET:
                Feet feet = pBodyPart as Feet;
                return feet.feetId;
            default: return 1;
        }
    }
}
