using System;
using UnityEngine;

public class CharacterKeyboardManager : MonoBehaviour, ICharacterInput
{
    private ItemBarManager _itemBarManager => ItemBarManager.Instance();
    
    private CharacterManager _characterManager;
    
    public event CharacterInputActionAttack OnCharacterAttack = delegate {  };
    public event CharacterInputActionMove OnCharacterMovement = delegate {  };
    public event CharacterInteraction OnCharacterInteraction = delegate {  };
    public event CharacterSecondaryInteraction OnCharacterSecondaryInteraction = delegate {  };

    private void Awake()
    {
        _characterManager = GetComponent<CharacterManager>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (_itemBarManager.IsWearingCorrectTool(ToolType.SWORD)) OnCharacterAttack(_characterManager);
            else OnCharacterInteraction(_characterManager);
        }
        if(Input.GetMouseButtonDown(1)) OnCharacterSecondaryInteraction(_characterManager);
        //if(Input.GetMouseButtonDown(0)) OnCharacterAttack();
        //if (Input.GetKeyDown(KeyCode.F)) OnCharacterInteraction(characterManager);
        if (Input.GetKeyDown(KeyCode.E) && !DialogueManager.Instance().DialogueIsPlaying)
        {
            Player player = (Player) _characterManager;
            
            if (player.PlayerInventoryUIManager.IsOpened && player.CharacterUIManager.CurrentUIOpened.GetType() == typeof(PlayerInvenotryUIManager))
                player.PlayerInventoryUIManager.Close();
            else if (!player.PlayerInventoryUIManager.IsOpened)
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

        if (direction == null) Debug.LogError("3214234324??????????");
        if(OnCharacterMovement == null) Debug.LogError("DAFUQ??????????");
        
        OnCharacterMovement(direction);
    }
}
