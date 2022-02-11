using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using System.Collections;
using static Utility;

public class DisplayTime : TimeManager
{
    public TextMeshProUGUI TimeText;
    private string _clockAMPM = "AM";

    void Update()
    {
        DisplayTimeUI();
    }
    void DisplayTimeUI()
    {
        int hours = elapsedTime.Hours;
        if (hours >= 12)
        {
            hours %= 12;
            _clockAMPM = "PM";
        }
        else
        {
            _clockAMPM = "AM";
        }
        TimeText.text = hours.ToString("00") + ":" + elapsedTime.Minutes.ToString("00") + _clockAMPM;
        //currentGameTime = _startDate.Add(elapsedTime);

        string day = currentGameTime.DayOfWeek.ToString();
        if (day == "Monday")
        {
        }
    }
    void DayChecker()
    {
        int hour = currentGameTime.Hour;
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
