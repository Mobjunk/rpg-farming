using TMPro;
using UnityEngine;

[RequireComponent(typeof(CharacterStateManager))]
public class CharacterManager : MonoBehaviour
{
    [SerializeField] protected ICharacterInput characterInputManager;
    
    [SerializeField] private CharacterStateManager characterStateManager;
    
    private CharacterAction characterAction;

    public CharacterAction CharacterAction
    {
        get => characterAction;
        set => characterAction = value;
    }
    
    public virtual void Awake() { }
    
    public virtual void Start()
    {
        characterStateManager = GetComponent<CharacterStateManager>();
    }

    public virtual void Update()
    {
        characterAction?.Update();
    }

    public void SetAction(CharacterAction action)
    {
        if (characterAction != null)
        {
            if (!characterAction.Interruptable()) return;
            characterStateManager.SetCharacterState(CharacterStates.IDLE);
            characterAction.OnStop();
        }

        characterAction = action;
        characterAction?.OnStart();
    }
}
