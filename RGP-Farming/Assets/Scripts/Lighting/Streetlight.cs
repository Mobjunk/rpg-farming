using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering;
using static Utility;

public class Streetlight : MonoBehaviour
{
    private TimeManager _timeManager => TimeManager.Instance();

    [SerializeField] private Sprite[] _sprites;
    [SerializeField] private GameObject[] _pointLights;
    private Light2D _light;
    private bool _lit;

    public int turnOnHour;

    public bool Flicker;

    private float nextActionTime = 0.0f;
    public float timeBetweenFlicker;
    private void Awake()
    {
        _light = GetComponent<Light2D>();
    }
    private void Update()
    {
        SwitchSpriteOnLit();
        if(_timeManager.CurrentGameTime.Hour <= turnOnHour)
        {
            _lit = false;
            foreach (GameObject light in _pointLights)
            {
                light.SetActive(false);
            }           
        }
        if (_timeManager.CurrentGameTime.Hour >= turnOnHour)
        {
            _lit = true;
            foreach (GameObject light in _pointLights)
            {
                light.SetActive(true);
            }
        }
    }
    private void SwitchSpriteOnLit()
    {
        SpriteRenderer Sprite = GetComponentInChildren<SpriteRenderer>();
        if (_lit)
            Sprite.sprite = _sprites[1];
        else
            Sprite.sprite = _sprites[0];
    }
}
