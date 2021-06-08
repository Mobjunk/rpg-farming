using UnityEngine;

[RequireComponent(typeof(CharacterInventory), typeof(PlayerInvenotryUIManager), typeof(CharacterInteractionManager))]
public class Player : CharacterManager
{

    private static Player intsance;

    public static Player Instance()
    {
        return intsance;
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

    /// <summary>
    /// Checks if a player has a controller connected
    /// </summary>
    private bool controllerConnected;
    public bool ControllerConnected => controllerConnected;


    /// <summary>
    /// The current gameobject the player is interacting with
    /// </summary>
    private GameObject interactedObject;

    public GameObject InteractedObject
    {
        get => interactedObject;
        set => interactedObject = value;
    }
    
    public override void Awake()
    {
        base.Awake();

        intsance = this;

        characterInteractionManager = GetComponent<CharacterInteractionManager>();
        characterInputManager = GetComponent<ICharacterInput>();
        characterInventory = GetComponent<CharacterInventory>();
        playerInventoryUIManager = GetComponent<PlayerInvenotryUIManager>();
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

    void SubscribeToInput()
    {
        characterInputManager.OnCharacterMovement += CharacterMovementMananger.Move;
        characterInputManager.OnCharacterInteraction += characterInteractionManager.OnCharacterInteraction;
        //characterInputManager.OnCharacterAttack += CharacterAttackManager.Attack;
    }

    public void Add()
    {
        characterInventory.AddItem(ItemManager.Instance().ForName("Pickaxe"));
        characterInventory.AddItem(ItemManager.Instance().ForName("Axe"));
    }

    public void Remove()
    {
        characterInventory.RemoveItem(ItemManager.Instance().ForName("Pickaxe"));
    }

    public void Coins()
    {
        characterInventory.UpdateCoins(-100);
    }
}
