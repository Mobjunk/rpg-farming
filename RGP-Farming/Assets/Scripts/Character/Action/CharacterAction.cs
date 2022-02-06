using UnityEngine;

[System.Serializable]
public abstract class CharacterAction : ICharacterAction
{
    private CharacterManager pCharacterManager;
    private CharacterStateManager _characterStateManager;

    public CharacterManager PCharacterManager
    {
        get => pCharacterManager;
        set => pCharacterManager = value;
    }

    public abstract CharacterStates GetCharacterState();

    public CharacterAction(CharacterManager pCharacterManager)
    {
        PCharacterManager = pCharacterManager;
        _characterStateManager = pCharacterManager.GetComponent<CharacterStateManager>();
    }
    
    public virtual void Update()
    {
        if (!GetCharacterState().Equals(CharacterStates.NONE))
        {
            Debug.LogError("dafuq");
            _characterStateManager.SetCharacterState(GetCharacterState());
        }
    }

    public virtual void OnStart()
    {
        
    }

    public virtual void OnStop()
    {
        Debug.LogError("Stopped the character action...");
    }

    public virtual bool Interruptable()
    {
        return true;
    }
}