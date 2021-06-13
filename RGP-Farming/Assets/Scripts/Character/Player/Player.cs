using UnityEngine;

[RequireComponent(typeof(CharacterInventory), typeof(PlayerInvenotryUIManager), typeof(CharacterInteractionManager))]
[RequireComponent(typeof(CharacterPlaceObject), typeof(CharacterUIManager))]
public class Player : CharacterManager
{
    private static Player intsance;

    public static Player Instance()
    {
        return intsance;
    }

    [SerializeField] private GameObject[] tileChecker;
    public GameObject[] TileChecker
    {
        get => tileChecker;
        set => tileChecker = value;
    }

    [SerializeField] private Item itemAboveHead;
    public Item ItemAboveHead
    {
        get => itemAboveHead;
        set => itemAboveHead = value;
    }

    [SerializeField] private SpriteRenderer itemAboveHeadRenderer;
    public SpriteRenderer ItemAboveHeadRenderer
    {
        get => itemAboveHeadRenderer;
        set => itemAboveHeadRenderer = value;
    }

    /// <summary>
    /// Character interaction
    /// </summary>
    private CharacterInteractionManager characterInteractionManager;
    public CharacterInteractionManager CharacterInteractionManager => characterInteractionManager;
    
    /// <summary>
    /// Players inventory
    /// </summary>
    private CharacterInventory characterInventory;
    public CharacterInventory CharacterInventory => characterInventory;

    /// <summary>
    /// The ui linked to the player's inventory
    /// </summary>
    private PlayerInvenotryUIManager playerInventoryUIManager;

    private CharacterPlaceObject characterPlaceObject;

    public CharacterPlaceObject CharacterPlaceObject
    {
        get => characterPlaceObject;
        set => characterPlaceObject = value;
    }

    private CharacterUIManager characterUIManager;

    public CharacterUIManager CharacterUIManager
    {
        get => characterUIManager;
        set => characterUIManager = value;
    }

    /// <summary>
    /// Checks if a player has a controller connected
    /// </summary>
    private bool controllerConnected;
    public bool ControllerConnected => controllerConnected;
    
    public override void Awake()
    {
        base.Awake();

        intsance = this;

        characterInteractionManager = GetComponent<CharacterInteractionManager>();
        characterInputManager = GetComponent<ICharacterInput>();
        characterInventory = GetComponent<CharacterInventory>();
        playerInventoryUIManager = GetComponent<PlayerInvenotryUIManager>();
        characterPlaceObject = GetComponent<CharacterPlaceObject>();
        characterUIManager = GetComponent<CharacterUIManager>();
    }

    public override void Start()
    {
        base.Start();

        SubscribeToInput();
        
        playerInventoryUIManager.Initialize(characterInventory);
    }

    public override void Update()
    {
        base.Update();

        controllerConnected = false;
        foreach(string name in Input.GetJoystickNames())
        {
            //Debug.Log("Controllername: " + name);
            //TODO: Add more controllers?
            switch (name)
            {
                case "Controller (Xbox 360 Wireless Receiver for Windows)":
                    controllerConnected = true;
                    break;
            }
        }

        if (controllerConnected && characterInputManager.GetType() != typeof(CharacterControllerManager)) UpdateInput<CharacterKeyboardManager, CharacterControllerManager>();
        else if (!controllerConnected && characterInputManager.GetType() != typeof(CharacterKeyboardManager)) UpdateInput<CharacterControllerManager, CharacterKeyboardManager>();
    }

    public void UpdateInput<T, Y>() where T : MonoBehaviour, ICharacterInput where Y : MonoBehaviour, ICharacterInput
    {
        Destroy(GetComponent<T>());
        characterInputManager = gameObject.AddComponent<Y>();

        SubscribeToInput();
    }

    private bool inputEnabled;
    public bool InputEnabled
    {
        get => inputEnabled;
        set => inputEnabled = value;
    }

    void SubscribeToInput()
    {
        characterInputManager.OnCharacterMovement += CharacterMovementMananger.Move;
        characterInputManager.OnCharacterInteraction += characterInteractionManager.OnCharacterInteraction;
        characterInputManager.OnCharacterSecondaryInteraction += characterInteractionManager.OnCharacterSecondaryInteraction;
        inputEnabled = true;
    }

    public void ToggleInput()
    {
        if (inputEnabled)
        {
            characterInputManager.OnCharacterMovement -= CharacterMovementMananger.Move;
            characterInputManager.OnCharacterInteraction -= characterInteractionManager.OnCharacterInteraction;
            characterInputManager.OnCharacterSecondaryInteraction -= characterInteractionManager.OnCharacterSecondaryInteraction;
            inputEnabled = false;
        } else SubscribeToInput();
    }
    
    public void AddStarterItems()
    {
        characterInventory.AddItem(ItemManager.Instance().ForName("Pickaxe"), show: true);
        characterInventory.AddItem(ItemManager.Instance().ForName("Axe"), show: true);
        characterInventory.AddItem(ItemManager.Instance().ForName("Hoe"), show: true);
        characterInventory.AddItem(ItemManager.Instance().ForName("Scythe"), show: true);
        characterInventory.AddItem(ItemManager.Instance().ForName("Watering can"), show: true);
        characterInventory.AddItem(ItemManager.Instance().ForName("Carrot seed"), 10, true);
        characterInventory.AddItem(ItemManager.Instance().ForName("Chest"));
    }
}
