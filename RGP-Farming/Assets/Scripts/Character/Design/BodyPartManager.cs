using UnityEngine;

public class BodyPartManager : MonoBehaviour
{
    public CachedSpritesManager CachedSpritesManager => CachedSpritesManager.Instance();
    
    private BodyPart _currentBodyPart;

    public BodyPart CurrentBodyPart
    {
        get => _currentBodyPart;
        set => _currentBodyPart = value;
    }
    
    public virtual void Awake()
    {
        LoadSprites(true);
    }

    public void LoadSprites(bool pWake = false)
    {
        if (_currentBodyPart != null)
        {
            LoadSpritesForPath(_currentBodyPart.walkPathName);
            if (!pWake)
            {
                LoadSpritesForPath(_currentBodyPart.axePathName);
                LoadSpritesForPath(_currentBodyPart.pickaxePathName);
                LoadSpritesForPath(_currentBodyPart.wateringCanPathName);
                LoadSpritesForPath(_currentBodyPart.hoePathName);
                LoadSpritesForPath(_currentBodyPart.carryPathName);
                LoadSpritesForPath(_currentBodyPart.fishingPathName);
                LoadSpritesForPath(_currentBodyPart.swordPathName);
            }
        }
    }
    
    private void LoadSpritesForPath(string[] pPath)
    {
        foreach (string path in pPath)
        {
            if (CachedSpritesManager.LoadedPaths.Contains(path)) continue;
            
            Object[] sprites = Resources.LoadAll(path, typeof(Sprite));
            foreach (Object sprite in sprites)
            {
                if (CachedSpritesManager.CachedSprites.Contains((Sprite) sprite)) continue;
                
                CachedSpritesManager.CachedSprites.Add((Sprite) sprite);
            }
            CachedSpritesManager.LoadedPaths.Add(path);
        }
    }

    public int GetMultiplier(BodyPart pBodyPart)
    {
        switch (pBodyPart.bodyType)
        {
            case BodyType.CHEST:
                Chest chest = pBodyPart as Chest;
                return chest.chestId;
            case BodyType.LEGS:
                Legs legs = pBodyPart as Legs;
                return legs.legsId;
            case BodyType.FEET:
                Feet feet = pBodyPart as Feet;
                return feet.feetId;
            case BodyType.EYES:
                Eyes eyes = pBodyPart as Eyes;
                return eyes.eyesId;
            default: return 1;
        }
    }
}