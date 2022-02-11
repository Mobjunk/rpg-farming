using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using System.Collections;
using static Utility;

public class TimeManager : MonoBehaviour
{
    [Header("Display")]
    private static float _timeSpeedMultiplier = 2500;
    private static float _startTime;

    //private DateTime currentDate;
    //public DateTime currentGameTime;

    DateTime _startDate;
    public static TimeSpan elapsedTime => TimeSpan.FromSeconds(Time.time * _timeSpeedMultiplier - _startTime);

    public DateTime currentGameTime;
    void Start()
    {
        _startTime = Time.time * _timeSpeedMultiplier;
        _startDate = new DateTime(2012, 3, 1, 1, 1, 1);
    }
    void Update()
    {
        currentGameTime = _startDate.Add(elapsedTime);
    }
    
}
