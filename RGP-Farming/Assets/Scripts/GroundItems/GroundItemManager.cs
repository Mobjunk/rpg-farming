using System;
using UnityEngine;

public class GroundItemManager : MonoBehaviour
{
    //[SerializeField] private Rigidbody2D rigidbody2D;
    [SerializeField] private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        //rigidbody2D = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D pOther)
    {
        Player characterManager = pOther.GetComponent<Player>();
        if (characterManager != null) GroundItemsManager.Instance().Remove(gameObject, true);
    }

    public void SetSprite(Sprite pSprite)
    {
        _spriteRenderer.sprite = pSprite;
    }
}
