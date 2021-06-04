using UnityEngine;

public class Player : CharacterManager
{
    public override void Awake()
    {
        base.Awake();
        
        characterInputManager = GetComponent<ICharacterInput>();
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
        //characterInputManager.OnCharacterMovement += characterMovementManager.Move;
        //characterInputManager.OnCharacterInteraction += characterInteractionManager.OnCharacterInteraction;
        //characterInputManager.OnCharacterAttack += CharacterAttackManager.Attack;
    }
}
