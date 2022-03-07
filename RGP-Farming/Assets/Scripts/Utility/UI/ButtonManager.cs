using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    
    public void StartGame()
    {
        Utility.UnloadScene("MainMenu");
        Utility.AddSceneIfNotLoaded("Character Design");
        //Utility.AddSceneIfNotLoaded("Core");
        //Utility.AddSceneIfNotLoaded("Level");
    }
    
    public void Quit()
    {
        Application.Quit();
    }
}
