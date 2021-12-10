using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterControllerManager : MonoBehaviour
{
    private PlayerInvenotryUIManager _playerInvenotryUiManager;
    
    [SerializeField] private InputActionAsset  _characterInputAsset;
    [SerializeField] private InputAction _inputMovement;
    [SerializeField] private InputAction _inputInventory;

    public bool InputEnabled;
    
    public Vector2 CurrentDirection;
    
    private void Awake()
    {
        _playerInvenotryUiManager = GetComponent<PlayerInvenotryUIManager>();
        
        _characterInputAsset = GetComponent<PlayerInput>().actions;

        _inputMovement = _characterInputAsset.FindAction("Movement");
        
        _inputMovement.performed += content => Move(content.ReadValue<Vector2>());
        _inputMovement.canceled += content => CurrentDirection = Vector2.zero;

        _inputInventory = _characterInputAsset.FindAction("Open Inventory");
        _inputInventory.performed += content => _playerInvenotryUiManager.Interact();
    }

    private void Move(Vector2 pDirection)
    {
        CurrentDirection = pDirection;
    }

    public void ToggleInput()
    {
        Debug.Log("Toggle INPUT");
    }
}
