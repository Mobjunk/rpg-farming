using UnityEngine;
using UnityEngine.UI;

public class CharacterUIBodyPartManager<T> : BodyPartManager
{
    private Image _image;
    private CharacterDesignUIManager _characterDesignUIManager => CharacterDesignUIManager.Instance();
    
    public virtual void Awake()
    {
        base.Awake();
        
        _image = GetComponent<Image>();
        
        _characterDesignUIManager.OnCharacterChange += OnCharacterChange;
    }

    private void OnCharacterChange()
    {
        UpdateBodyPart(CurrentBodyPart, _characterDesignUIManager.CurrentDirection, _characterDesignUIManager.CharacterSkinColor, _characterDesignUIManager.CharacterHairColor);
    }

    private void UpdateBodyPart(BodyPart pBodyPart, int pRotation, int pSkinColor, int pHairColor)
    {
        CurrentBodyPart = pBodyPart;

        if (CurrentBodyPart == null)
        {
            _image.enabled = false;
            return;
        }

        string fileName = CurrentBodyPart.GetFileName("IDLE", !CurrentBodyPart.bodyType.Equals(BodyType.BODY) ? 0 : pSkinColor);
        int baseIndex = CurrentBodyPart.GetSpriteIndex("IDLE", pRotation);
        
        int multiplier = GetMultiplier(CurrentBodyPart);
        if (CurrentBodyPart.UseHairColor()) multiplier = pHairColor;
        
        if (CurrentBodyPart.RequiresMultiplier()) baseIndex += (8 * multiplier);
        
        Sprite sprite = CachedSpritesManager.GetSprite(fileName + "" + baseIndex, CurrentBodyPart.bodyType);

        if (sprite != null)
        {
            _image.sprite = sprite;
            _image.enabled = true;
        } else _image.enabled = false;
    }

    public void SetBodyPart(T pBodyPart)
    {
        CurrentBodyPart = pBodyPart as BodyPart;
        LoadSprites();
    }

    public void UpdateBodyPart(int pIndex, ref int pIndexUsed, T[] pBodyParts)
    {
        pIndexUsed = Utility.WrapIndex(pIndex, pBodyParts.Length, pIndexUsed);
        SetBodyPart(pBodyParts[pIndexUsed]);
        OnCharacterChange();
    }

    public void Test()
    {
        OnCharacterChange();
    }
}