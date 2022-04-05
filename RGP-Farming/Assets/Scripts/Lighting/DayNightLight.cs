using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering;

public class DayNightLight : MonoBehaviour
{
    private TimeManager _timeManager => TimeManager.Instance();

    private Light2D _light;
    private void Awake()
    {
        _light = GetComponent<Light2D>();
        if(_light == null)
        {
            Debug.Log("No light found in the scene");
        }
    }
    void Update()
    {
        SetLighting();
    }
    public void SetLighting()
    {
        float hour = _timeManager.CurrentGameTime.Hour;
        float minutes = _timeManager.CurrentGameTime.Hour * 60 + _timeManager.CurrentGameTime.Minute;
        if (hour >= 18 && hour <= 23)
        {
            float weightValue = 1 + ((minutes - (24 * 60)) / (6 * 60));
            
            _light.intensity = 1 - weightValue;
            
        }
        if(hour >= 23 && hour <= 24)
        {
            _light.intensity = 0.16f;
        }
        if (hour >= _timeManager.StartingHour && hour < 18)
        {
            _light.intensity = 1;
        }
    }
}
