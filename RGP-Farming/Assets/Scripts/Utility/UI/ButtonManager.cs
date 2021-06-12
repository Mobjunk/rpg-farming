using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    public ShopUIManager shopUIManager;
    
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

    public void Interact()
    {
        shopUIManager.Interact();
    }

    public void SwitchTab(int index)
    {
        Player.Instance().CharacterUIManager.CurrentUIOpened.SwitchToTab(index);
    }
}
