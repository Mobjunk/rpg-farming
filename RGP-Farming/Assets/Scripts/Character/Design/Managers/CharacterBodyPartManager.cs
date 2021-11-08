using System;
using UnityEngine;

public abstract class CharacterBodyPartManager : MonoBehaviour
{
    private CharacterStateManager _characterStateManager;
    private SpriteRenderer _spriteRenderer;
    public BodyPart CurrentBodyPart;


    public virtual void Awake()
    {
        _characterStateManager = GetComponentInParent<CharacterStateManager>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        if (CurrentBodyPart != null)
        {
            _spriteRenderer.sprite = CurrentBodyPart.bSprites.sprites[0].sprite[1];
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
        UpdateBodyPart(CurrentBodyPart, _characterStateManager.GetDirection());
    }

    /// <summary>
    /// Handles updating the visual of a body part
    /// </summary>
    /// <param name="bodyPart">The body part scriptable object</param>
    /// <param name="rotation">The rotation of the character</param>
    /// <param name="darkSkin">Does the character have dark skin color</param>
    public void UpdateBodyPart(BodyPart bodyPart, int rotation, bool darkSkin = false)
    {
        CurrentBodyPart = bodyPart;

        SpriteLayout bodySprites = GetSprites(rotation);

        if (bodySprites == null)
        {
            _spriteRenderer.enabled = false;
            return;
        }

        int currentIndex = 1;
        if (_characterStateManager.GetCharacterState().ToString().Contains("WALKING_HOLD"))
            currentIndex = 3 + int.Parse(_characterStateManager.GetCharacterState().ToString().Replace("WALKING_HOLD_", ""));
        else if (_characterStateManager.GetCharacterState().ToString().Contains("WALKING_"))
            currentIndex = int.Parse(_characterStateManager.GetCharacterState().ToString().Replace("WALKING_", ""));
        else if(_characterStateManager.GetCharacterState().ToString().StartsWith("PICKUP"))
            currentIndex = 6 + (int.Parse(_characterStateManager.GetCharacterState().ToString().Replace("PICKUP_", "")));
        else if (_characterStateManager.GetCharacterState().ToString().Equals("IDLE_HOLD"))
            currentIndex = 4;
        
        if (bodySprites.sprites[0].sprite.Length != 0)
        {
            if (currentIndex >= bodySprites.sprites[darkSkin ? 1 : 0].sprite.Length)
            {
                //Debug.Log("bodySprites: " + gameObject.name);
                //Debug.Log("currentIndex: " + currentIndex);
                //Debug.Log("bodySprites.sprites[darkSkin ? 1 : 0].sprite.Length: " + bodySprites.sprites[darkSkin ? 1 : 0].sprite.Length);
                return;
            }
            
            _spriteRenderer.sprite = bodySprites.sprites[darkSkin ? 1 : 0].sprite[currentIndex];
            _spriteRenderer.enabled = true;
        } else _spriteRenderer.enabled = false;
    }

    /// <summary>
    /// Handles grabbign the right array of sprites based on the characters rotation
    /// </summary>
    /// <param name="rotation"></param>
    /// <returns></returns>
    private SpriteLayout GetSprites(int rotation)
    {
        if (CurrentBodyPart == null) return null;
        switch (rotation)
        {
            case 0:
                return CurrentBodyPart.bSprites;
            case 1:
                return CurrentBodyPart.lSprites;
            case 2:
                return CurrentBodyPart.rSprites;
            case 3:
                return CurrentBodyPart.tSprites;
        }

        return null;
    }
}
