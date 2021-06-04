using UnityEngine;

[System.Serializable]
public class SpriteLayout
{
    [System.Serializable]
    public struct SpriteData
    {
        public Sprite[] sprite;
    }

    public SpriteData[] sprites;

    public SpriteLayout(int size)
    {
        sprites = new SpriteData[size];
    }
}