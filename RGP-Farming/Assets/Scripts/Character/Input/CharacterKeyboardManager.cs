using System;
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
    }

    private void FixedUpdate()
    {
        
        float horizontalMovement = Input.GetAxisRaw("Horizontal");
        float verticalMovement = Input.GetAxisRaw("Vertical");
        
        Vector2 direction = new Vector2(horizontalMovement, verticalMovement);
         
        OnCharacterMovement(direction);
    }
}
