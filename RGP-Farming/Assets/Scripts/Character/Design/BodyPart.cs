using UnityEngine;

public class BodyPart : ScriptableObject
{
    [Header("Type")] public BodyType bodyType;

    [Header("Bottom sprites")]
    public SpriteLayout defaultBottomSprites = new SpriteLayout(8);
    
    [Header("Top sprites")]
    public SpriteLayout defaultTopSprites = new SpriteLayout(8);
    
    [Header("Right sprites")]
    public SpriteLayout defaultRightSprites = new SpriteLayout(8);
    
    [Header("Left sprites")]
    public SpriteLayout defaultLeftSprites = new SpriteLayout(8);
    
}
