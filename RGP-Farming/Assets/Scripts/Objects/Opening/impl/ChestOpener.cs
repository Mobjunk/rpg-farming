using UnityEngine;

public class ChestOpener : Opener
{
    private Animator _animator;
    private float _openingTimer;
    private bool _animationRunning, _isOpened;
    
    //TODO: Find a better way to do this
    private CharacterManager _characterManager;

    public override void Awake()
    {
        base.Awake();
        _animator = GetComponent<Animator>();
    }

    public void Update()
    {
        if (_openingTimer > 0)
        {
            _openingTimer -= Time.deltaTime;
        }
        else if(!_isOpened && _animationRunning)
        {
            base.Open(_characterManager);
            _characterManager = null;
            _isOpened = true;
            _animationRunning = false;
            _animator.SetBool("opening", false);
        }
    }

    public override void Open(CharacterManager pCharacterManager)
    {
        this._characterManager = pCharacterManager;
        _isOpened = false;
        _animationRunning = true;
        _animator.SetBool("opening", true);
        _openingTimer = 0.4f;
    }

    public override void Close(CharacterManager pCharacterManager)
    {
        base.Close(pCharacterManager);
        _animator.SetBool("opening", false);
    }
}
