using System;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(Rigidbody2D)), DisallowMultipleComponent()]
public class CharacterMovementMananger : MonoBehaviour, ICharacterMovement
{
    private SoundManager _soundManager => SoundManager.Instance();
    private TilemapManager _tilemapManager => TilemapManager.Instance();
    
    [SerializeField] private CharacterManager _characterManager;
    [SerializeField] private CharacterStateManager _characterStateManager;
    [SerializeField] private float _movementSpeed = 0.65f;
    public float MovementSpeed => _movementSpeed;
    
    private Rigidbody2D _rigidBody2D;
    [SerializeField] private Animator _animator;
    private HeightBasedSorting _sorting;
    [HideInInspector] public Vector2 CurrentDirection;
    
    private void Awake()
    {
        _characterManager = GetComponent<CharacterManager>();
        _characterStateManager = GetComponent<CharacterStateManager>();
        _rigidBody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        if (_animator == null) _animator = GetComponentInChildren<Animator>();
        _sorting = GetComponent<HeightBasedSorting>();
    }

    public void Move(Vector2 pDirection)
    {
        if (_characterManager.CharacterAction != null && !_characterManager.CharacterAction.Interruptable()) return;
        
        pDirection.Normalize();

        CurrentDirection = pDirection;

        _rigidBody2D.MovePosition((Vector2) transform.position + (pDirection * _movementSpeed));
        

        if (!pDirection.Equals(Vector2.zero))
        {
            if (_characterStateManager.GetCharacterState().Equals(CharacterStates.WALKING_3) || _characterStateManager.GetCharacterState().Equals(CharacterStates.WALKING_7) || _characterStateManager.GetCharacterState().Equals(CharacterStates.CARRY_3) || _characterStateManager.GetCharacterState().Equals(CharacterStates.CARRY_7))
                _soundManager.ExecuteSound("footsteps", _tilemapManager.GetTileType(transform.position), gameObject);
            
            _animator.SetFloat("moveX", Mathf.Round(pDirection.x));
            _animator.SetFloat("moveY", Mathf.Round(pDirection.y));
            //Make sure it resets the player action when moving
            //But only do this for players, not for npcs
            if(_characterManager is Player)
                if (_characterStateManager.GetCharacterState() != CharacterStates.IDLE)
                {
                    ResetSkillingAnimations();
                    _characterManager.CharacterActionBubbles.SetBubbleAction(BubbleActions.NONE);
                    _characterManager.SetAction(null);
                }
        }

        _animator.SetBool("moving", !pDirection.Equals(Vector2.zero));
        
        _sorting.UpdateOrder();
    }

    public void ResetMovement()
    {
        _animator.SetBool("moving", false);
    }

    public void ResetSkillingAnimations()
    {
        _animator.SetBool("axe_swing", false);
        _animator.SetBool("pickaxe_swing", false);
        _animator.SetBool("watering", false);
        _animator.SetBool("hoe", false);
        _animator.SetBool("fishing", false);
        _animator.SetBool("sword_swing", false);
        _animator.SetBool("fishing_idle", false);
    }
}
