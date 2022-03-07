﻿using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CachedSpritesManager : Singleton<CachedSpritesManager>
{
    public List<Sprite> CachedSprites = new List<Sprite>();
    public List<string> LoadedPaths = new List<string>();

    public Sprite GetSprite(string pSpriteName)
    {
        return CachedSprites.FirstOrDefault(sprite => sprite.name.Equals(pSpriteName));
    }
}