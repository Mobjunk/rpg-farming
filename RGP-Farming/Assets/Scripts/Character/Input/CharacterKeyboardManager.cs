using System;
using UnityEngine;

public class CharacterKeyboardManager : MonoBehaviour, ICharacterInput
{
    private CharacterManager characterManager;
    
    public event CharacterInputAction OnCharacterAttack = delegate {  };
    public event CharacterInputActionMove OnCharacterMovement = delegate {  };
    public event CharacterInteraction OnCharacterInteraction = delegate {  };

    private void Awake()
    {
        characterManager = GetComponent<CharacterManager>();
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0)) OnCharacterAttack();
        if (Input.GetKeyDown(KeyCode.F)) OnCharacterInteraction(characterManager);
    }

    private void FixedUpdate()
    {
        
        float horizontalMovement = Input.GetAxisRaw("Horizontal");
        float verticalMovement = Input.GetAxisRaw("Vertical");
        
        Vector2 direction = new Vector2(horizontalMovement, verticalMovement);
         
        OnCharacterMovement(direction);
    }
}
