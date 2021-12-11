using System;
using UnityEngine;

public class CharacterKeyboardManager : MonoBehaviour, ICharacterInput
{
    private CharacterManager characterManager;
    
    public event CharacterInputAction OnCharacterAttack = delegate {  };
    public event CharacterInputActionMove OnCharacterMovement = delegate {  };
    public event CharacterInteraction OnCharacterInteraction = delegate {  };
    public event CharacterSecondaryInteraction OnCharacterSecondaryInteraction = delegate {  };

    private void Awake()
    {
        characterManager = GetComponent<CharacterManager>();
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0)) OnCharacterInteraction(characterManager);
        if(Input.GetMouseButtonDown(1)) OnCharacterSecondaryInteraction(characterManager);
        //if(Input.GetMouseButtonDown(0)) OnCharacterAttack();
        //if (Input.GetKeyDown(KeyCode.F)) OnCharacterInteraction(characterManager);
        if (Input.GetKeyDown(KeyCode.E))
        {
            Player player = (Player) characterManager;
            
            if (player.PlayerInventoryUIManager.isOpened && player.CharacterUIManager.CurrentUIOpened.GetType() == typeof(PlayerInvenotryUIManager))
                player.PlayerInventoryUIManager.Close();
            else if (!player.PlayerInventoryUIManager.isOpened)
            {
                if (player.CharacterUIManager.CurrentUIOpened != null && !player.CharacterUIManager.CurrentUIOpened.AllowedToOpenInvnetory) return;
                player.PlayerInventoryUIManager.Open();
            }
        }
    }

    private void FixedUpdate()
    {
        
        float horizontalMovement = Input.GetAxisRaw("Horizontal");
        float verticalMovement = Input.GetAxisRaw("Vertical");
        
        Vector2 direction = new Vector2(horizontalMovement, verticalMovement);
         
        OnCharacterMovement(direction);
    }
}
