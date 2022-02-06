using TMPro;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(BoxCollider2D), typeof(Animator))]
[RequireComponent(typeof(HeightBasedSorting),typeof(CharacterStateManager), typeof(CharacterDesignManager))]
[RequireComponent(typeof(CharacterHealthManager))]
public class CharacterManager : MonoBehaviour
{
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

    public virtual void Awake()
    {
        CharacterMovementMananger = GetComponent<CharacterMovementMananger>();
        
        //Makes sure the z rotation is turned off
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;

        //Handles setting the right offset and size for the boxcollider
        BoxCollider2D boxCollider = GetComponent<BoxCollider2D>();
        boxCollider.offset = new Vector2(-0.005381916f, -0.2006676f);
        boxCollider.size = new Vector2(0.1124128f, 0.07866466f);
    }
    
    public virtual void Start()
    {
        _characterStateManager = GetComponent<CharacterStateManager>();
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
        if (_characterAction != null)
        {
            if (!_characterAction.Interruptable()) return;
            _characterStateManager.SetCharacterState(CharacterStates.IDLE);
            _characterAction.OnStop();
        }

        _characterAction = pAction;
        _characterAction?.OnStart();
    }
}
