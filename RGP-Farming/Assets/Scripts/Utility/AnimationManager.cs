using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    private GroundItemsManager _groundItemsManager => GroundItemsManager.Instance();
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void TurnOffTreeHitAnimation()
    {
        _animator.SetBool("treeHit", false);
    }

    public void RemoveTreeTop()
    {
        //_treeFadeOut = true;
        _groundItemsManager.Add(new GameItem(ItemManager.Instance().ForName("wood"), 15), gameObject.transform.GetChild(0).position);
        _groundItemsManager.Add(new GameItem(ItemManager.Instance().ForName("leaf"), 10), gameObject.transform.GetChild(0).position);
        Destroy(gameObject);
    }

    public void RemoveHitAnimation()
    {
        _animator.SetBool("hit", false);
    }

    public void ResetCropAnimation()
    {
        _animator.SetBool("shake", false);
    }
}
