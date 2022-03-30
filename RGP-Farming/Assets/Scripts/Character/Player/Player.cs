using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterInventory), typeof(PlayerInvenotryUIManager), typeof(CharacterInteractionManager))]
[RequireComponent(typeof(CharacterPlaceObject), typeof(CharacterUIManager), typeof(PlayerAttackManager))]
public class Player : CharacterManager
{
    private ItemManager _itemManager => ItemManager.Instance();
    private static Player _intsance;

    public static Player Instance() => _intsance;

    [SerializeField] private GameObject[] _tileChecker;
    public GameObject[] TileChecker => _tileChecker;

    [SerializeField] private GameObject[] _waterPartical;

    public GameObject[] WaterPartical
    {
        get => _waterPartical;
        set => _waterPartical = value;
    }
    
    [SerializeField] private GameItem _itemAboveHead;
    public GameItem ItemAboveHead
    {
        get => _itemAboveHead;
        set => _itemAboveHead = value;
    }

    [SerializeField] private Transform[] _attackPoints;

    public Transform[] AttackPoints => _attackPoints;

    [SerializeField] private SpriteRenderer _itemAboveHeadRenderer;
    public SpriteRenderer ItemAboveHeadRenderer => _itemAboveHeadRenderer;

    /// <summary>
    /// Character interaction
    /// </summary>
    private CharacterInteractionManager _characterInteractionManager;
    public CharacterInteractionManager CharacterInteractionManager => _characterInteractionManager;
    
    /// <summary>
    /// Players inventory
    /// </summary>
    private CharacterInventory _characterInventory;
    public CharacterInventory CharacterInventory => _characterInventory;

    /// <summary>
    /// The ui linked to the player's inventory
    /// </summary>
    private PlayerInvenotryUIManager _playerInventoryUIManager;

    public PlayerInvenotryUIManager PlayerInventoryUIManager => _playerInventoryUIManager;

    private CharacterPlaceObject _characterPlaceObject;

    public CharacterPlaceObject CharacterPlaceObject => _characterPlaceObject;

    private CharacterUIManager _characterUIManager;

    public CharacterUIManager CharacterUIManager => _characterUIManager;

    private PlayerFishing _playerFishing;

    public PlayerFishing PlayerFishing => _playerFishing;

    private PlayerAttackManager _playerAttackManager;

    public PlayerAttackManager PlayerAttackManager => _playerAttackManager;

    private CharacterEnergyManager _characterEnergyManager;

    public CharacterEnergyManager CharacterEnergyManager => _characterEnergyManager;

    private bool _inputEnabled;
    public bool InputEnabled => _inputEnabled;
    

    /// <summary>
    /// Checks if a player has a controller connected
    /// </summary>
    //private bool _controllerConnected;
    //public bool ControllerConnected => _controllerConnected;

    public override void Awake()
    {
        base.Awake();

        Application.targetFrameRate = 30;

        _intsance = this;

        _characterInteractionManager = GetComponent<CharacterInteractionManager>();
        _characterInputManager = GetComponent<ICharacterInput>();
        _characterInventory = GetComponent<CharacterInventory>();
        _playerInventoryUIManager = GetComponent<PlayerInvenotryUIManager>();
        _characterPlaceObject = GetComponent<CharacterPlaceObject>();
        _characterUIManager = GetComponent<CharacterUIManager>();
        _playerFishing = GetComponent<PlayerFishing>();
        _playerAttackManager = GetComponent<PlayerAttackManager>();
        _characterEnergyManager = GetComponent<CharacterEnergyManager>();
    }

    public override void Start()
    {
        base.Start();

        SubscribeToInput();
        
        _playerInventoryUIManager.Initialize(_characterInventory);
    }

    /*public override void Update()
    {
        base.Update();
        
        _controllerConnected = false;
        foreach(string name in Input.GetJoystickNames())
        {
            //Debug.Log("Controllername: " + name);
            //TODO: Add more controllers?
            switch (name)
            {
                case "Controller (Xbox 360 Wireless Receiver for Windows)":
                    _controllerConnected = true;
                    break;
            }
        }

        if (_controllerConnected && _characterInputManager.GetType() != typeof(CharacterControllerManager)) UpdateInput<CharacterKeyboardManager, CharacterControllerManager>();
        else if (!_controllerConnected && _characterInputManager.GetType() != typeof(CharacterKeyboardManager)) UpdateInput<CharacterControllerManager, CharacterKeyboardManager>();
    }*/

    public void UpdateInput<T, Y>() where T : MonoBehaviour, ICharacterInput where Y : MonoBehaviour, ICharacterInput
    {
        Destroy(GetComponent<T>());
        _characterInputManager = gameObject.AddComponent<Y>();

        SubscribeToInput();
    }

    void SubscribeToInput()
    {
        _characterInputManager.OnCharacterMovement += CharacterMovementMananger.Move;
        _characterInputManager.OnCharacterInteraction += _characterInteractionManager.OnCharacterInteraction;
        _characterInputManager.OnCharacterSecondaryInteraction += _characterInteractionManager.OnCharacterSecondaryInteraction;
        _characterInputManager.OnCharacterAttack += _playerAttackManager.Attack;
        _inputEnabled = true;
    }

    public void ToggleInput()
    {
        if (_inputEnabled)
        {
            _characterInputManager.OnCharacterMovement -= CharacterMovementMananger.Move;
            _characterInputManager.OnCharacterInteraction -= _characterInteractionManager.OnCharacterInteraction;
            _characterInputManager.OnCharacterSecondaryInteraction -= _characterInteractionManager.OnCharacterSecondaryInteraction;
            _characterInputManager.OnCharacterAttack -= _playerAttackManager.Attack;
            _inputEnabled = false;
        } else SubscribeToInput();
    }
    
    public void AddStarterItems()
    {
        _characterInventory.AddItem(_itemManager.ForName("Pickaxe"), pShow: true);
        _characterInventory.AddItem(_itemManager.ForName("Axe"), pShow: true);
        //_characterInventory.AddItem(_itemManager.ForName("Hoe"), pShow: true);
        //_characterInventory.AddItem(_itemManager.ForName("Scythe"), pShow: true);
        //_characterInventory.AddItem(_itemManager.ForName("Sword"), pShow: true);
        //_characterInventory.AddItem(_itemManager.ForName("Watering can"), pShow: true);
        //_characterInventory.AddItem(_itemManager.ForName("Fishing rod"), pShow: true);
        //_characterInventory.AddItem(_itemManager.ForName("Fishing bait"), 10, pShow: true);
        //_characterInventory.AddItem(_itemManager.ForName("Carrot seed"), 10, true);

    }
}
