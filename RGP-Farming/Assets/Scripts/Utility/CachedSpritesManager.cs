using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CachedSpritesManager : Singleton<CachedSpritesManager>
{
    public List<Sprite> AllSprites = new List<Sprite>();
    public List<string> LoadedPaths = new List<string>();

    public List<Sprite> CachedSprites = new List<Sprite>();

    public Sprite GetSprite(string pSpriteName)
    {
        foreach (Sprite sprite in AllSprites.ToArray())
        {
            if (sprite.name.Equals(pSpriteName)) return sprite;
        }
        return null;
        //return AllSprites.FirstOrDefault(sprite => sprite.name.Equals(pSpriteName));
    }

    public Sprite GetCachedSprite(string pSpriteName)
    {
        foreach (Sprite sprite in CachedSprites.ToArray())
        {
            if (sprite.name.Equals(pSpriteName)) return sprite;
        }
        return null;
    }
}