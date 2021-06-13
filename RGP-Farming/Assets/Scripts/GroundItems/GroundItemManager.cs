using System;
using UnityEngine;

public class GroundItemManager : MonoBehaviour
{
    //[SerializeField] private Rigidbody2D rigidbody2D;
    [SerializeField] private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        //rigidbody2D = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Player characterManager = other.GetComponent<Player>();
        if (characterManager != null) GroundItemsManager.Instance().Remove(gameObject, true);
    }

    public void SetSprite(Sprite sprite)
    {
        spriteRenderer.sprite = sprite;
    }
}
