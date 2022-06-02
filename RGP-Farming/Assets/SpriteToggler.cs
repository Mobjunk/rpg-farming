using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteToggler : MonoBehaviour
{

    void Awake()
    {
        GetComponent<SpriteRenderer>().enabled = false;
    }

}
