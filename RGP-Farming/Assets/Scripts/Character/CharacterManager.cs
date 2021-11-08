using TMPro;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(BoxCollider2D), typeof(Animator))]
[RequireComponent(typeof(HeightBasedSorting),typeof(CharacterStateManager), typeof(CharacterDesignManager))]
[RequireComponent(typeof(CharacterHealthManager))]
public class CharacterManager : MonoBehaviour
{
    /// <summary>
    /// Statemachine of the character
    /// </summary>
    public CharacterStateManager CharacterStateManager;

    /// <summary>
    /// Movement of the character
    /// </summary>
    public CharacterMovementMananger CharacterMovementMananger;

    /// <summary>
    /// Action a character can perform
    /// </summary>
    private CharacterAction characterAction;
    
    public CharacterAction CharacterAction
    {
        get => characterAction;
        set => characterAction = value;
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
        CharacterStateManager = GetComponent<CharacterStateManager>();
    }

    public virtual void Update()
    {
        characterAction?.Update();
    }

    /// <summary>
    /// Handles setting a character action
    /// </summary>
    /// <param name="action">A action a character can perform</param>
    public void SetAction(CharacterAction pAction)
    {
        if (characterAction != null)
        {
            if (!characterAction.Interruptable()) return;
            CharacterStateManager.SetCharacterState(CharacterStates.IDLE);
            characterAction.OnStop();
        }

        characterAction = pAction;
        characterAction?.OnStart();
    }
}
