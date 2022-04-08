using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class SeasonManager : Singleton<SeasonManager>
{
    private TimeManager _timeManager => TimeManager.Instance();

    //This is the index of the sprite array for the tile data.
    public int SeasonalCount = 0;

    private bool Refreshed;
    [Header("Tilemaps that need refreshing")]
    [SerializeField] private Tilemap[] _tilemaps;

    
    /// <summary>
    //Theres loads of logic behind when a season starts 
    //For now it will just devide a year by 4 and asigns a season to wich part of the year it is.
    /// <summary>
    void CheckSeason()
    {      
        if (_timeManager.CurrentGameTime.Month >= 3)
        {
            //SeasonalCount = 
        }
    }
    public void RefreshAllTilemaps()
    {
        foreach (Tilemap tilemap in _tilemaps)
        {
            tilemap.RefreshAllTiles();
        }
    }
}
