using System;
using UnityEngine;
using UnityEngine.Serialization;

public class CharacterStateManager : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] CharacterStates characterState = CharacterStates.IDLE;

    public void SetCharacterState(CharacterStates state)
    {
        characterState = state;
    }

    public CharacterStates GetCharacterState()
    {
        return characterState;
    }

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        //if(animator != null) animator.SetInteger("AnimationState", (int)characterState);
    }
}