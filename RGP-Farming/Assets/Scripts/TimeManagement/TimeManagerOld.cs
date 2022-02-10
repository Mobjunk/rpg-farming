using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using System.Collections;
using static Utility;

public class TimeManagerOld : MonoBehaviour
{
	[Header("Display")]
	public TextMeshProUGUI timeText;
	private string ClockAMPM = "AM";

	[Tooltip("A Normal Day is 24 minutes")]
	public int timeSpeedMultiplier  = 1;

	[Header("Date")]
	private static float startTime;


	DateTime _startDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 8, 0, 0);

	private DateTime currentDate;
	
	[Tooltip("In Minutes")]
	public float startDayTime;
	[Tooltip("In Minutes")]
	public float startNightTime;

	private void Start()
	{
		startTime = Time.time * timeSpeedMultiplier;
	}
	void Update()
	{
		//SetDate();
	}	
	public void SetDate()
    {
		//Begint wanneer je de applicatie start
		int timeElapsed = (int)Time.time;

		int years = timeElapsed / YEAR;
		timeElapsed %= YEAR;

		int months = timeElapsed / MONTH;
		timeElapsed %= MONTH;

		int days = timeElapsed / DAY;
		timeElapsed %= DAY;

		int hours = timeElapsed / HOUR;
		timeElapsed %= HOUR;

		int minutes = timeElapsed;
		timeElapsed %= MINUTE;

		int seconds = timeElapsed;

		DateTime currentTime = new DateTime(_startDate.Year + years, _startDate.Month + months, _startDate.Day + days, _startDate.Hour + hours, _startDate.Minute + minutes, _startDate.Second + seconds);

		TimeSpan interval = currentTime - _startDate;

		Debug.Log($"maanden: {interval.TotalDays / 31}, dagen: {interval.Days}, uren: {interval.Hours}, minuten: {interval.Minutes}, secondes: {interval.Seconds}");

		if (hours >= 12)
		{
			hours %= 12;
			ClockAMPM = "PM";
		}
        else
		{
			ClockAMPM = "AM";
		}
		timeText.text = hours.ToString("00") + ":" + minutes.ToString("00") + ClockAMPM;
	}
	//TODO:
	//Wanneer je savonds niet meer buiten mag zijn ga je automatisch naar bed. Dan reset de tijd naar 8AM.
	// Dan moet hij de dag onthouden of hij er 1 bij op mag tellen. 
	// nog gekeken worden wanneer het am en pm is.
	// Modules is de %= en die haalt er altijd 12 af wanneer hij 12 wordt bijv.
	
}