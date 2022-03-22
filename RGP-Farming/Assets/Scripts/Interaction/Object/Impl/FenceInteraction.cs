using System;
using UnityEngine;

public class FenceInteraction : InteractionManager
{
    [SerializeField] private BoxCollider2D _collider2D;
    
    private Animator _animator;

    private TestScript _testScript;
    
    /// <summary>
    /// If the gate is closed or not
    /// </summary>
    private bool _isClosed = true;
    /// <summary>
    /// The values of the box collider
    /// </summary>
    private Vector2 _realOffset;
    private Vector2 _realSize;
    
    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _testScript = GetComponent<TestScript>();
    }

    private void Start()
    {
        _realOffset = _collider2D.offset;
        _realSize = _collider2D.size;
    }

    public override void OnInteraction(CharacterManager pCharacterManager)
    {
        if (_animator.GetBool("closing") == false && _animator.GetBool("opening") == false)
        {
            Utility.SetAnimator(_animator, _isClosed ? "opening" : "closing", true, true);
            _isClosed = !_isClosed;
            if (!_isClosed)
            {
                _collider2D.size = new Vector2(0.3267612f, 0.5f);
                _collider2D.offset = new Vector2(-0.3366194f, -0.25f);
                _testScript.UpdateGrid(true);
            }
            else
            {
                _collider2D.size = _realSize;
                _collider2D.offset = _realOffset;
                _testScript.UpdateGrid();
            }
        }
    }
}
