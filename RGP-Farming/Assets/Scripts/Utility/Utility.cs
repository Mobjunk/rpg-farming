using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using Object = UnityEngine.Object;

public static class Utility
{
    
    public const int N = 1 << 0;
    public const int NE = 1 << 1;
    public const int E = 1 << 2;
    public const int SE = 1 << 3;
    public const int S = 1 << 4;
    public const int SW = 1 << 5;
    public const int W = 1 << 6;
    public const int NW = 1 << 7;
    
    public const int NORTH = 0;
    public const int NORTH_EAST = 1;
    public const int EAST = 2;
    public const int SOUTH_EAST = 3;
    public const int SOUTH = 4;
    public const int SOUTH_WEST = 5;
    public const int WEST = 6;
    public const int NORTH_WEST = 7;

    public static int WrapIndex(int pIndex, int pMaxLength, int pIndexUsed)
    {
        int newIndex = pIndexUsed + pIndex;
        if (newIndex < 0) newIndex = pMaxLength - 1;
        else if (newIndex >= pMaxLength) newIndex = 0;
        return newIndex;
    }

    public static void SetAnimator(Animator pAnimator, string pName, bool pSet, bool pStopAnimation = false)
    {
        pAnimator.SetBool(pName, pSet);
        if(pStopAnimation) CoroutineManager.Instance().StartCoroutine(ResetAnimation(pAnimator, pName, GetAnimationClipTime(pAnimator, pName)));
    }

    public static void SetAnimator(Animator pAnimator, string pName, float pSet, bool pStopAnimation = false)
    {
        pAnimator.SetFloat(pName, pSet);
        if(pStopAnimation) CoroutineManager.Instance().StartCoroutine(ResetAnimation(pAnimator, pName, GetAnimationClipTime(pAnimator, pName)));
    }

    public static void SetAnimator(Animator pAnimator, string pName, int pSet, bool pStopAnimation = false)
    {
        pAnimator.SetInteger(pName, pSet);
        if(pStopAnimation) CoroutineManager.Instance().StartCoroutine(ResetAnimation(pAnimator, pName, GetAnimationClipTime(pAnimator, pName)));
    }

    static IEnumerator ResetAnimation(Animator pAnimator, string pAnimationName, float pAnimationTime)
    {
        yield return new WaitForSeconds(pAnimationTime);
        pAnimator.SetBool(pAnimationName, false);
    }
    
    public static float GetAnimationClipTime(Animator pAnimator, string pAnimationName)
    {
        AnimationClip[] clips = pAnimator.runtimeAnimatorController.animationClips;
        switch (pAnimationName)
        {
            case "pickaxe_swing":
            case "axe_swing":
                return 0.75f;
        }
        return (from clip in clips where clip.name.ToLower().Equals(pAnimationName.ToLower()) select clip.length).FirstOrDefault();
    }

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

    public static bool CanInteractWithTile(Grid pGrid, Vector3Int pTilePosition, GameObject[] pTileChecker, float pDistanceCheck = 1)
    {
        for (int index = 0; index < pTileChecker.Length; index++)
        {
            Vector3Int pos = pGrid.WorldToCell(pTileChecker[index].transform.position);
            int distance = Mathf.FloorToInt(Vector2.Distance(new Vector2(pTilePosition.x, pTilePosition.y), new Vector2(pos.x, pos.y)));
            
            if (distance <= pDistanceCheck) return true;
        }

        return false;
    }
    
    
    public static string UppercaseFirst(string pInput)
    {
        return pInput.First().ToString().ToUpper() + pInput.Substring(1);
    }
    
    public static int[] GetPositionForDirection(int pX, int pY, int pDir)
    {
        switch (pDir)
        {
            case 0: //North
                pY++;
                break;
            case 1: //North East
                pX++;
                pY++;
                break;
            case 2: //East
                pX++;
                break;
            case 3: //South east
                pX++;
                pY--;
                break;
            case 4: //South
                pY--;
                break;
            case 5: //South West
                pX--;
                pY--;
                break;
            case 6: //West
                pX--;
                break;
            case 7: //North West
                pX--;
                pY++;
                break;
        }
        return new int[] { pX, pY };
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
