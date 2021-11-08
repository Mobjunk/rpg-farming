using UnityEngine;

[System.Serializable]
public abstract class CharacterAction : ICharacterAction
{
    public CharacterManager CharacterManager;
    public CharacterStateManager CharacterStateManager;

    public abstract CharacterStates GetCharacterState();

    public CharacterAction(CharacterManager pCharacterManager)
    {
        CharacterManager = pCharacterManager;
        CharacterStateManager = CharacterManager.GetComponent<CharacterStateManager>();
    }
    
    public virtual void Update()
    {
        if (!GetCharacterState().Equals(CharacterStates.NONE))
        {
            Debug.LogError("dafuq");
            CharacterStateManager.SetCharacterState(GetCharacterState());
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