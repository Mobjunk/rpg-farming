using UnityEngine;

public class BodyPart : ScriptableObject
{
    [Header("Type")] public BodyType bodyType;
    
    [Header("Gender settings")]
    public bool male;
    public bool female;
    
    [Header("Bottom sprites")] public SpriteLayout bSprites = new SpriteLayout(2);
    [Header("Left sprites")] public SpriteLayout lSprites = new SpriteLayout(2);
    [Header("Right sprites")] public SpriteLayout rSprites = new SpriteLayout(2);
    [Header("Top sprites")] public SpriteLayout tSprites = new SpriteLayout(2);
}
