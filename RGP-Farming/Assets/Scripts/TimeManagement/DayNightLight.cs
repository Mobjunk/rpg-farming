using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class DayNightLight : MonoBehaviour
{
    private float _intensity;
    public GameObject objectLight;
    public bool Nighttime = false;

    

    // Update is called once per frame
    void Update()
    {

        if (Nighttime) objectLight.GetComponent<Light2D>().intensity = 0.5f;
    }
}
