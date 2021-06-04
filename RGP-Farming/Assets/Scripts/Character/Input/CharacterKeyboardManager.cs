using UnityEngine;

public class CharacterKeyboardManager : MonoBehaviour, ICharacterInput
{
    public event CharacterInputAction OnCharacterAttack = delegate {  };
    public event CharacterInputActionMove OnCharacterMovement = delegate {  };
    public event CharacterInputAction OnCharacterInteraction = delegate {  };

    private void Update()
    {
        if(Input.GetMouseButtonDown(0)) OnCharacterAttack();
        if (Input.GetKeyDown(KeyCode.F)) OnCharacterInteraction();
        
        Vector2 direction = Vector2.zero;
         
        OnCharacterMovement(direction);
    }
}
