using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D)), DisallowMultipleComponent()]
public class CharacterMovementMananger : MonoBehaviour, ICharacterMovement
{
    [SerializeField] private float speed = 0.65f;
    private Rigidbody2D rigidBody2D;
    private Animator animator;
    
    private void Awake()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    public void Move(Vector2 direction)
    {
        direction.Normalize();

        rigidBody2D.MovePosition((Vector2)transform.position + (direction * speed));

        if (!direction.Equals(Vector2.zero))
        {
            animator.SetFloat("moveX", Mathf.Round(direction.x));
            animator.SetFloat("moveY", Mathf.Round(direction.y));
        }

        animator.SetBool("moving", !direction.Equals(Vector2.zero));
    }
}
