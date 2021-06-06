using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonManager : MonoBehaviour
{

    public void StartGame()
    {
        Utility.UnloadScene("MainMenu");
        Utility.AddSceneIfNotLoaded("Level");
        Utility.AddSceneIfNotLoaded("Core");
    }
    
    public void Quit()
    {
        Application.Quit();
    }
    
}
