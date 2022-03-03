using System;
using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.Serialization;

public class CharacterStateManager : MonoBehaviour
{
    private Animator _animator;

    public Animator GetAnimator() => _animator;
    
    [SerializeField] private CharacterStates _characterState = CharacterStates.IDLE;

    public event CharacterInputAction OnStateChanged = delegate {  };
    
    public void SetCharacterState(CharacterStates pState)
    {
        _characterState = pState;
        OnStateChanged();
    }

    public CharacterStates GetCharacterState()
    {
        return _characterState;
    }

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    /*public void SetAnimator(string pName, bool pSet, bool pStopAnimation = false)
    {
        _animator.SetBool(pName, pSet);
        if(pStopAnimation) StartCoroutine(ResetAnimation(pName, GetAnimationClipTime(pName)));
    }

    public void SetAnimator(string pName, float pSet, bool pStopAnimation = false)
    {
        _animator.SetFloat(pName, pSet);
        if(pStopAnimation) StartCoroutine(ResetAnimation(pName, GetAnimationClipTime(pName)));
    }

    public void SetAnimator(string pName, int pSet, bool pStopAnimation = false)
    {
        _animator.SetInteger(pName, pSet);
        if(pStopAnimation) StartCoroutine(ResetAnimation(pName, GetAnimationClipTime(pName)));
    }

    IEnumerator ResetAnimation(string pAnimationName, float pAnimationTime)
    {
        yield return new WaitForSeconds(pAnimationTime);
        _animator.SetBool(pAnimationName, false);
    }
    
    private float GetAnimationClipTime(string pAnimationName)
    {
        AnimationClip[] clips = _animator.runtimeAnimatorController.animationClips;
        return (from clip in clips where clip.name.ToLower().Equals(pAnimationName.ToLower()) select clip.length).FirstOrDefault();
    }*/

    /// <summary>
    /// The current direction the character is facing based on the moveX and moveY in the animator
    /// </summary>
    /// <returns></returns>
    public int GetDirection()
    {
        int moveX = (int) (Mathf.Round(_animator == null ? 0 : _animator.GetFloat("moveX")));
        int moveY = (int) (Mathf.Round(_animator == null ? 0 : _animator.GetFloat("moveY")));
        
        if ((moveX == -1 && moveY == 0) || (moveX == -1 && moveY == 1) || (moveX == -1 && moveY == -1)) //Left
        {
            return 1;
        }

        if ((moveX == 1 && moveY == 0) || (moveX == 1 && moveY == 1) || (moveX == 1 && moveY == -1)) //Right
        {
            return 2;
        }

        if (moveX == 0 && moveY == -1) //Down
        {
            return 0;
        }

        if (moveX == 0 && moveY == 1) //Up
        {
            return 3;
        }
        
        if(moveX != 0 && moveY != 0)
            Debug.LogError($"ERROR: moveX: {moveX}, moveY: {moveY}");

        return 0;
    }
}