using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class ContractManager : Singleton<ContractManager>
{
    private Player _player => Player.Instance();

    private TimeManager _timeManager => TimeManager.Instance();

    [SerializeField] private SpriteRenderer _spriteRenderer;
    
    [SerializeField] private CharacterContractManager _characterContractManager;
    
    [SerializeField] private List<AbstractContractData> _possibleContracts = new List<AbstractContractData>();

    [SerializeField] private List<AcceptableContracts> _acceptableContracts = new List<AcceptableContracts>(5);

    [SerializeField] private Sprite[] _boardSprites;
    
    //[Serializable] private List

    private void Awake()
    {
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    private void Start()
    {
        _characterContractManager = _player.GetComponent<CharacterContractManager>();

        int randomAmountOfContracts = Random.Range(1, 5);
        Debug.Log("randomAmountOfContracts: " + randomAmountOfContracts);
        for (int index = 0; index < randomAmountOfContracts; index++)
        {
            AbstractContractData abstractContractData = _possibleContracts[Random.Range(0, _possibleContracts.Count)];
            _acceptableContracts.Add(new AcceptableContracts(abstractContractData, 1));
        }
    }

    private void Update()
    {
        List<AcceptableContracts> toRemove = new List<AcceptableContracts>();
        foreach (AcceptableContracts acceptableContracts in _acceptableContracts)
        {
            TimeSpan interval = acceptableContracts.ExpireDate - _timeManager.CurrentGameTime;
            if (interval <= TimeSpan.Zero) toRemove.Add(acceptableContracts);
        }

        foreach (AcceptableContracts remove in toRemove) _acceptableContracts.Remove(remove);
        toRemove.Clear();

        if (_spriteRenderer.sprite != _boardSprites[_acceptableContracts.Count])
            _spriteRenderer.sprite = _boardSprites[_acceptableContracts.Count];
    }

    public void Test()
    {
        AssignContract(ContractTypes.CROPS);
    }

    public void CheatItems(int pIndex)
    {
        Contract currentContract = _characterContractManager.ContractsInProgress[pIndex];
        if (currentContract == null) return;
        
        _player.CharacterInventory.AddItem(currentContract.AbstractContractData.linkedItem, currentContract.RequiredAmount);
    }
    
    public void AssignContract(ContractTypes pContractType)
    {
        List<AbstractContractData> possibleContracts = GetFilteredList(pContractType);
        
        AbstractContractData givenContract = possibleContracts[Random.Range(0, possibleContracts.Count)];
        if (CanAssignContract(givenContract))
        {
            _characterContractManager.ContractsInProgress.Add(new Contract(givenContract, 5));
        } else Debug.LogError("Contract couldn't be given...");
    }

    public void CompleteContract(int pIndex)
    {
        Contract currentContract = _characterContractManager.ContractsInProgress[pIndex];
        if (!currentContract.CanFinishContract())
        {
            Debug.LogWarning("Not able to finish this contract...");
            return;
        }

        if (!_player.CharacterInventory.HasItem(currentContract.AbstractContractData.linkedItem, currentContract.RequiredAmount))
        {
            Debug.LogError("Does not have the required items...");
            return;
        }
        
        _player.CharacterInventory.RemoveItem(currentContract.AbstractContractData.linkedItem, currentContract.RequiredAmount);
        _characterContractManager.ContractsInProgress.Remove(currentContract);

        if (currentContract.AbstractContractData.receiveCoins)
        {
            int randomCoins = Random.Range(currentContract.AbstractContractData.minCoins, currentContract.AbstractContractData.maxCoins);
            _player.CharacterInventory.UpdateCoins(randomCoins);
        }

        foreach (GameItem reward in currentContract.AbstractContractData.rewards)
            _player.CharacterInventory.AddItem(reward.Item, reward.Amount, true);
    }

    public void CompleteContract(Contract pCurrentContract)
    {
        if (!pCurrentContract.CanFinishContract())
        {
            Debug.LogWarning("Not able to finish this contract...");
            return;
        }

        if (!_player.CharacterInventory.HasItem(pCurrentContract.AbstractContractData.linkedItem, pCurrentContract.RequiredAmount))
        {
            Debug.LogError("Does not have the required items...");
            return;
        }
        
        _player.CharacterInventory.RemoveItem(pCurrentContract.AbstractContractData.linkedItem, pCurrentContract.RequiredAmount);
        _characterContractManager.ContractsInProgress.Remove(pCurrentContract);
        Debug.Log("Finished the " + pCurrentContract.AbstractContractData.name + " contract!");

        if (pCurrentContract.AbstractContractData.receiveCoins)
        {
            int randomCoins = Random.Range(pCurrentContract.AbstractContractData.minCoins, pCurrentContract.AbstractContractData.maxCoins);
            _player.CharacterInventory.UpdateCoins(randomCoins);
        }

        foreach (GameItem reward in pCurrentContract.AbstractContractData.rewards)
            _player.CharacterInventory.AddItem(reward.Item, reward.Amount, true);
    }

    /// <summary>
    /// Handles getting a list of specific contract types
    /// </summary>
    /// <param name="pContractType">The type of contract the player is gettinng</param>
    /// <returns></returns>
    private List<AbstractContractData> GetFilteredList(ContractTypes pContractType)
    {
        return _possibleContracts.Where(contract => contract.contractType.Equals(pContractType)).ToList();
    }
    
    /// <summary>
    /// Checks if the given contract doesn't already exist within the players contracts in progress
    /// </summary>
    /// <param name="pGivenContract">The contract that is going to be given to the player</param>
    /// <returns>If it is possible for this contract to be given</returns>
    private bool CanAssignContract(AbstractContractData pGivenContract)
    {
        return _characterContractManager.ContractsInProgress.All(currentContract => !currentContract.AbstractContractData.Equals(pGivenContract));
    }
}

[Serializable]
public class AcceptableContracts
{
    public AbstractContractData Contract;
    public DateTime ExpireDate;

    public AcceptableContracts(AbstractContractData pContract, int pDaysTillExpired)
    {
        Contract = pContract;
        ExpireDate = TimeManager.Instance().GetNewDate(pAddedDays: pDaysTillExpired);
    }
}