using UnityEngine;

public class SeasonalButton : MonoBehaviour
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
        _seasonManager.TogglePanel();
    }
    
    public void ExitGame()
    {
        Application.Quit();
    }
}
