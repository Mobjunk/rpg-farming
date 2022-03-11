using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteHolder : MonoBehaviour
{
    public Sprite[] sprites;

    public int SpriteCount;

    private void Update()
    {   
            GetComponent<SpriteRenderer>().sprite = sprites[SpriteCount];  
    }
    //Check Season 

    //Set Sprite to index.

}
