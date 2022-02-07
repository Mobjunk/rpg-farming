using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    public ShopUIManager _shopUIManager;
    
    public void StartGame()
    {
        Utility.UnloadScene("MainMenu");
        Utility.AddSceneIfNotLoaded("Core");
        Utility.AddSceneIfNotLoaded("Level");
    }
    
    public void Quit()
    {
        Application.Quit();
    }

    public void Interact()
    {
        _shopUIManager.Interact();
    }

    public void SwitchTab(int pIndex)
    {
        Player.Instance().CharacterUIManager.CurrentUIOpened.SwitchToTab(pIndex);
    }
}
