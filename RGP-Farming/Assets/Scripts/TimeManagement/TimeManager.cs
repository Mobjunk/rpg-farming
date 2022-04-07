using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using System.Collections;
using static Utility;

public class TimeManager : Singleton<TimeManager>
{
    [Header("Display")]
    private static float _timeSpeedMultiplier ;
    private static float _startTime;

    public float MinutesADay; 
    public int StartingHour;

    DateTime _startDate;
    //public TimeSpan ElapsedTime => TimeSpan.FromSeconds(Time.time * _timeSpeedMultiplier - _startTime);

    public TimeSpan ElapsedTime;
    public DateTime CurrentGameTime;

    void Start()
    {
        _timeSpeedMultiplier = 86400 / (MinutesADay * 60);
        _startTime = Time.time * _timeSpeedMultiplier;
        _startDate = new DateTime(2022,1,1,StartingHour,0,0);
        
        //Calculates the modifier based on seconds in a day.
        Debug.Log("Game starts already");
        Debug.Log(_startTime);
        Debug.Log("Time = " + Time.time);
    }
    void Update()
    {
        ElapsedTime = TimeSpan.FromSeconds(Time.time * _timeSpeedMultiplier - _startTime);

        CurrentGameTime = _startDate.Add(ElapsedTime);
    }
    public DateTime GetNewDate(int pAddedYears = 0, int pAddedMonths = 0, int pAddedDays = 0, int pAddedHours = 0, int pAddedMinutes = 0, int pAddedSeconds = 0)
    {
        return new DateTime(CurrentGameTime.Year + pAddedYears, CurrentGameTime.Month + pAddedMonths, CurrentGameTime.Day + pAddedDays, CurrentGameTime.Hour + pAddedHours, CurrentGameTime.Minute + pAddedMinutes, CurrentGameTime.Second + pAddedSeconds);
    }
    
    public void SetMorningDate()
    {
        _startTime = Time.time * _timeSpeedMultiplier;   
        _startDate = new DateTime(CurrentGameTime.Year, CurrentGameTime.Month, CurrentGameTime.Day + 1, StartingHour, 0, 0);
    }
}
