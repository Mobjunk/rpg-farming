using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using System.Collections;
using static Utility;

public class TimeManager : MonoBehaviour
{
    [Header("Display")]
    public TextMeshProUGUI timeText;
    private string ClockAMPM = "AM";
    private static float timeSpeedMultiplier = 5000;
    private static float startTime;

    private DateTime currentDate;
    //Voor nu op 0 anders klopt de tijd niet.
    DateTime _startDate = new DateTime(0,0,0,0,0,0);
    DateTime _currentTime;
    public static TimeSpan elapsedTime => TimeSpan.FromSeconds(Time.time * timeSpeedMultiplier - startTime);
    void Start()
    {
        startTime = Time.time * timeSpeedMultiplier;
    }
    void Update()
    {
        DisplayTime();
        DayChecker();
    }
    //In ander script
    void DisplayTime()
    {
        int hours = elapsedTime.Hours;
        if (hours >= 12)
        {
            hours %= 12;
            ClockAMPM = "PM";
        }
        else
        {
            ClockAMPM = "AM";
        }  
        timeText.text = hours.ToString("00") + ":" + elapsedTime.Minutes.ToString("00") + ClockAMPM;
        _currentTime = _startDate.Add(elapsedTime);

        string day = _currentTime.DayOfWeek.ToString();
        if (day == "Monday")
        {
            Debug.Log("Goed voor elkaar");
        }
    }
    void SetDate(DateTime pdateTime)
    {
        startTime = Time.time * timeSpeedMultiplier;
        currentDate = pdateTime;
    }
    //Testing to check day.
    void DayChecker()
    {
        int hour = _currentTime.Hour;
        if(hour  > 7 && hour < 11)
        {
            Debug.Log("Goedmorgen");
        }
        if(hour > 12 && hour < 22)
        {
            Debug.Log("Goedemiddag");
        }
    }
}
