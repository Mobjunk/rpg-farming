using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeasonSprites : MonoBehaviour
{
    public Sprite[] sprites;

    private SeasonManager _seasonManager => SeasonManager.Instance();

    private void Update()
    {   
            GetComponent<SpriteRenderer>().sprite = sprites[_seasonManager.SeasonalCount];  
    }
}
