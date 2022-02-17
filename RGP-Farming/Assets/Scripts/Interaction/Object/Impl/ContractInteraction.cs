using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class ContractInteraction : MonoBehaviour
{
    private ContractDataManager _contractDataManager => ContractDataManager.Instance();
    private Player _player => Player.Instance();
    private CursorManager _cursorManager => CursorManager.Instance();
    
    private SpriteRenderer _spriteRenderer;
    
    [SerializeField] private Sprite[] _contractSprites;

    private AbstractContractData _selectedContract;

    private AcceptableContracts _acceptableContract;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Setup(AcceptableContracts pAcceptableContract, AbstractContractData pSelectedContract)
    {
        _acceptableContract = pAcceptableContract;
        _selectedContract = pSelectedContract;
        _spriteRenderer.sprite = _contractSprites[Random.Range(0, _contractSprites.Length)];
    }

    private void OnMouseDown()
    {
        if (_player.CharacterInteractionManager.GetInteractableObjects().Contains(transform.root.gameObject))
        {
            Debug.Log("parent.name: " + transform.root.name);
            Debug.Log("contract: " + _selectedContract.name);
            
            string rewards = _selectedContract.rewards.Aggregate("", (current, item) => current + $"{item.Amount}x {item.Item.itemName},");
            if (_selectedContract.receiveCoins) rewards += _selectedContract.minCoins != _selectedContract.maxCoins ? $"{_selectedContract.minCoins}-{_selectedContract.maxCoins} Coins," : $"{_selectedContract.maxCoins}x Coins,";
            rewards = rewards.Length > 0 ? rewards.Remove(rewards.Length - 1) : "N/A";
            
            _contractDataManager.SetupContract(_selectedContract.linkedNpc.name, $"amount here x {_selectedContract.linkedItem.itemName}", "", rewards, "TODO", _acceptableContract.ExpireDate.ToString("ddd, dd MMM yyyy HH:mm:ss"));
        }
    }

    private void OnMouseOver()
    {
        if(_player.CharacterInteractionManager.GetInteractableObjects().Contains(transform.root.gameObject)) _cursorManager.SetUsableInteractionCursor();
        else _cursorManager.SetNonUsableInteractionCursor();
    }

    private void OnMouseExit() => _cursorManager.SetDefaultCursor();
}
