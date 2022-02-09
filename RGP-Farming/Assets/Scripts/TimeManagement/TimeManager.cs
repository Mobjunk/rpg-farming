using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class TimeManager : MonoBehaviour
{
	public TextMeshProUGUI timeText;
	private float timeValue;
	private float hourCount;
	private float minuteCount;
	private string ClockAMPM = "AM";

	[Tooltip("A Normal Day is 24 minutes")]
	public float TimeSpeedMultiplier;
	[Tooltip("In Minutes")]
	public float startDayTime;
	[Tooltip("In Minutes")]
	public float startNightTime;
	void Update()
	{
		HandleTime();
	}

	public void HandleTime()
	{
		//Time.Timespan elapsed time; Time.time in de project settings aanpassen.
		// Crops kunnen gebruik maken van Time.unscaledTime zodat ze niet dezelfde waarde gebruiken als time.time [Nighel]
		//Time multiplier kan nog aanpassen (Time.timescale past ook delta time aan. [kan ook project settings])
		timeValue += Time.deltaTime * TimeSpeedMultiplier;
		hourCount = (int)timeValue / 60;
		minuteCount = (int)timeValue - (int)hourCount * 60;
		// Full Day
		if (timeValue >= 1440)
		{
			timeValue = 0;
			//Reset a day
		}
		// First half of Day
		if (timeValue >= 720)
		{
			ClockAMPM = "PM";
			hourCount -= 12;
		}
		// Second half of Day
		else
		{
			ClockAMPM = "AM";
		}

		if (hourCount < 1)
		{
			hourCount = 12;
		}
		//Display in UI
		timeText.text = hourCount.ToString("00") + ":" + minuteCount.ToString("00") + ClockAMPM;
	}
}