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
    void Start()
    {
        _startTime = Time.time * _timeSpeedMultiplier;
        _startDate = new DateTime(2022,1,1,0,0,0);
    }
    void Update()
    {
        CurrentGameTime = _startDate.Add(ElapsedTime);
        Debug.Log(CurrentGameTime.DayOfWeek);

        //TimeTillDate(2022,1,1,23,1,20);

    }
    void TimeTillDate(DateTime pendDate)
    {
        TimeSpan timeTillEndDate = CurrentGameTime - pendDate;
        Debug.Log("tijd to go" + timeTillEndDate);
    }

}
