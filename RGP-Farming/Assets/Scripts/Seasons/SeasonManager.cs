using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using TMPro;
using UnityEngine.UI;

public class SeasonManager : Singleton<SeasonManager>
{
    private TimeManager _timeManager => TimeManager.Instance();
    private TilemapManager _tilemapManager => TilemapManager.Instance();

    //This is the index of the sprite array for the tile data.
    // Summer = 0 , Autumn = 1 , Winter = 2 , Spring = 3.
    [HideInInspector]
    public int SeasonalCount = 0;

    [SerializeField] private FMODUnity.StudioEventEmitter _studioEventEmitter;
    
    //bla bla static seasoncount = SeasonalCount;

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI _timeText;
    [SerializeField] private Button[] _buttons;
    [SerializeField] private Sprite[] _UISpritesDay;
    [SerializeField] private Sprite[] _UISpritesNight;
    [SerializeField] private GameObject _fullSeasonPanel;

    private bool _night = true;
    private bool _panelOn;

    
    private Tilemap[] _tilemaps => _tilemapManager.AllTilemaps;

    private void Update()
    {
        SetSeasonalIndex();
    }
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
    public void SetSeasonalIndex()
    {
        SeasonalRuleTile.SeasonalIndex = SeasonalCount;
        if (_studioEventEmitter != null)
            _studioEventEmitter.SetParameter("season", SeasonalCount);
        else _studioEventEmitter = GameObject.FindWithTag("Music").GetComponent<FMODUnity.StudioEventEmitter>();
    }
    public void RefreshAllTilemaps()
    {
        foreach (Tilemap tilemap in _tilemaps)
        {
            tilemap.RefreshAllTiles();
        }
    }
    public void DayNightSwitch()
    {
        if (!_night)
        {
            for (int i = 0; i < 4; i++)
            {
                _buttons[i].image.sprite = _UISpritesDay[i];
                _timeText.text = "Day";
                _night = true;
            }
        }
        else if (_night)
        {
            for (int i = 0; i < 4; i++)
            {
                _buttons[i].image.sprite = _UISpritesNight[i];
                _timeText.text = "Night";
                _night = false;

            }
        }
    }
    public void TogglePanel()
    {
        if (_panelOn)
        {
            _fullSeasonPanel.gameObject.SetActive(false);
            _panelOn = false;
        }
        else if (!_panelOn)
        {
            _fullSeasonPanel.gameObject.SetActive(true);
            _panelOn = true;
        }
    }
}
