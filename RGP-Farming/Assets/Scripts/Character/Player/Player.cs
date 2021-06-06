using UnityEngine;

[RequireComponent(typeof(CharacterInventory), typeof(PlayerInvenotryUIManager))]
public class Player : CharacterManager
{

    private CharacterInventory characterInventory;
    private PlayerInvenotryUIManager playerInventoryUIManager;
    
    public override void Awake()
    {
        base.Awake();
        
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

        bool controllerConnected = false;
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
        //characterInputManager.OnCharacterInteraction += characterInteractionManager.OnCharacterInteraction;
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
}
