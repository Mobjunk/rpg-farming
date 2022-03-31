using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using static Utility;

public class DayAndNight : MonoBehaviour
{
    private Player _player => Player.Instance();
    private TimeManager _timeManager => TimeManager.Instance();
    private TransitionManager _transitionManager => TransitionManager.Instance();

    private bool _bed;

    private void Update()
    {
        EndDay();
    }
    public void EndDay()
    {
        if(_timeManager.CurrentGameTime.Hour == 23)
        {
            _bed = true;          
        }
        if (_bed)
        {
            StartCoroutine(PlayerToBed());
            _transitionManager.CallTransition(1f);
            _bed = false;
        }
    }
    
    IEnumerator PlayerToBed()
    {
        //Lock player movement
        if (_player.InputEnabled) _player.ToggleInput();

        //Transition to Dark
        yield return new WaitForSeconds(1f);

        //Add the bed scene in the background
        AddSceneIfNotLoaded("Playerhouse");

        //Turn off main scene
        ToggleRootObjectsInScene("Main Level");

        //Teleport player to bed.
        _player.transform.position = new Vector2(8, 2.4f);

        Debug.Log("Player put to bed");

        //Transition to Light
        yield return new WaitForSeconds(1f);

        //Set current time to 8 am
        //TODO  --->  DIT WERKT NIET : _timeManager.SetMorningDate();

        //Unlock player movement
        if (!_player.InputEnabled) _player.ToggleInput();
    }
}
