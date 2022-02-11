using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteHolder : MonoBehaviour
{
    public Sprite[] sprites;

    public int seasonCount;

    private void Update()
    {   
            GetComponent<SpriteRenderer>().sprite = sprites[seasonCount];  
    }
    //Check Season 

    //Set Sprite to index.

}
