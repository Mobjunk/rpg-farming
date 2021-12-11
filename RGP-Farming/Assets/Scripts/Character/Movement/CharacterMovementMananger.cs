using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D)), DisallowMultipleComponent()]
public class CharacterMovementMananger : MonoBehaviour
{
    [SerializeField] private float speed = 0.65f;
    private Rigidbody2D rigidBody2D;
    private Animator animator;
    private HeightBasedSorting sorting;
    private CharacterControllerManager _characterControllerManager;
    
    private void Awake()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sorting = GetComponent<HeightBasedSorting>();
        _characterControllerManager = GetComponent<CharacterControllerManager>();
    }

    private void FixedUpdate()
    {
        
        _characterControllerManager.CurrentDirection.Normalize();

        rigidBody2D.MovePosition((Vector2)transform.position + (_characterControllerManager.CurrentDirection * speed));

        if (!_characterControllerManager.CurrentDirection.Equals(Vector2.zero))
        {
            animator.SetFloat("moveX", Mathf.Round(_characterControllerManager.CurrentDirection.x));
            animator.SetFloat("moveY", Mathf.Round(_characterControllerManager.CurrentDirection.y));
        }

        animator.SetBool("moving", !_characterControllerManager.CurrentDirection.Equals(Vector2.zero));
        
        sorting.UpdateOrder();
    }
    
    public void Move(Vector2 direction) { }
}
