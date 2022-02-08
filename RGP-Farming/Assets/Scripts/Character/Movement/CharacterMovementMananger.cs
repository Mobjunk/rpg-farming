using System;
using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(Rigidbody2D)), DisallowMultipleComponent()]
public class CharacterMovementMananger : MonoBehaviour, ICharacterMovement
{
    [FormerlySerializedAs("speed")] [SerializeField] private float _movementSpeed = 0.65f;
    private Rigidbody2D _rigidBody2D;
    private Animator _animator;
    private HeightBasedSorting _sorting;
    public Vector2 CurrentDirection;
    
    private void Awake()
    {
        _rigidBody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _sorting = GetComponent<HeightBasedSorting>();
    }

    public void Move(Vector2 pDirection)
    {
        pDirection.Normalize();

        CurrentDirection = pDirection;

        _rigidBody2D.MovePosition((Vector2)transform.position + (pDirection * _movementSpeed));

        if (!pDirection.Equals(Vector2.zero))
        {
            _animator.SetFloat("moveX", Mathf.Round(pDirection.x));
            _animator.SetFloat("moveY", Mathf.Round(pDirection.y));
        }

        _animator.SetBool("moving", !pDirection.Equals(Vector2.zero));
        
        _sorting.UpdateOrder();
    }
}
