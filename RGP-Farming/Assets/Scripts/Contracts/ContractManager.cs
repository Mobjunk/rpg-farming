using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class ContractManager : Singleton<ContractManager>
{
    /// <summary>
    /// Singleton references
    /// </summary>
    private Player _player => Player.Instance();
    private TimeManager _timeManager => TimeManager.Instance();
    private ContractInteraction _contractInteraction => ContractInteraction.Instance();
    private ContractDataManager _contractDataManager => ContractDataManager.Instance();
    
    /// <summary>
    /// The player's contract manager
    /// </summary>
    [SerializeField] private CharacterContractManager _characterContractManager;
    
    /// <summary>
    /// All possible contracts the system can pick from
    /// </summary>
    [SerializeField] private List<AbstractContractData> _possibleContracts = new List<AbstractContractData>();

    /// <summary>
    /// A list of all the contracts the player can currently accept
    /// </summary>
    [SerializeField] private List<AcceptableContracts> _acceptableContracts = new List<AcceptableContracts>(5);

    /// <summary>
    /// All possible spawn points of the contracts
    /// </summary>
    [SerializeField] private List<GameObject> _spawnPoints = new List<GameObject>(5);

    /// <summary>
    /// The prafab of a contract
    /// </summary>
    [SerializeField] private GameObject _contractPrefab;
    
    private void Start()
    {
        _characterContractManager = _player.GetComponent<CharacterContractManager>();

        int randomAmountOfContracts = Random.Range(1, 5);
        Debug.Log("randomAmountOfContracts: " + randomAmountOfContracts);
        for (int index = 0; index < randomAmountOfContracts; index++)
        {
            GameObject spawnPoint = _spawnPoints[Random.Range(0, _spawnPoints.Count)];
            _spawnPoints.Remove(spawnPoint);

            GameObject contractObject = Instantiate(_contractPrefab, parent: spawnPoint.transform);
            
            AbstractContractData abstractContractData = _possibleContracts[Random.Range(0, _possibleContracts.Count)];
            //TODO: Add something to make the contract amount random
            //TODO: Add something to make the days to complete based on the difficulty of the task
            AcceptableContracts acceptableContract = new AcceptableContracts(spawnPoint, abstractContractData, 1, 5, 1);
            _acceptableContracts.Add(acceptableContract);
            
            contractObject.GetComponent<ContractInteraction>().Setup(acceptableContract, abstractContractData);
        }
    }

    private void Update()
    {
        List<AcceptableContracts> toRemove = (from acceptableContracts in _acceptableContracts let interval = acceptableContracts.ExpireDate - _timeManager.CurrentGameTime where interval <= TimeSpan.Zero select acceptableContracts).ToList();
        Remove(toRemove);
    }

    public void RemoveContract(AcceptableContracts pDeclinedContract)
    {
        List<AcceptableContracts> toRemove = _acceptableContracts.Where(acceptableContracts => acceptableContracts.Equals(pDeclinedContract)).ToList();
        Remove(toRemove);
    }

    public void Remove(List<AcceptableContracts> pToRemove)
    {
        foreach (AcceptableContracts remove in pToRemove)
        {
            _spawnPoints.Add(remove.SpawnPoint);
            
            foreach (Transform child in remove.SpawnPoint.transform)
                Destroy(child.gameObject);

            if (_contractInteraction.AcceptableContract.Equals(remove))
            {
                _contractDataManager.Close();
                _contractInteraction.Clear();
            }
            
            _acceptableContracts.Remove(remove);
        }
        pToRemove.Clear();
    }
    
    /*public void AssignContract(ContractTypes pContractType)
    {
        List<AbstractContractData> possibleContracts = GetFilteredList(pContractType);
        
        AbstractContractData givenContract = possibleContracts[Random.Range(0, possibleContracts.Count)];
        if (CanAssignContract(givenContract))
        {
            _characterContractManager.ContractsInProgress.Add(new Contract(givenContract, 5));
        } else Debug.LogError("Contract couldn't be given...");
    }*/

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
    public GameObject SpawnPoint;
    public AbstractContractData Contract;
    public DateTime ExpireDate;
    public int RequiredAmount;
    public int DaysToComplete;

    public AcceptableContracts(GameObject pSpawnPoint, AbstractContractData pContract, int pDaysTillExpired, int pRequiredAmount, int pDaysToComplete)
    {
        SpawnPoint = pSpawnPoint;
        Contract = pContract;
        ExpireDate = TimeManager.Instance().GetNewDate(pAddedDays: pDaysTillExpired);
        RequiredAmount = pRequiredAmount;
        DaysToComplete = pDaysToComplete;
    }
}