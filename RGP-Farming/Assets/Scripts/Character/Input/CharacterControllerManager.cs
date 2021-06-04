using UnityEngine;

public class CharacterControllerManager : MonoBehaviour, ICharacterInput
{
    public event CharacterInputAction OnCharacterAttack = delegate {  };
    public event CharacterInputActionMove OnCharacterMovement = delegate {  };
    public event CharacterInputAction OnCharacterInteraction = delegate {  };

    private void Update()
    {
        if(Input.GetButtonDown("Fire1")) OnCharacterAttack();
        if (Input.GetButtonDown("Fire2")) OnCharacterInteraction();
        
        Vector2 direction = Vector2.zero;
         
        OnCharacterMovement(direction);
    }
}
