using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using Object = UnityEngine.Object;

public class BodySprite
{
    public BodyType BodyType;
    public List<string> Paths = new List<string>();

    public BodySprite(BodyType pBodyType) => BodyType = pBodyType;
}

public class SpriteLoader : MonoBehaviour
{
    private CachedSpritesManager _cachedSpritesManager => CachedSpritesManager.Instance();

    [SerializeField] private Canvas _canvas;
    [SerializeField] private TextMeshProUGUI _loadingText;
    private int _dotsLoaded = 1;
    private float _timePassed;

    [SerializeField] private BodyPart[] _allBodyParts;

    public bool FinishedLoading;
    DateTime before = DateTime.Now;
    
    private void Awake()
    {
        //Application.targetFrameRate = 120;
        
        List<BodySprite> paths = new List<BodySprite>();

        foreach (BodyPart bodyPart in _allBodyParts)
        {
            if(bodyPart == null) continue;

            BodySprite bodySprite = new BodySprite(bodyPart.bodyType);
            
            bodySprite.Paths.AddRange(bodyPart.walkPathName);
            bodySprite.Paths.AddRange(bodyPart.axePathName);
            bodySprite.Paths.AddRange(bodyPart.pickaxePathName);
            bodySprite.Paths.AddRange(bodyPart.wateringCanPathName);
            bodySprite.Paths.AddRange(bodyPart.hoePathName);
            bodySprite.Paths.AddRange(bodyPart.carryPathName);
            bodySprite.Paths.AddRange(bodyPart.fishingPathName);
            bodySprite.Paths.AddRange(bodyPart.swordPathName);
            
            paths.Add(bodySprite);
        }
        
        Debug.Log("paths: " + paths.Count);

        StartCoroutine(LoadSpritesFromPath(paths.ToArray()));
    }

    private void Update()
    {
        if (FinishedLoading)
        {
            DateTime after = DateTime.Now; 
            TimeSpan duration = after.Subtract(before);
            Debug.Log("Duration in milliseconds: " + duration.Milliseconds);
            
            _canvas.gameObject.SetActive(false);
            
            Debug.LogWarning("totalSprites = " + _cachedSpritesManager.GetTotalSpriteSize());
            
            MainMenuTheme.Instance().DestroyNow();
            
            Utility.AddSceneIfNotLoaded("New Core");
            Utility.AddSceneIfNotLoaded("Main Level");
            
            Destroy(GetComponent<SpriteLoader>());
        } else
        {
            _timePassed += Time.deltaTime;
            if (_timePassed >= 0.25f)
            {
                string addition = "";
                for (int index = 0; index < _dotsLoaded; index++) addition += ".";
                
                _loadingText.text = $"Loading{addition}";
                
                _dotsLoaded++;
                if (_dotsLoaded >= 4) _dotsLoaded = 0;
                
                _timePassed = 0;
            }
        }
    }

    IEnumerator LoadSpritesFromPath(BodySprite[] pBodySprites) //string[] pPath
    {
        for (int index = 0; index < pBodySprites.Length; index++)
        {
            BodySprite bodySprite = pBodySprites[index];
            for (int pathIndex = 0; pathIndex < bodySprite.Paths.Count; pathIndex++)
            {
                string path = bodySprite.Paths[pathIndex];
                if (_cachedSpritesManager.LoadedPaths.Contains(path)) continue;
                
                Object[] sprites = Resources.LoadAll(path, typeof(Sprite));
                for (int spriteIndex = 0; spriteIndex < sprites.Length; spriteIndex++)
                {
                    Sprite sprite = (Sprite) sprites[spriteIndex];
                    if (_cachedSpritesManager.GetDictionary(bodySprite.BodyType).ContainsKey(sprite.name))
                    {
                        Debug.LogError($"SPRITE WITH NAME {sprite.name} already exist!");
                        continue;
                    }
                    
                    _cachedSpritesManager.AddSprite(sprite, bodySprite.BodyType);
                }
                _cachedSpritesManager.LoadedPaths.Add(path);
                yield return null;
            }
        }

        FinishedLoading = true;
    }
}
