using UnityEngine;
using UnityEngine.SceneManagement;

public class SeasonalButton : GameUIManager
{
    private SeasonManager _seasonManager => SeasonManager.Instance();
    
    public void Winter()
    {
        _seasonManager.SeasonalCount = (int) SeasonValues.WINTER;
        _seasonManager.SetSeasonalIndex();
        _seasonManager.RefreshAllTilemaps();
    }
    
    public void Summer()
    {
        _seasonManager.SeasonalCount = (int)SeasonValues.SUMMER;
        _seasonManager.SetSeasonalIndex();
        _seasonManager.RefreshAllTilemaps();
    }
    
    public void Spring()
    {
        _seasonManager.SeasonalCount = (int) SeasonValues.SPRING;
        _seasonManager.SetSeasonalIndex();
        _seasonManager.RefreshAllTilemaps();
    }
    
    public void Autum()
    {
        _seasonManager.SeasonalCount = (int) SeasonValues.AUTUM;
        _seasonManager.SetSeasonalIndex();
        _seasonManager.RefreshAllTilemaps();
    }
    
    public void ChangeButtonSprites()
    {
        _seasonManager.DayNightSwitch();
    }
    
    public void ToggleSettings()
    {
        base.Open();
    }
    
    public void ExitGame()
    {
        Application.Quit();
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("Intro");
    }

    public override void Update()
    {
        base.Update();

        if (CharacterInputManager.Instance().EscapeAction.WasPressedThisFrame() && Player.CharacterUIManager.CurrentUIOpened == null && TimeSinceInteracting <= 0 && !DialogueManager.Instance().DialogueIsPlaying)
            Open();
    }

    public override void Open()
    {
        _seasonManager.TogglePanel();
        base.Open();
    }

    public override void Close()
    {
        _seasonManager.TogglePanel();
        base.Close();
    }
}
