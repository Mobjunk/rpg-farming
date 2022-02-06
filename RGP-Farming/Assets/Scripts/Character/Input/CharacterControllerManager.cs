using UnityEngine;

public class CharacterControllerManager : MonoBehaviour, ICharacterInput
{
    private CharacterManager _characterManager;
    
    public event CharacterInputAction OnCharacterAttack = delegate {  };
    public event CharacterInputActionMove OnCharacterMovement = delegate {  };
    public event CharacterInteraction OnCharacterInteraction = delegate {  };
    public event CharacterSecondaryInteraction OnCharacterSecondaryInteraction = delegate {  };

    private void Awake()
    {
        _characterManager = GetComponent<CharacterManager>();
    }

    private void Update()
    {
        if(Input.GetButtonDown("Fire1")) OnCharacterAttack();
        if (Input.GetButtonDown("Fire2")) OnCharacterInteraction(_characterManager);
        if (Input.GetButtonDown("Fire3")) OnCharacterSecondaryInteraction(_characterManager);
        
        Vector2 direction = Vector2.zero;
         
        OnCharacterMovement(direction);
    }
}
