using UnityEngine;

[System.Serializable]
public abstract class CharacterAction : ICharacterAction
{
    private CharacterManager characterManager;
    private CharacterStateManager _characterStateManager;

    public CharacterManager CharacterManager
    {
        get => characterManager;
        set => characterManager = value;
    }

    public abstract CharacterStates GetCharacterState();

    public CharacterAction(CharacterManager pCharacterManager)
    {
        CharacterManager = pCharacterManager;
        _characterStateManager = pCharacterManager.GetComponent<CharacterStateManager>();
    }
    
    public virtual void Update()
    {
        if (!GetCharacterState().Equals(CharacterStates.NONE))
            _characterStateManager.SetCharacterState(GetCharacterState());
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