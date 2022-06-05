using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestParameter : MonoBehaviour
{
    // Start is called before the first frame update
    private Player _player => Player.Instance();
    [SerializeField]FMODUnity.StudioParameterTrigger _studioTrigger;
    [SerializeField]FMODUnity.StudioEventEmitter _eventEmitter;
    void Start()
    {
        _studioTrigger = GetComponent<FMODUnity.StudioParameterTrigger>();
        _eventEmitter = _player.GetComponent<FMODUnity.StudioEventEmitter>();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(_studioTrigger.Emitters.Length);
        if(_studioTrigger.Emitters.Length == 0)
        {
            _studioTrigger.Emitters = new FMODUnity.EmitterRef[] {
               // _eventEmitter.
            };
        }
  //      foreach (FMODUnity.EmitterRef e in _studioTrigger.Emitters) {
  //          if (e.Target == null || e.Target != _eventEmitter) {
  //              e.Target = _eventEmitter;
  //          }
  //      }
    }
}
