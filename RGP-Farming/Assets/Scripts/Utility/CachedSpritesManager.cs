using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CachedSpritesManager : Singleton<CachedSpritesManager>
{
    private BodyType[] _allTypes = { BodyType.HAT, BodyType.BODY, BodyType.EYES, BodyType.CHEST, BodyType.LEGS, BodyType.FEET, BodyType.BEARD };
    
    private Dictionary<string, Sprite> bodySprites = new Dictionary<string, Sprite>();
    private Dictionary<string, Sprite> hatSprites = new Dictionary<string, Sprite>();
    private Dictionary<string, Sprite> hairSprites = new Dictionary<string, Sprite>();
    private Dictionary<string, Sprite> eyesSprites = new Dictionary<string, Sprite>();
    private Dictionary<string, Sprite> chestSprites = new Dictionary<string, Sprite>();
    private Dictionary<string, Sprite> legsSprites = new Dictionary<string, Sprite>();
    private Dictionary<string, Sprite> feetSprites = new Dictionary<string, Sprite>();
    private Dictionary<string, Sprite> beardSprites = new Dictionary<string, Sprite>();
    
    //public List<Sprite> AllSprites = new List<Sprite>();
    public List<string> LoadedPaths = new List<string>();

    //public List<Sprite> CachedSprites = new List<Sprite>();

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public Sprite GetSprite(string pSpriteName, BodyType pBodyType)
    {
        return GetDictionary(pBodyType)[pSpriteName];
    }

    public void AddSprite(Sprite pSprite, BodyType pBodyType)
    {
        GetDictionary(pBodyType).Add(pSprite.name, pSprite);
    }

    public int GetTotalSpriteSize()
    {
        int totalSprites = 0;

        for (int index = 0; index < _allTypes.Length; index++)
            totalSprites += GetDictionary(_allTypes[index]).Count;
        
        return totalSprites;
    }

    public Dictionary<string, Sprite> GetDictionary(BodyType pBodyType)
    {
        switch (pBodyType)
        {
            case BodyType.BODY:
                return bodySprites;
            case BodyType.HAT:
                return hatSprites;
            case BodyType.HAIR:
                return hairSprites;
            case BodyType.EYES:
                return eyesSprites;
            case BodyType.CHEST:
                return chestSprites;
            case BodyType.LEGS:
                return legsSprites;
            case BodyType.FEET:
                return feetSprites;
            case BodyType.BEARD:
                return beardSprites;
            default:
                return null;
        }
    }
}