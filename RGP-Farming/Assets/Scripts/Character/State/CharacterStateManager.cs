using System;
using UnityEngine;
using UnityEngine.Serialization;

public class CharacterStateManager : MonoBehaviour
{
    Animator animator;
    [SerializeField] CharacterStates characterState = CharacterStates.IDLE;

    public event CharacterInputAction OnStateChanged = delegate {  };
    
    public void SetCharacterState(CharacterStates state)
    {
        characterState = state;
        OnStateChanged();
    }

    public CharacterStates GetCharacterState()
    {
        return characterState;
    }

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    /// <summary>
    /// The current direction the character is facing based on the moveX and moveY in the animator
    /// </summary>
    /// <returns></returns>
    public int GetDirection()
    {
        int moveX = (int) (Mathf.Round(animator.GetFloat("moveX")));
        int moveY = (int) (Mathf.Round(animator.GetFloat("moveY")));
        
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