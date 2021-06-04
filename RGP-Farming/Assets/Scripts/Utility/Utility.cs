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
}