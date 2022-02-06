using System;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class Utility
{
    public static void AddSceneIfNotLoaded(string pSceneName)
    {
        Scene playerScene = SceneManager.GetSceneByName(pSceneName);
        if (!playerScene.IsValid())
        {
            SceneManager.LoadScene(pSceneName, LoadSceneMode.Additive);
        }
    }

    public static void UnloadScene(string pSceneName)
    {
        SceneManager.UnloadSceneAsync(pSceneName);
    }

    public static void SwitchScenes(string pOldSceneName, string pNewSceneName)
    {
        SceneManager.UnloadSceneAsync(pOldSceneName);
        AddSceneIfNotLoaded(pNewSceneName);
    }

    //TODO: Find a better name for this
    /// <summary>
    /// Handles changing a int based on the parameter
    /// </summary>
    /// <param name="pIncrease">Increase the integer</param>
    /// <param name="pCurrentInt">Reference to the int your changing</param>
    /// <param name="pMaxInteger">The max of the int your changing</param>
    public static void HandleChange(bool pIncrease, ref int pCurrentInt, int pMaxInteger)
    {
        pCurrentInt += pIncrease ? 1 : -1;
        if (pCurrentInt < 0) pCurrentInt = pMaxInteger;
        if (pCurrentInt >= pMaxInteger) pCurrentInt = 0;
    }
    
    public static GameObject FindObject(this GameObject pParent, string pName)
    {
        Transform[] trs= pParent.GetComponentsInChildren<Transform>(true);
        foreach(Transform t in trs){
            if(t.name == pName){
                return t.gameObject;
            }
        }
        return null;
    }
    
    public static Vector2 GetAnchor(AnchorsPresets pAnchor)
    {
        switch (pAnchor)
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
                throw new ArgumentOutOfRangeException(nameof(pAnchor), pAnchor, null);
        }
    }

    public static bool CanInteractWithTile(Grid pGrid, Vector3Int pTilePosition, GameObject[] pTileChecker)
    {
        for (int index = 0; index < pTileChecker.Length; index++)
        {
            Vector3Int pos = pGrid.WorldToCell(pTileChecker[index].transform.position);
            int distance = Mathf.FloorToInt(Vector2.Distance(new Vector2(pTilePosition.x, pTilePosition.y), new Vector2(pos.x, pos.y)));

            if (distance <= 1) return true;
        }

        return false;
    }
    
    
    public static string UppercaseFirst(string pInput)
    {
        return pInput.First().ToString().ToUpper() + pInput.Substring(1);
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
