using System;
using System.Collections;
using System.Linq;
using UnityEngine;

public class CharacterActionBubbles : MonoBehaviour
{
    [SerializeField] private GameObject _bubblesGameObject;

    private SpriteRenderer _spriteRenderer;
    private Animator _animator;

    [SerializeField] private BubbleActions _currentBubbleAction;

    private void Awake()
    {
        _spriteRenderer = _bubblesGameObject.GetComponent<SpriteRenderer>();
        _animator = _bubblesGameObject.GetComponent<Animator>();
    }

    public void SetBubbleAction(BubbleActions pBubbleAction)
    {
        _currentBubbleAction = pBubbleAction;
        _spriteRenderer.enabled = !pBubbleAction.Equals(BubbleActions.NONE);
        ResetAnimations();
        if (!pBubbleAction.Equals(BubbleActions.NONE))
            Utility.SetAnimator(_animator, pBubbleAction.ToString().ToLower(), true);
    }

    private void ResetAnimations()
    {
        foreach (string name in Enum.GetNames(typeof(BubbleActions)))
        {
            if (name.Equals("none")) continue;
            Utility.SetAnimator(_animator, name.ToLower(), false);
        }
    }
    
    public void Test()
    {
        SetBubbleAction(BubbleActions.WAITING);
    }

    public void Test2()
    {
        SetBubbleAction(BubbleActions.READY);
    }

    public void Test3()
    {
        SetBubbleAction(BubbleActions.NONE);
    }
}

public enum BubbleActions
{
    NONE,
    WAITING,
    READY
}