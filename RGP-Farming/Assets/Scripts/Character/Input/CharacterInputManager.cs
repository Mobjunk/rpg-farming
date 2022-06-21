using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterInputManager : Singleton<CharacterInputManager>, ICharacterInput
{
    private ItemBarManager _itemBarManager => ItemBarManager.Instance();
    
    private CharacterManager _characterManager;
    
    public event CharacterInputActionAttack OnCharacterAttack = delegate {  };
    public event CharacterInputActionMove OnCharacterMovement = delegate {  };
    public event CharacterInteraction OnCharacterInteraction = delegate {  };
    public event CharacterSecondaryInteraction OnCharacterSecondaryInteraction = delegate {  };

    private PlayerControls _playerControls;
    
    [HideInInspector] public InputAction MoveAction;

    [HideInInspector] public InputAction InteractAction;

    [HideInInspector] public InputAction UseAction;

    [HideInInspector] public InputAction EscapeAction;

    [HideInInspector] public InputAction SpaceAction;

    [HideInInspector] public InputAction OpenAction;
    
    /// <summary>
    /// Item bar actions
    /// </summary>
    [HideInInspector] public InputAction SlotOne;

    [HideInInspector] public InputAction SlotTwo;

    [HideInInspector] public InputAction SlotThree;

    [HideInInspector] public InputAction SlotFour;

    [HideInInspector] public InputAction SlotFive;

    [HideInInspector] public InputAction SlotSix;

    [HideInInspector] public InputAction SlotSeven;

    [HideInInspector] public InputAction SlotEight;

    [HideInInspector] public InputAction SlotNine;

    [HideInInspector] public InputAction SlotTen;

    [HideInInspector] public InputAction SlotEleven;

    [HideInInspector] public InputAction SlotTwelve;
    
    [HideInInspector] public InputAction NextSlot;

    [HideInInspector] public InputAction PrevSlot;

    [HideInInspector] public bool GamepadActive;

    private void Awake()
    {
        _characterManager = GetComponent<CharacterManager>();
        _playerControls = new PlayerControls();
    }

    private void OnEnable()
    {
        MoveAction = _playerControls.Player.Move;
        MoveAction.Enable();

        InteractAction = _playerControls.Player.Interact;
        InteractAction.Enable();

        UseAction = _playerControls.Player.Use;
        UseAction.Enable();

        EscapeAction = _playerControls.Player.Escaspe;
        EscapeAction.Enable();

        SpaceAction = _playerControls.Player.Space;
        SpaceAction.Enable();

        OpenAction = _playerControls.Player.OpenInventory;
        OpenAction.Enable();

        SlotOne = _playerControls.Player.SlotOne;
        SlotOne.Enable();

        SlotTwo = _playerControls.Player.SlotTwo;
        SlotTwo.Enable();

        SlotThree = _playerControls.Player.SlotThree;
        SlotThree.Enable();

        SlotFour = _playerControls.Player.SlotFour;
        SlotFour.Enable();

        SlotFive = _playerControls.Player.SlotFive;
        SlotFive.Enable();

        SlotSix = _playerControls.Player.SlotSix;
        SlotSix.Enable();

        SlotSeven = _playerControls.Player.SlotSeven;
        SlotSeven.Enable();

        SlotEight = _playerControls.Player.SlotEight;
        SlotEight.Enable();

        SlotNine = _playerControls.Player.SlotNine;
        SlotNine.Enable();

        SlotTen = _playerControls.Player.SlotTen;
        SlotTen.Enable();

        SlotEleven = _playerControls.Player.SlotEleven;
        SlotEleven.Enable();

        SlotTwelve = _playerControls.Player.SlotTwelve;
        SlotTwelve.Enable();

        NextSlot = _playerControls.Player.NextSlot;
        NextSlot.Enable();

        PrevSlot = _playerControls.Player.PrevSlot;
        PrevSlot.Enable();
    }

    private void OnDisable()
    {
        MoveAction.Disable();
        InteractAction.Disable();
        UseAction.Disable();
        EscapeAction.Disable();
        SpaceAction.Disable();
        OpenAction.Disable();
        
        SlotOne.Disable();
        SlotTwo.Disable();
        SlotThree.Disable();
        SlotFour.Disable();
        SlotFive.Disable();
        SlotSix.Disable();
        SlotSeven.Disable();
        SlotEight.Disable();
        SlotNine.Disable();
        SlotTen.Disable();
        SlotEleven.Disable();
        SlotTwelve.Disable();
        NextSlot.Disable();
        PrevSlot.Disable();
    }

    private void Update()
    {
        GamepadActive = Gamepad.current != null;
        
        //if(!Application.isEditor)
        //    Cursor.visible = !GamepadActive;
        
        if (InteractAction.WasPressedThisFrame())
        {
            if (_itemBarManager.IsWearingCorrectTool(ToolType.SWORD)) OnCharacterAttack(_characterManager);
            else OnCharacterInteraction(_characterManager);
        }

        if (UseAction.WasPressedThisFrame()) OnCharacterSecondaryInteraction(_characterManager);

        if (OpenAction.WasPressedThisFrame() && _characterManager is Player player)
        {
            if (player.PlayerInventoryUIManager.IsOpened) return;
            
            player.PlayerInventoryUIManager.Open();
        }
    }

    private void FixedUpdate()
    {
        OnCharacterMovement(MoveAction.ReadValue<Vector2>());
    }
}
