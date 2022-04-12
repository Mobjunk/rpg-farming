using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SeasonalButton : MonoBehaviour
{
    private SeasonManager _seasonManager => SeasonManager.Instance();
    public void Winter()
    {
        _seasonManager.SeasonalCount = 2;
        _seasonManager.RefreshAllTilemaps();
    }
    public void Summer()
    {
        _seasonManager.SeasonalCount = 0;
        _seasonManager.RefreshAllTilemaps();
    }
    public void Spring()
    {
        _seasonManager.SeasonalCount = 3;
        _seasonManager.RefreshAllTilemaps();
    }
    public void Autum()
    {
        _seasonManager.SeasonalCount = 1;
        _seasonManager.RefreshAllTilemaps();
    }
    public void ChangeButtonSprites()
    {
        _seasonManager.DayNightSwitch();
    }
}
