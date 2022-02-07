using UnityEngine;
using UnityEngine.UI;

public class CoinAnimation : MonoBehaviour
{
    [SerializeField] private int _spriteIndex;
    [SerializeField] private Sprite[] _coinSprites;
    [SerializeField] private Image _image;
    private float _spriteDelay;
    
    private void Awake()
    {
        _image = GetComponent<Image>();
    }

    void Update()
    {
        if (_spriteDelay > 0)
        {
            _spriteDelay -= Time.deltaTime;
            return;
        }
        if (_spriteIndex >= _coinSprites.Length) _spriteIndex = 0;
        _image.sprite = _coinSprites[_spriteIndex++];
        _spriteDelay = 0.1f;
    }
}
