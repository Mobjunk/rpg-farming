using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using System.Collections;
using static Utility;

public class DisplayTime : MonoBehaviour
{
    private TimeManager _timeManager => TimeManager.Instance();
    public TextMeshProUGUI TimeText;
    public TextMeshProUGUI DayText;
    private string _clockAMPM = "AM";

    void Update()
    {
        DisplayTimeUI();
    }
    void DisplayTimeUI()
    {
        int hours = _timeManager.ElapsedTime.Hours;
        string addition = string.Empty;
        if (hours >= 12)
        {
            if (hours == 12) addition = "12";
            hours %= 12;
            _clockAMPM = "PM";
            
        }
        else 
        {
            if (hours == 0) addition = "12";
            _clockAMPM = "AM";
        }
        TimeText.text = (addition.Equals(string.Empty) ? hours.ToString("00") : addition) + ":" + _timeManager.ElapsedTime.Minutes.ToString("00") + _clockAMPM;
        //currentGameTime = _startDate.Add(elapsedTime);

        string day = _timeManager.CurrentGameTime.DayOfWeek.ToString().Substring(0,3);
        DayText.text = day + ".";
    }
    void DayChecker()
    {
        int hour = _timeManager.CurrentGameTime.Hour;
        if (hour > 7 && hour < 11)
        {
            Debug.Log("Goedmorgen");
        }
        if (hour > 12 && hour < 22)
        {
            Debug.Log("Goedemiddag");
        }
    }
}
