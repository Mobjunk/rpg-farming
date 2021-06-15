using System;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class Utility
{
    public static void AddSceneIfNotLoaded(string sceneName)
    {
        Scene playerScene = SceneManager.GetSceneByName(sceneName);
        if (!playerScene.IsValid())
        {
            SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
        }
    }

    public static void UnloadScene(string sceneName)
    {
        SceneManager.UnloadSceneAsync(sceneName);
    }

    public static void SwitchScenes(string oldSceneName, string newSceneName)
    {
        SceneManager.UnloadSceneAsync(oldSceneName);
        AddSceneIfNotLoaded(newSceneName);
    }

    //TODO: Find a better name for this
    /// <summary>
    /// Handles changing a int based on the parameter
    /// </summary>
    /// <param name="increase">Increase the integer</param>
    /// <param name="currentInt">Reference to the int your changing</param>
    /// <param name="maxInteger">The max of the int your changing</param>
    public static void HandleChange(bool increase, ref int currentInt, int maxInteger)
    {
        currentInt += increase ? 1 : -1;
        if (currentInt < 0) currentInt = maxInteger;
        if (currentInt >= maxInteger) currentInt = 0;
    }
    
    public static GameObject FindObject(this GameObject parent, string name)
    {
        Transform[] trs= parent.GetComponentsInChildren<Transform>(true);
        foreach(Transform t in trs){
            if(t.name == name){
                return t.gameObject;
            }
        }
        return null;
    }
    
    public static Vector2 GetAnchor(AnchorsPresets anchor)
    {
        switch (anchor)
        {
            case AnchorsPresets.TOP_LEFT:
                return new Vector2(0, 1);
            case AnchorsPresets.TOP:
                return new Vector2(0.5f, 1);
            case AnchorsPresets.TOP_RIGHT:
                return new Vector2(1, 1);
            case AnchorsPresets.RIGHT:
                return new Vector2(1, 0.5f);
            case AnchorsPresets.BOTTOM_RIGHT:
                return new Vector2(1, 0);
            case AnchorsPresets.BOTTOM:
                return new Vector2(0.5f, 0);
            case AnchorsPresets.BOTTOM_LEFT:
                return new Vector2(0, 0);
            case AnchorsPresets.LEFT:
                return new Vector2(0, 0.5f);
            case AnchorsPresets.CENTER:
                return new Vector2(0.5f, 0.5f);
            default:
                throw new ArgumentOutOfRangeException(nameof(anchor), anchor, null);
        }
    }

    public static bool CanInteractWithTile(Grid grid, Vector3Int tilePosition, GameObject[] tileChecker)
    {
        for (int index = 0; index < tileChecker.Length; index++)
        {
            Vector3Int pos = grid.WorldToCell(tileChecker[index].transform.position);
            int distance = Mathf.FloorToInt(Vector2.Distance(new Vector2(tilePosition.x, tilePosition.y), new Vector2(pos.x, pos.y)));

            if (distance <= 1) return true;
        }

        return false;
    }
    
    
    public static string UppercaseFirst(string input)
    {
        return input.First().ToString().ToUpper() + input.Substring(1);
    }
}

public enum AnchorsPresets
{
    TOP_LEFT,
    TOP,
    TOP_RIGHT,
    RIGHT,
    BOTTOM_RIGHT,
    BOTTOM,
    BOTTOM_LEFT,
    LEFT,
    CENTER
}
