using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class ContractInteraction : Singleton<ContractInteraction>
{
    private ContractDataManager _contractDataManager => ContractDataManager.Instance();
    private Player _player => Player.Instance();
    private CursorManager _cursorManager => CursorManager.Instance();
    
    private SpriteRenderer _spriteRenderer;
    
    [SerializeField] private Sprite[] _contractSprites;

    [HideInInspector] public AbstractContractData SelectedContract;

    [HideInInspector] public AcceptableContracts AcceptableContract;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Setup(AcceptableContracts pAcceptableContract, AbstractContractData pSelectedContract)
    {
        AcceptableContract = pAcceptableContract;
        SelectedContract = pSelectedContract;
        _spriteRenderer.sprite = _contractSprites[Random.Range(0, _contractSprites.Length)];
    }

    private void OnMouseDown()
    {
        if (_cursorManager.IsPointerOverUIElement())
        {
            _cursorManager.SetDefaultCursor();
            return;
        }
        
        if (_player.CharacterInteractionManager.GetInteractableObjects().Contains(transform.root.gameObject))
        {
            string rewards = SelectedContract.rewards.Aggregate("", (current, item) => current + $"{item.Amount}x {item.Item.itemName},");
            if (SelectedContract.receiveCoins) rewards += SelectedContract.minCoins != SelectedContract.maxCoins ? $"{SelectedContract.minCoins}-{SelectedContract.maxCoins} Coins," : $"{SelectedContract.maxCoins}x Coins,";
            rewards = rewards.Length > 0 ? rewards.Remove(rewards.Length - 1) : "N/A";

            _contractDataManager.SetupContract(SelectedContract.linkedNpc.name, $"{AcceptableContract.RequiredAmount} {SelectedContract.linkedItem.itemName}{(AcceptableContract.RequiredAmount > 1 ? "s" : "")}", "", rewards, AcceptableContract.DaysToComplete + $" day{(AcceptableContract.DaysToComplete > 1 ? "s" : "")} from now", AcceptableContract.ExpireDate.ToString("ddd, dd MMM yyyy HH:mm:ss"));
        }
    }

    private void OnMouseOver()
    {
        if (_cursorManager.IsPointerOverUIElement())
        {
            _cursorManager.SetDefaultCursor();
            return;
        }
        
        if(_player.CharacterInteractionManager.GetInteractableObjects().Contains(transform.root.gameObject)) _cursorManager.SetUsableInteractionCursor();
        else _cursorManager.SetNonUsableInteractionCursor();
    }

    private void OnMouseExit() => _cursorManager.SetDefaultCursor();

    public void Clear()
    {
        SelectedContract = null;
        AcceptableContract = null;
    }
}
