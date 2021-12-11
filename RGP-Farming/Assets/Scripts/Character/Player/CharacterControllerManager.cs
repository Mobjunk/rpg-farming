using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterControllerManager : MonoBehaviour
{

    [SerializeField] private InputActionAsset  _characterInputAsset;
    [SerializeField] private InputAction _inputMovement;

    public bool InputEnabled;
    public Vector2 CurrentDirection;

    private void Awake()
    {
        _characterInputAsset = GetComponent<PlayerInput>().actions;

        _inputMovement = _characterInputAsset.FindAction("Movement");
        _inputMovement.performed += content => Move(content.ReadValue<Vector2>());
        _inputMovement.canceled += content => CurrentDirection = Vector2.zero;
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
