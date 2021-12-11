using UnityEngine;

[System.Serializable]
public abstract class CharacterAction : ICharacterAction
{
    private CharacterManager characterManager;
    private CharacterStateManager characterStateManager;

    public CharacterManager CharacterManager
    {
        get => characterManager;
        set => characterManager = value;
    }

    public abstract CharacterStates GetCharacterState();

    public CharacterAction(CharacterManager characterManager)
    {
        CharacterManager = characterManager;
        characterStateManager = characterManager.GetComponent<CharacterStateManager>();
    }
    
    public virtual void Update()
    {
        if (!GetCharacterState().Equals(CharacterStates.NONE))
        {
            Debug.LogError("dafuq");
            characterStateManager.SetCharacterState(GetCharacterState());
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