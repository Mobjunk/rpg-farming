using UnityEngine;

public class CharacterBodyManager : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private BodyPart currentBodyPart;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (currentBodyPart != null)
        {
            //TODO: Change 0 to the skin color (0 light, 1 dark)...
            spriteRenderer.sprite = currentBodyPart.tSprites.sprites[0].sprite[1];
            spriteRenderer.enabled = true;
        }
    }

    public void UpdateBodyPart(BodyPart bodyPart, int rotation, bool darkSkin = false)
    {
        currentBodyPart = bodyPart;

        SpriteLayout bodySprites = GetSprites(rotation);
        
        spriteRenderer.sprite = bodySprites.sprites[darkSkin ? 1 : 0].sprite[1];
        spriteRenderer.enabled = true;
    }

    private SpriteLayout GetSprites(int rotation)
    {
        switch (rotation)
        {
            case 0:
                return currentBodyPart.tSprites;
            case 1:
                return currentBodyPart.rSprites;
            case 2:
                return currentBodyPart.bSprites;
            case 3:
                return currentBodyPart.lSprites;
        }

        return null;
    }
}
