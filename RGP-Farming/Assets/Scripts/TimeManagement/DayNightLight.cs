using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class DayNightLight : MonoBehaviour
{
    private float _intensity;
    public GameObject objectLight;

    public bool Daytime;
    void Start()
    {
       _intensity = objectLight.GetComponent<Light2D>().intensity;
    }

    // Update is called once per frame
    void Update()
    {
        if (!Daytime)
        {
            _intensity = 0.5f;
        }
        else _intensity = 1f;

    }
}
