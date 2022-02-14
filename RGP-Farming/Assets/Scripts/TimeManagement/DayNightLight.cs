using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering;

public class DayNightLight : MonoBehaviour
{
    private TimeManager _timeManager => TimeManager.Instance();
    public Volume volume;
    public Light2D Light;
    private void Awake()
    {
        volume = GetComponent<Volume>();
        if(volume == null)
        {
            Debug.Log("Error");
        }
    }
    void Update()
    {
        SetLighting();
    }
    /*public void SetLighting()
    {
        float hour = _timeManager.ElapsedTime.Hours;
        float minutes = _timeManager.ElapsedTime.Hours * 60 + _timeManager.ElapsedTime.Minutes;
        if (hour >= 18 && hour <= 24)
        {
            float weightValue =  1 + ((minutes - (24 * 60)) / (6 * 60) );
            volume.weight = weightValue;
            Debug.Log("Weight"+ weightValue);
        }
        if(hour >= 8 && hour < 18)
        {
            volume.weight = 0;
        }
    }*/

    public void SetLighting()
    {
        float hour = _timeManager.ElapsedTime.Hours;
        float minutes = _timeManager.ElapsedTime.Hours * 60 + _timeManager.ElapsedTime.Minutes;
        if (hour >= 18 && hour <= 24)
        {
            float weightValue = 1 + ((minutes - (24 * 60)) / (6 * 60));
            Light.intensity =  1 - weightValue;
            Debug.Log("Light.Intensity" + Light.intensity);
        }
        if (hour >= 8 && hour < 18)
        {
            volume.weight = 0;
        }
    }
}
