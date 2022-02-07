using UnityEngine;

[RequireComponent(typeof(CharacterInventory), typeof(PlayerInvenotryUIManager), typeof(CharacterInteractionManager))]
[RequireComponent(typeof(CharacterPlaceObject), typeof(CharacterUIManager))]
public class Player : CharacterManager
{
    private ItemManager _itemManager => ItemManager.Instance();
    private static Player _intsance;

    public static Player Instance()
    {
        return _intsance;
    }

    [SerializeField] private GameObject[] _tileChecker;
    public GameObject[] TileChecker
    {
        get => _tileChecker;
        set => _tileChecker = value;
    }

    [SerializeField] private GameItem _itemAboveHead;
    public GameItem ItemAboveHead
    {
        get => _itemAboveHead;
        set => _itemAboveHead = value;
    }

    [SerializeField] private SpriteRenderer _itemAboveHeadRenderer;
    public SpriteRenderer ItemAboveHeadRenderer
    {
        get => _itemAboveHeadRenderer;
        set => _itemAboveHeadRenderer = value;
    }

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

    public PlayerInvenotryUIManager PlayerInventoryUIManager
    {
        get => _playerInventoryUIManager;
    }

    private CharacterPlaceObject _characterPlaceObject;

    public CharacterPlaceObject CharacterPlaceObject
    {
        get => _characterPlaceObject;
        set => _characterPlaceObject = value;
    }

    private CharacterUIManager _characterUIManager;

    public CharacterUIManager CharacterUIManager
    {
        get => _characterUIManager;
        set => _characterUIManager = value;
    }

    /// <summary>
    /// Checks if a player has a controller connected
    /// </summary>
    private bool _controllerConnected;
    public bool ControllerConnected => _controllerConnected;
    
    public override void Awake()
    {
        base.Awake();

        _intsance = this;

        _characterInteractionManager = GetComponent<CharacterInteractionManager>();
        _characterInputManager = GetComponent<ICharacterInput>();
        _characterInventory = GetComponent<CharacterInventory>();
        _playerInventoryUIManager = GetComponent<PlayerInvenotryUIManager>();
        _characterPlaceObject = GetComponent<CharacterPlaceObject>();
        _characterUIManager = GetComponent<CharacterUIManager>();
    }

    public override void Start()
    {
        base.Start();

        SubscribeToInput();
        
        _playerInventoryUIManager.Initialize(_characterInventory);
    }

    public override void Update()
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
    }

    public void UpdateInput<T, Y>() where T : MonoBehaviour, ICharacterInput where Y : MonoBehaviour, ICharacterInput
    {
        Destroy(GetComponent<T>());
        _characterInputManager = gameObject.AddComponent<Y>();

        SubscribeToInput();
    }

    private bool _inputEnabled;
    public bool InputEnabled
    {
        get => _inputEnabled;
        set => _inputEnabled = value;
    }

    void SubscribeToInput()
    {
        _characterInputManager.OnCharacterMovement += CharacterMovementMananger.Move;
        _characterInputManager.OnCharacterInteraction += _characterInteractionManager.OnCharacterInteraction;
        _characterInputManager.OnCharacterSecondaryInteraction += _characterInteractionManager.OnCharacterSecondaryInteraction;
        _inputEnabled = true;
    }

    public void ToggleInput()
    {
        if (_inputEnabled)
        {
            _characterInputManager.OnCharacterMovement -= CharacterMovementMananger.Move;
            _characterInputManager.OnCharacterInteraction -= _characterInteractionManager.OnCharacterInteraction;
            _characterInputManager.OnCharacterSecondaryInteraction -= _characterInteractionManager.OnCharacterSecondaryInteraction;
            _inputEnabled = false;
        } else SubscribeToInput();
    }
    
    public void AddStarterItems()
    {
        _characterInventory.AddItem(_itemManager.ForName("Pickaxe"), pShow: true);
        _characterInventory.AddItem(_itemManager.ForName("Axe"), pShow: true);
        _characterInventory.AddItem(_itemManager.ForName("Hoe"), pShow: true);
        _characterInventory.AddItem(_itemManager.ForName("Scythe"), pShow: true);
        _characterInventory.AddItem(_itemManager.ForName("Watering can"), pShow: true);
        _characterInventory.AddItem(_itemManager.ForName("Coal"), 100);
        _characterInventory.AddItem(_itemManager.ForName("Iron ore"), 100);
        _characterInventory.AddItem(_itemManager.ForName("Chest"));
        _characterInventory.AddItem(_itemManager.ForName("Furnace"));
        _characterInventory.AddItem(_itemManager.ForName("Carrot seed"), 10, true);
        _characterInventory.AddItem(_itemManager.ForName("Cabbage seed"), 10);
        _characterInventory.AddItem(_itemManager.ForName("Eggplant seed"), 10);
        _characterInventory.AddItem(_itemManager.ForName("Lemon seed"), 10);
        _characterInventory.AddItem(_itemManager.ForName("Onion seed"), 10);
        _characterInventory.AddItem(_itemManager.ForName("Pineapple seed"), 10);
        _characterInventory.AddItem(_itemManager.ForName("Tomato seed"), 10);
        _characterInventory.AddItem(_itemManager.ForName("Watermelon seed"), 10);
        /*characterInventory.AddItem(itemManager.ForName("Wood"), 50);
        characterInventory.AddItem(itemManager.ForName("Stone"), 50);*/

    }
}
