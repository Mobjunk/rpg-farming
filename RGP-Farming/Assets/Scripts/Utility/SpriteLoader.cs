using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Object = UnityEngine.Object;

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
        Application.targetFrameRate = 30;
        
        List<string> paths = new List<string>();

        foreach (BodyPart bodyPart in _allBodyParts)
        {
            paths.AddRange(bodyPart.walkPathName);
            paths.AddRange(bodyPart.axePathName);
            paths.AddRange(bodyPart.pickaxePathName);
            paths.AddRange(bodyPart.wateringCanPathName);
            paths.AddRange(bodyPart.hoePathName);
            paths.AddRange(bodyPart.carryPathName);
            paths.AddRange(bodyPart.fishingPathName);
            paths.AddRange(bodyPart.swordPathName);
        }

        StartCoroutine(LoadSpritesFromPath(paths.ToArray()));
    }

    private void Update()
    {
        if (FinishedLoading)
        {
            DateTime after = DateTime.Now; 
            TimeSpan duration = after.Subtract(before);
            Debug.Log("Duration in milliseconds: " + duration.Milliseconds);
            
            MainMenuTheme.Instance().DestroyNow();
            
            Utility.AddSceneIfNotLoaded("New Core");
            Utility.AddSceneIfNotLoaded("Main Level");
            
            Destroy(GetComponent<SpriteLoader>());
            _canvas.gameObject.SetActive(false);
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

    IEnumerator LoadSpritesFromPath(string[] pPath)
    {
        foreach (string path in pPath)
        {
            //if(path.Equals("Character/fish/hair_fish/braids_fish")) Debug.Log("TESTER " + _cachedSpritesManager.AllSprites.Count);
            if (_cachedSpritesManager.LoadedPaths.Contains(path))
            {
                //Debug.Log("Skipping path " + path);
                continue;
            }
            
            Object[] sprites = Resources.LoadAll(path, typeof(Sprite));
            foreach (Object sprite in sprites)
            {
                //Debug.Log(sprite.name);
                if (_cachedSpritesManager.AllSprites.Contains((Sprite) sprite))
                {
                    //Debug.Log("Skipping sprite: " + sprite.name);
                    continue;
                }
                
                _cachedSpritesManager.AllSprites.Add((Sprite) sprite);
            }
            _cachedSpritesManager.LoadedPaths.Add(path);
            yield return null;
        }

        FinishedLoading = true;
    }
}
