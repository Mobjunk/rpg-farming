using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    private Animator _animator;
    private SpriteRenderer _spriteRenderer;
    private bool _treeFadeOut;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    public void TurnOffTreeHitAnimation()
    {
        _animator.SetBool("treeHit", false);
    }

    public void RemoveTreeTop()
    {
        _treeFadeOut = true;
    }

    private void Update()
    {
        if (_treeFadeOut)
        {
            if (_spriteRenderer.color.a > 0.5)
            {
                _spriteRenderer.color = new Color(_spriteRenderer.color.r, _spriteRenderer.color.g,
                    _spriteRenderer.color.b, _spriteRenderer.color.a - 0.025f);
            }
            else Destroy(gameObject);
        }
    }
}
