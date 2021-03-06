using TMPro;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(BoxCollider2D))]
[RequireComponent(typeof(HeightBasedSorting))]
[RequireComponent(typeof(CharacterHealthManager))]
public class CharacterManager : MonoBehaviour
{
    private DialogueManager _dialogueManager => DialogueManager.Instance();
    
    /// <summary>
    /// Input manager of the character
    /// </summary>
    protected ICharacterInput _characterInputManager;
    
    /// <summary>
    /// Statemachine of the character
    /// </summary>
    private CharacterStateManager _characterStateManager;

    public CharacterStateManager CharacterStateManager
    {
        get => _characterStateManager;
        set => _characterStateManager = value;
    }
    
    /// <summary>
    /// Movement of the character
    /// </summary>
    private CharacterMovementMananger _characterMovementMananger;

    public CharacterMovementMananger CharacterMovementMananger
    {
        get => _characterMovementMananger;
        set => _characterMovementMananger = value;
    }
    
    /// <summary>
    /// Action a character can perform
    /// </summary>
    private CharacterAction _characterAction;
    
    public CharacterAction CharacterAction
    {
        get => _characterAction;
        set => _characterAction = value;
    }

    private CharacterActionBubbles _characterActionBubbles;

    public CharacterActionBubbles CharacterActionBubbles => _characterActionBubbles;

    [SerializeField] private bool _stopAutoAdjustingBoxCollider;

    public virtual void Awake()
    {
        CharacterMovementMananger = GetComponent<CharacterMovementMananger>();
        _characterActionBubbles = GetComponent<CharacterActionBubbles>();
        
        //Makes sure the z rotation is turned off
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;

        //Handles setting the right offset and size for the boxcollider
        BoxCollider2D boxCollider = GetComponent<BoxCollider2D>();

        if (!_stopAutoAdjustingBoxCollider)
        {
            boxCollider.offset = new Vector2(0, 0);
            boxCollider.size = new Vector2(0.4959272f, 0.1186142f);
        }
    }
    
    public virtual void Start()
    {
        _characterStateManager = GetComponent<CharacterStateManager>();
        if (_characterStateManager == null) _characterStateManager = GetComponentInChildren<CharacterStateManager>();
    }

    public virtual void Update()
    {
        _characterAction?.Update();
    }

    /// <summary>
    /// Handles setting a character action
    /// </summary>
    /// <param name="pAction">A action a character can perform</param>
    public void SetAction(CharacterAction pAction)
    {
        if (_dialogueManager.DialogueIsPlaying && pAction != null) return;
        
        if (_characterAction != null)
        {
            if (!_characterAction.Interruptable()) return;
            _characterAction.OnStop();
            _characterStateManager.SetCharacterState(CharacterStates.IDLE);
        }

        _characterAction = pAction;
        _characterAction?.OnStart();
    }
}
