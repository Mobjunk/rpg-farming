using UnityEngine;
using UnityEngine.UI;

public class CoinAnimation : MonoBehaviour
{
    [SerializeField] private int spriteIndex;
    [SerializeField] private Sprite[] coinSprites;
    [SerializeField] private Image image;
    private float spriteDelay;
    
    private void Awake()
    {
        image = GetComponent<Image>();
    }

    void Update()
    {
        if (spriteDelay > 0)
        {
            spriteDelay -= Time.deltaTime;
            return;
        }
        if (spriteIndex >= coinSprites.Length) spriteIndex = 0;
        image.sprite = coinSprites[spriteIndex++];
        spriteDelay = 0.1f;
    }
}
