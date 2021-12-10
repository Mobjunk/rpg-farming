using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D)), DisallowMultipleComponent()]
public class CharacterMovementMananger : MonoBehaviour
{
    [SerializeField] private float _movementSpeed = 0.65f;
    private Rigidbody2D _rigidbody;
    private Animator _characterAnimator;
    private HeightBasedSorting _characterSorting;
    private CharacterControllerManager _characterControllerManager;
    
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _characterAnimator = GetComponent<Animator>();
        _characterSorting = GetComponent<HeightBasedSorting>();
        _characterControllerManager = GetComponent<CharacterControllerManager>();
    }

    private void FixedUpdate()
    {
        
        _characterControllerManager.CurrentDirection.Normalize();

        _rigidbody.MovePosition((Vector2)transform.position + (_characterControllerManager.CurrentDirection * _movementSpeed));

        if (!_characterControllerManager.CurrentDirection.Equals(Vector2.zero))
        {
            _characterAnimator.SetFloat("moveX", Mathf.Round(_characterControllerManager.CurrentDirection.x));
            _characterAnimator.SetFloat("moveY", Mathf.Round(_characterControllerManager.CurrentDirection.y));
        }

        _characterAnimator.SetBool("moving", !_characterControllerManager.CurrentDirection.Equals(Vector2.zero));
        
        _characterSorting.UpdateOrder();
    }
    
    public void Move(Vector2 pDirection) { }
}
