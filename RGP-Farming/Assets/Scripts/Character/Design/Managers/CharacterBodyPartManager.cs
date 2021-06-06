using System;
using UnityEngine;

public abstract class CharacterBodyPartManager : MonoBehaviour
{
    private CharacterStateManager characterStateManager;
    private SpriteRenderer spriteRenderer;
    private BodyPart currentBodyPart;

    public BodyPart CurrentBodyPart
    {
        get => currentBodyPart;
        set => currentBodyPart = value;
    }

    public virtual void Awake()
    {
        characterStateManager = GetComponentInParent<CharacterStateManager>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (currentBodyPart != null)
        {
            spriteRenderer.sprite = currentBodyPart.bSprites.sprites[0].sprite[1];
            spriteRenderer.enabled = true;
        }

        characterStateManager.OnStateChanged += OnStateChange;
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
        UpdateBodyPart(currentBodyPart, characterStateManager.GetDirection());
    }

    /// <summary>
    /// Handles updating the visual of a body part
    /// </summary>
    /// <param name="bodyPart">The body part scriptable object</param>
    /// <param name="rotation">The rotation of the character</param>
    /// <param name="darkSkin">Does the character have dark skin color</param>
    public void UpdateBodyPart(BodyPart bodyPart, int rotation, bool darkSkin = false)
    {
        currentBodyPart = bodyPart;

        SpriteLayout bodySprites = GetSprites(rotation);

        if (bodySprites == null)
        {
            spriteRenderer.enabled = false;
            return;
        }

        int currentIndex = 1;
        if (characterStateManager.GetCharacterState().ToString().Contains("WALKING"))
            currentIndex = int.Parse(characterStateManager.GetCharacterState().ToString().Replace("WALKING_", ""));
        else if(characterStateManager.GetCharacterState().ToString().StartsWith("PICKUP"))
            currentIndex = 3 + (int.Parse(characterStateManager.GetCharacterState().ToString().Replace("PICKUP_", "")));
        
        if (bodySprites.sprites[0].sprite.Length != 0)
        {
            if (currentIndex >= bodySprites.sprites[darkSkin ? 1 : 0].sprite.Length)
            {
                //Debug.Log("bodySprites: " + gameObject.name);
                //Debug.Log("currentIndex: " + currentIndex);
                //Debug.Log("bodySprites.sprites[darkSkin ? 1 : 0].sprite.Length: " + bodySprites.sprites[darkSkin ? 1 : 0].sprite.Length);
                return;
            }
            
            spriteRenderer.sprite = bodySprites.sprites[darkSkin ? 1 : 0].sprite[currentIndex];
            spriteRenderer.enabled = true;
        } else spriteRenderer.enabled = false;
    }

    /// <summary>
    /// Handles grabbign the right array of sprites based on the characters rotation
    /// </summary>
    /// <param name="rotation"></param>
    /// <returns></returns>
    private SpriteLayout GetSprites(int rotation)
    {
        if (currentBodyPart == null) return null;
        switch (rotation)
        {
            case 0:
                return currentBodyPart.bSprites;
            case 1:
                return currentBodyPart.lSprites;
            case 2:
                return currentBodyPart.rSprites;
            case 3:
                return currentBodyPart.tSprites;
        }

        return null;
    }
}
