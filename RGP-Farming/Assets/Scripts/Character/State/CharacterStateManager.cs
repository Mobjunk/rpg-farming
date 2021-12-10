using System;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.Serialization;

public class CharacterStateManager : MonoBehaviour
{
    private Animator _characterAnimator;
    [SerializeField] CharacterStates _characterState = CharacterStates.IDLE;

    public event CharacterInputAction OnStateChanged = delegate {  };
    
    public void SetCharacterState(CharacterStates state)
    {
        _characterState = state;
        OnStateChanged();
    }

    public CharacterStates GetCharacterState()
    {
        return _characterState;
    }

    private void Awake()
    {
        _characterAnimator = GetComponent<Animator>();
    }

    public void SetAnimator(string pName, bool pSet)
    {
        _characterAnimator.SetBool(pName, pSet);
    }

    public void SetAnimator(string pName, float pSet)
    {
        _characterAnimator.SetFloat(pName, pSet);
    }

    public void SetAnimator(string pName, int pSet)
    {
        _characterAnimator.SetInteger(pName, pSet);
    }

    /// <summary>
    /// The current direction the character is facing based on the moveX and moveY in the animator
    /// </summary>
    /// <returns></returns>
    public int GetDirection()
    {
        int moveX = (int) (Mathf.Round(_characterAnimator.GetFloat("moveX")));
        int moveY = (int) (Mathf.Round(_characterAnimator.GetFloat("moveY")));
        
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