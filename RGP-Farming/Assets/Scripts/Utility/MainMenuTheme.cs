using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuTheme : Singleton<MainMenuTheme>
{
    void Awake()
    {
        DontDestroyOnLoad(this);
    }

    public void DestroyNow()
    {
        DestroyImmediate(gameObject);
    }
}
