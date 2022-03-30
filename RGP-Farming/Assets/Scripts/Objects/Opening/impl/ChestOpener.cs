using FMOD;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class ChestOpener : Opener
{
    private SoundManager _soundManager => SoundManager.Instance();
    
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
            _animator.SetBool("open", true);
        }
    }

    public override void Open(CharacterManager pCharacterManager)
    {
        this._characterManager = pCharacterManager;
        _isOpened = false;
        _animationRunning = true;
        _animator.SetBool("opening", true);
        _openingTimer = 0.4f;
        _soundManager.ExecuteSound("ChestOpenSound");
    }

    public override void Close(CharacterManager pCharacterManager)
    {
        base.Close(pCharacterManager);
        _animator.SetBool("open", false);
        Utility.SetAnimator(_animator, "closing", true, true);
        _soundManager.ExecuteSound("ChestClosedSound");
    }
}
