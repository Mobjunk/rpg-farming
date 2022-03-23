using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class soundtest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        FMOD.Studio.EventInstance instance = FMODUnity.RuntimeManager.CreateInstance("event:/FX/Character/Walking/Walk");
        instance.start();
        instance.setParameterByName("Surface", 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
