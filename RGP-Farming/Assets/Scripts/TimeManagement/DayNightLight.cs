using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering;

public class DayNightLight : TimeManager
{
    //public bool Nighttime = false;
    public Volume volume;
    public float volumeValue;
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
    public void SetLighting()
    {
        float hour = elapsedTime.Minutes;
        Debug.Log("Hour"+ hour);

        // 8 uur sochtends volledig licht.

        // 6 uur savonds begint het donker te worden
        

        // 12 uur moet het pikke donker zijn.

        // 1 uur character nokkie.

        if (hour > 18 && hour < 24)
        {
            Debug.Log("Works");
            float weightValue = (hour - 18) * (1 / 6);
            volume.weight = weightValue;
            Debug.Log("Weight"+ weightValue);

        }
    }
}
