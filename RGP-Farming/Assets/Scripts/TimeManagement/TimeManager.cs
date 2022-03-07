using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using System.Collections;
using static Utility;

public class TimeManager : Singleton<TimeManager>
{
    [Header("Display")]
    private static float _timeSpeedMultiplier = 5000;
    private static float _startTime;

    //private DateTime currentDate;
    //public DateTime currentGameTime;

    DateTime _startDate;
    public TimeSpan ElapsedTime => TimeSpan.FromSeconds(Time.time * _timeSpeedMultiplier - _startTime);

    public DateTime CurrentGameTime;
    void Awake()
    {
        _startTime = Time.time * _timeSpeedMultiplier;
        _startDate = new DateTime(2022,1,1,0,0,0);
        CurrentGameTime = _startDate.Add(ElapsedTime);
    }
    void Update()
    {
        CurrentGameTime = _startDate.Add(ElapsedTime);
    }

    public DateTime GetNewDate(int pAddedYears = 0, int pAddedMonths = 0, int pAddedDays = 0, int pAddedHours = 0, int pAddedMinutes = 0, int pAddedSeconds = 0)
    {
        return new DateTime(CurrentGameTime.Year + pAddedYears, CurrentGameTime.Month + pAddedMonths, CurrentGameTime.Day + pAddedDays, CurrentGameTime.Hour + pAddedHours, CurrentGameTime.Minute + pAddedMinutes, CurrentGameTime.Second + pAddedSeconds);
    }
}
